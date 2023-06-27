using BeverageVendingMachine.Application.DTOs;
using BeverageVendingMachine.Core.Common;
using BeverageVendingMachine.Core.DTOs;
using BeverageVendingMachine.Core.Entities;
using BeverageVendingMachine.Core.Interfaces.Repositories;
using BeverageVendingMachine.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeverageVendingMachine.Core.Services
{
    /// <summary>
    /// Service to imitate terminal (operating system) to operate with the vending machine
    /// </summary>
    public sealed class TerminalService : ITerminalService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICoinDenominationRepository _coinDenominationRepository;
        private readonly IStorageService _storage;

        public TerminalService(IUnitOfWork unitOfWork, ICoinDenominationRepository coinDenominationRepository, IStorageService storage)
        {
            _unitOfWork = unitOfWork;
            _coinDenominationRepository = coinDenominationRepository;
            _storage = storage;
        }

        /// <summary>
        /// Get the instance of storage singleton
        /// </summary>
        /// <returns>Returns the instance of storage singleton</returns>
        public IStorageService GetStorageInstance()
        {
            return _storage;
        }


        #region Fields
        /// <summary>
        /// Collection of coin denomination Ids that were blocked
        /// </summary>
        public List<int> BlockedCoinDenominations { get; set; } = new List<int>();

        /// <summary>
        /// Selected item for a purchase
        /// </summary>
        private StorageItem PurchaseItem { get; set; }
        #endregion

        #region Admin interface

        /// <summary>
        /// Blocks passed coin denomination by Id
        /// </summary>
        /// <param name="coinDenominationId">Id of the coin denomination to block</param>
        /// <returns>Returns 1 if successful, 0 if the coin is already blocked, -1 if there was error</returns>
        public int BlockCoinDenomination(int coinDenominationId)
        {
            var result = 1;
            if (!BlockedCoinDenominations.Any(coinDenomination => coinDenomination == coinDenominationId))
                BlockedCoinDenominations.Add(coinDenominationId);
            else result = 0;
            return result;
        }

        /// <summary>
        /// Unblock passed coin denomination by Id
        /// </summary>
        /// <param name="coinDenominationId">Id of the coin denomination to unblock</param>
        /// <returns>Returns 1 if successful, 0 if the coin is already unblocked, -1 if there was error</returns>
        public int UnblockCoinDenomination(int coinDenominationId)
        {
            var result = 1;
            if (BlockedCoinDenominations.Any(coinDenomination => coinDenomination == coinDenominationId))
                BlockedCoinDenominations.RemoveAll(coinDenomination => coinDenomination == coinDenominationId);
            else result = 0;
            return result;
        }

        #endregion


        #region Load terminal data
        
        /// <summary>
        /// Gets all existing coins
        /// </summary>
        /// <returns>All existing coins</returns>
        public async Task<IReadOnlyList<CoinDenomination>> GetCoins()
        {
            var coins = await _unitOfWork.Repository<CoinDenomination>().GetAllAsync();
            return coins;
        }

        /// <summary>
        /// Gets all existing storage items
        /// </summary>
        /// <returns>All existing storage items</returns>
        public async Task<IReadOnlyList<StorageItem>> GetStorageItems()
        {
            return await _unitOfWork.Repository<StorageItem>().GetAllAsync();
        }
        #endregion


        #region User interface

        /// <summary>
        /// Gets update data from the vending machine terminal
        /// </summary>
        /// <returns>Update data from the vending machine terminal</returns>
        public UpdateData GetTerminalUpdateData()
        {
            return new UpdateData(_storage.DepositedAmount, CalculateChange(), _storage.CoinDenominations, _storage.StorageItems.ConvertToProduct(PurchaseItem == null ? 0 : PurchaseItem.Id));
        }

        /// <summary>
        /// To deposit a coin to a vending machine temporary storage for a purchase
        /// </summary>
        /// <param name="coinDenominationId">Id of the coin denomination you want to deposit</param>
        /// <returns>Update data for vending machine terminal</returns>
        public async Task<UpdateData> DepositCoin(int coinDenominationId)
        {
            var coin = _storage.DepositCoin(coinDenominationId);
            await _unitOfWork.Repository<CoinDenomination>().UpdateAsync(coin);
            await _unitOfWork.Complete();
            return GetTerminalUpdateData();
        }

        /// <summary>
        /// Selects item for a puchase
        /// </summary>
        /// <param name="purchaseItemId">Id of the selected purchase item</param>
        /// <returns>Update data for vending machine terminal</returns>
        public UpdateData SelectPurchaseItem(int purchaseItemId)
        {
            PurchaseItem = _storage.StorageItems.FirstOrDefault(storageItem => storageItem.Id == purchaseItemId);
            return GetTerminalUpdateData();
        }

        /// <summary>
        /// Unselects item for a puchase
        /// </summary>
        /// <returns>Update data for vending machine terminal</returns>
        public UpdateData UnselectPurchaseItem()
        {
            PurchaseItem = null;
            return GetTerminalUpdateData();
        }

        /// <summary>
        /// Calculates change by subtracting selected item cost from the deposited amount
        /// </summary>
        /// <returns>Returns amount to be returned to a vending machine user</returns>
        public decimal CalculateChange()
        {
            return PurchaseItem == null ? GetDepositedAmount() : GetDepositedAmount() - PurchaseItem.Cost;
        }

        /// <summary>
        /// Returns the amount of deposited coins for a purchase
        /// </summary>
        /// <returns>the amount of deposited coins for a purchase</returns>
        public decimal GetDepositedAmount()
        {
            return GetStorageInstance().DepositedAmount;
        }

        /// <summary>
        /// Takes purchase item from inventory, deducts the purchase item cost from deposited coins
        /// </summary>
        /// <returns>Returns purchase item from inventory</returns>
        public StorageItem TakePurchaseItemFromInventory()
        {
            if(PurchaseItem == null) throw new Exception("Purchase item is not selected.");

            var result = _storage.TakeItemFromStorageItems(PurchaseItem.Id);
            UnselectPurchaseItem();

            try
            {
                _storage.TakeAmountFromDepositedAndStorageCoins(result.Cost);
                _unitOfWork.Complete();
            }
            catch
            {
                _storage.AddStorageItem(result);
                SelectPurchaseItem(result.Id);
                throw;
            }

            return result;
        }

        /// <summary>
        /// Releases change
        /// </summary>
        /// <returns>Returns coins collection for a customer</returns>
        public async Task<CoinsCollection> ReleaseChange()
        {
            var change = CalculateChange();
            if (change >= 0)
            {
                try
                {
                    var changeCoins = GetStorageInstance().TakeAmountFromDepositedAndStorageCoins(change);

                    foreach (var coinDenominationGroup in changeCoins.CoinDenominationsQuantity.Where(coinDenominationGroup => coinDenominationGroup.Value > 0))
                    {
                        var coinDenomination = await _coinDenominationRepository.GetCoinDenominationByValue(coinDenominationGroup.Key);
                        await _unitOfWork.Repository<CoinDenomination>().UpdateAsync(coinDenomination);
                        var coinOperation = new CoinOperation(PurchaseItem, coinDenomination, coinDenominationGroup.Value, false);
                        await _unitOfWork.Repository<CoinOperation>().AddAsync(coinOperation);
                    }
                    await _unitOfWork.Complete();
                    return changeCoins;
                }
                catch
                {
                    throw;
                }
            }
            else
            {
                throw new Exception("Not enough deposited amount.");
            }
        }

        /// <summary>
        /// Releases purchase item and change
        /// </summary>
        /// <returns>Returns an object with the purchase item and change inside</returns>
        /// <exception cref="Exception">Not enough deposited amount.</exception>
        public async Task<PurchaseResult> ReleasePurchaseItemAndChange()
        {
            try
            {
                return new PurchaseResult(TakePurchaseItemFromInventory(), await ReleaseChange());
            }
            catch
            {
                throw;
            }
        }
        
        #endregion
    }
}

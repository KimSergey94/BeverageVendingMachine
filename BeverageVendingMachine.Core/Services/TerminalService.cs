using BeverageVendingMachine.Application.DTOs;
using BeverageVendingMachine.Core.Common;
using BeverageVendingMachine.Core.Entities;
using BeverageVendingMachine.Core.Entities.Aggregates.StorageAggregate;
using BeverageVendingMachine.Core.Interfaces.Entities;
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

        /// <summary>
        /// Singleton vending machine storage instance
        /// </summary>
        private static Storage _storage;

        protected TerminalService(IUnitOfWork unitOfWork, ICoinDenominationRepository coinDenominationRepository)
        {
            _unitOfWork = unitOfWork;
            _coinDenominationRepository = coinDenominationRepository;
        }

        /// <summary>
        /// Get the instance of storage singleton
        /// </summary>
        /// <returns>Returns the instance of storage singleton</returns>
        public static Storage GetStorageInstance()
        {
            if (_storage == null)
            {
                //init last state from db

                _storage = new Storage();//add params
            }
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
        private IStorageItem PurchaseItem { get; set; }
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


        #region User interface

        /// <summary>
        /// To deposit a coin to a vending machine temporary storage for a purchase
        /// </summary>
        /// <param name="coin">Coin you want to deposit</param>
        public void DepositCoin(CoinDenomination coin)
        {
            _storage.DepositCoin(coin);
        }

        /// <summary>
        /// Selects item for a puchase
        /// </summary>
        /// <param name="purchaseItem">Selected inventory item for a purchase</param>
        public void SelectPurchaseItem(IStorageItem purchaseItem)
        {
            PurchaseItem = purchaseItem;
        }

        /// <summary>
        /// Unselects item for a puchase
        /// </summary>
        public void UnselectPurchaseItem()
        {
            PurchaseItem = null;
        }

        /// <summary>
        /// Calculates change by subtracting selected item cost from the deposited amount
        /// </summary>
        /// <returns>Returns amount to be returned to a vending machine user</returns>
        public double CalculateChange()
        {
            return GetDepositedAmount() - PurchaseItem.Cost;
        }

        /// <summary>
        /// Returns the amount of deposited coins for a purchase
        /// </summary>
        /// <returns>the amount of deposited coins for a purchase</returns>
        public double GetDepositedAmount()
        {
            return GetStorageInstance().DepositedAmount;
        }

        /// <summary>
        /// Takes purchase item from inventory, deducts the purchase item cost from deposited coins
        /// </summary>
        /// <returns>Returns purchase item from inventory</returns>
        public IStorageItem TakePurchaseItemFromInventory()
        {
            var result = _storage.TakeItemFromStorageItems(PurchaseItem);
            UnselectPurchaseItem();

            try
            {
                //needs to be checked
                _storage.TakePurchasedItemCostFromDepositedCoins(result.Cost);
            }
            catch
            {
                _storage.AddStorageItem(PurchaseItem);
                //needs to be checked
                SelectPurchaseItem(PurchaseItem);
                throw;
            }

            return result;
        }

        /// <summary>
        /// Releases change
        /// </summary>
        /// <returns>Returns change for a customer</returns>
        public async Task<SortedDictionary<double, List<CoinDenomination>>> ReleaseChange()
        {
            var change = CalculateChange();
            if (change >= 0)
            {
                try
                {
                    var changeCoins = GetStorageInstance().GetCoinsForChange(change);

                    foreach (var coinDenominationGroup in changeCoins)
                    {
                        var coinDenomination = await _coinDenominationRepository.GetCoinDenominationByValue(coinDenominationGroup.Key);
                        var coinOperation = new CoinOperation(coinDenomination, coinDenominationGroup.Value.Count, false);
                        await _unitOfWork.repository<CoinOperation>().AddAsync(coinOperation);
                    }
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

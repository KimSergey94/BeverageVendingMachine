using BeverageVendingMachine.Application.DTOs;
using BeverageVendingMachine.Core.Entities;
using BeverageVendingMachine.Core.Entities.StorageAggregate;
using BeverageVendingMachine.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeverageVendingMachine.Application.Services
{
    /// <summary>
    /// Service to imitate terminal to operate with the vending machine
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


        /// <summary>
        /// Collection of coin denomination Ids that were blocked
        /// </summary>
        public List<int> BlockedCoinDenominations { get; set; } = new List<int>();


        /// <summary>
        /// Selected item for a purchase
        /// </summary>
        private IStorageItem PurchaseItem { get; set; }


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
        /// Releases purchase item and change
        /// </summary>
        /// <returns>Returns what should be returned to a customer (purchase item and change)</returns>
        /// <exception cref="Exception">Not enough deposited amount.</exception>
        public async Task<PurchaseResult> ReleaseSelectedItemAndChange()
        {
            var result = new PurchaseResult(PurchaseItem, new SortedDictionary<double, List<CoinDenomination>>());

            var change = CalculateChange();
            if (change >= 0)
            {
                try
                {
                    result.Change = GetStorageInstance().GetCoinsForChange(change);

                    foreach (var coinDenominationGroup in result.Change)
                    {
                        var coinDenomination = await _coinDenominationRepository.GetCoinDenominationByValue(coinDenominationGroup.Key);
                        var coinOperation = new CoinOperation(coinDenomination, coinDenominationGroup.Value.Count, false);
                        await _unitOfWork.repository<CoinOperation>().AddAsync(coinOperation);
                    }

                    GetStorageInstance().TakeDepositedCoins();
                    _storage.TakePurchaseItemFromInventoryItems(PurchaseItem);
                    UnselectPurchaseItem();

                    //needs to be checked
                    return result;
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
        /// Blocks passed coin denomination by Id
        /// </summary>
        /// <param name="coinDenominationId">Id of the coin denomination to block</param>
        public void BlockCoinDenomination(int coinDenominationId)
        {
            if (!BlockedCoinDenominations.Any(coinDenomination => coinDenomination == coinDenominationId))
                BlockedCoinDenominations.Add(coinDenominationId);
        }

        /// <summary>
        /// Unblock passed coin denomination by Id
        /// </summary>
        /// <param name="coinDenominationId">Id of the coin denomination to unblock</param>
        public void UnblockCoinDenomination(int coinDenominationId)
        {
            if (BlockedCoinDenominations.Any(coinDenomination => coinDenomination == coinDenominationId))
                BlockedCoinDenominations.RemoveAll(coinDenomination => coinDenomination == coinDenominationId);
        }
    }
}

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
        private static StorageAggregate _storage;

        protected TerminalService(IUnitOfWork unitOfWork, ICoinDenominationRepository coinDenominationRepository)
        {
            _unitOfWork = unitOfWork;
            _coinDenominationRepository = coinDenominationRepository;
        }

        /// <summary>
        /// Get the instance of storage singleton
        /// </summary>
        /// <returns>Returns the instance of storage singleton</returns>
        public static StorageAggregate GetStorageInstance()
        {
            if (_storage == null)
            {
                //init last state from db

                _storage = new StorageAggregate();//add params
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
        private IStorageItem SelectedItem { get; set; }

        /// <summary>
        /// Selects item for a puchase
        /// </summary>
        /// <param name="selectedItem">Selected storage item</param>
        public void SelectItem(IStorageItem selectedItem)
        {
            SelectedItem = selectedItem;
        }

        /// <summary>
        /// Unselects item for a puchase
        /// </summary>
        public void UnselectItem()
        {
            SelectedItem = null;
        }

        /// <summary>
        /// Calculates change by subtracting selected item cost from the deposited amount
        /// </summary>
        /// <returns>Returns amount to be returned to a vending machine user</returns>
        public double CalculateChange()
        {
            return GetDepositedAmount() - SelectedItem.Cost;
        }

        /// <summary>
        /// Returns the deposited amount
        /// </summary>
        /// <returns>deposited amount</returns>
        public double GetDepositedAmount()
        {
            return GetStorageInstance().DepositedAmount;
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

        /// <summary>
        /// Releases selected storage item
        /// </summary>
        /// <returns>StorageItem type selectedItem</returns>
        /// <exception cref="Exception">Not enough deposited amount.</exception>
        public async Task<IStorageItem> ReleaseSelectedItemAndChange()
        {
            var result = SelectedItem;
            var change = CalculateChange();
            if (change >= 0)
            {
                try
                {
                    var coinsForChange = GetStorageInstance().GetCoinsForChange(change);

                    foreach (var coinDenominationGroup in coinsForChange)
                    {
                        var coinDenomination = await _coinDenominationRepository.GetCoinDenominationByValue(coinDenominationGroup.Key);
                        var coinOperation = new CoinOperation(coinDenomination, coinDenominationGroup.Value.Count, false);
                        await _unitOfWork.repository<CoinOperation>().AddAsync(coinOperation);
                    }
                    
                    GetStorageInstance().TakeDepositedCoins();
                    SelectedItem = null;
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
    }
}

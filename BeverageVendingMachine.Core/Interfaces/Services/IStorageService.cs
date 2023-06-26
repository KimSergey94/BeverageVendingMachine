using BeverageVendingMachine.Core.DTOs;
using BeverageVendingMachine.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeverageVendingMachine.Core.Interfaces.Services
{
    /// <summary>
    /// Represents vending machine physical storage interface
    /// </summary>
    public interface IStorageService
    {
        #region Fields

        /// <summary>
        /// CoinDenomination entities
        /// </summary>
        List<CoinDenomination> CoinDenominations { get; }

        /// <summary>
        /// Represents items inside vending machine storage
        /// </summary>
        List<StorageItem> StorageItems { get; set; }

        /// <summary>
        /// The total deposited amount from all deposited coins
        /// </summary>
        decimal DepositedAmount { get; }
        #endregion

        #region Coins operations

        /// <summary>
        /// To deposit a coin to a vending machine temporary storage for a purchase
        /// </summary>
        /// <param name="coinDenominationId">Id of the coin denomination you want to deposit</param>
        /// <returns>Deposited coin denomination entity</returns>
        CoinDenomination DepositCoin(int coinDenominationId);

        #region Take coins by amount operations

        /// <summary>
        /// Takes passed amount from deposited and storage coins and returns coins collection object
        /// </summary>
        /// <param name="amount">The amount to take from deposited and storage coins</param>
        CoinsCollection TakeAmountFromDepositedAndStorageCoins(decimal amount);

        /// <summary>
        /// Takes passed amount from deposited coins and returns coins collection object
        /// </summary>
        /// <param name="amount">The amount to take from deposited coins</param>
        CoinsCollection TakeAmountFromDepositedCoins(decimal amount);

        /// <summary>
        /// Provides coins collection for a change to a customer from storage coins
        /// </summary>
        /// <param name="amount">The change amount needed to be returned to a user</param>
        /// <returns>The coins collection to provide to user</returns>
        CoinsCollection GetCoinsForChange(decimal amount);

        #endregion
        #endregion

        #region Storage items operations

        /// <summary>
        /// Takes all items from vending machine storage
        /// </summary>
        /// <returns>Returns all storage items</returns>
        List<StorageItem> TakeAllItemsFromStorage();

        /// <summary>
        /// Takes item from vending machine storage
        /// </summary>
        /// <param name="item">Item from vending machine storage</param>
        /// <returns>Returns taken item from storage</returns>
        /// <exception cref="Exception">Not found purchase item</exception>
        StorageItem TakeItemFromStorageItems(int storageItemId);

        /// <summary>
        /// Adds passed item to vending machine storage
        /// </summary>
        /// <param name="item">Item to add to vending machine storage</param>
        /// <returns>Returns 1 if successful, 0 if the item is already in storage, -1 if there was error</returns>
        int AddStorageItem(StorageItem item);

        /// <summary>
        /// Updates storage item
        /// </summary>
        /// <param name="storageItem">storage item update entity</param>
        /// <returns>Returns 1 if successful, 0 if the item is not found, -1 if there was error</returns>
        int UpdateStorageItem(StorageItem item);

        /// <summary>
        /// Deletes storage item
        /// </summary>
        /// <param name="storageItem">storage item update entity</param>
        /// <returns>Returns 1 if successful, 0 if the item is not found, -1 if there was error</returns>
        int DeleteStorageItem(StorageItem item);

        /// <summary>
        /// Imports new storage items and deletes storage items with ids that are in the passed collection
        /// </summary>
        /// <param name="newStorageItemsList">Collection of new storage items </param>
        void ImportAndUpdatePassedStorageItems(List<StorageItem> newStorageItemsList);

        #endregion
    }
}

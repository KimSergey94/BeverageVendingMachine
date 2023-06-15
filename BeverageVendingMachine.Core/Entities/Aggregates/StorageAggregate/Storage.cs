using BeverageVendingMachine.Core.Common;
using BeverageVendingMachine.Core.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeverageVendingMachine.Core.Entities.Aggregates.StorageAggregate
{
    /// <summary>
    /// Represents vending machine physical storage. Implementation of storage operations
    /// </summary>
    public class Storage : IAggregateRoot
    {
        public Storage() { }
        public Storage(SortedDictionary<double, List<CoinDenomination>> coins, SortedDictionary<double, List<CoinDenomination>> depositedCoins, List<IStorageItem> items)
        {
            Coins = coins;
            DepositedCoins = depositedCoins;
            StorageItems = items;
        }


        #region Fields

        /// <summary>
        /// The total deposited amount from all deposited coins
        /// </summary>
        public double DepositedAmount
        {
            get
            {
                var result = 0.00;
                foreach (var depositedCoins in DepositedCoins)
                    result += depositedCoins.Key * depositedCoins.Value.Count;
                return result;
            }
        }

        /// <summary>
        /// Dictionary with coin denomination as a key and the collection of coins with such denomination as a value that are contained inside the vending machine storage 
        /// </summary>
        public SortedDictionary<double, List<CoinDenomination>> Coins { get; } = new SortedDictionary<double, List<CoinDenomination>>();


        /// <summary>
        /// Dictionary with coin denomination as a key and the collection of coins with such denomination deposited to the vending machine storage as a value
        /// </summary>
        public SortedDictionary<double, List<CoinDenomination>> DepositedCoins { get; } = new SortedDictionary<double, List<CoinDenomination>>();

        /// <summary>
        /// Represents items inside vending machine storage
        /// </summary>
        public List<IStorageItem> StorageItems { get; set; } = new List<IStorageItem>();

        #endregion


        #region Coins operations

        /// <summary>
        /// To deposit a coin to a vending machine temporary storage for a purchase
        /// </summary>
        /// <param name="coin">Coin you want to deposit</param>
        public void DepositCoin(CoinDenomination coin)
        {
            if (DepositedCoins[coin.Value] == null)
                DepositedCoins[coin.Value] = new List<CoinDenomination>() { coin };
            else DepositedCoins[coin.Value].Add(coin);
        }

        /// <summary>
        /// To deposit coins to a vending machine storage
        /// </summary>
        /// <param name="coins">Coins you want to deposit</param>
        public void DepositCoins(double coinDenomination, List<CoinDenomination> coins)
        {
            if (DepositedCoins[coinDenomination] == null)
                DepositedCoins[coinDenomination] = new List<CoinDenomination>(coins);
            else DepositedCoins[coinDenomination].AddRange(coins);
        }

        /// <summary>
        /// Moves deposited coins to primary storage
        /// </summary>
        public void TakeDepositedCoins()
        {
            foreach (var coinDenominationGroup in DepositedCoins)
                Coins.Add(coinDenominationGroup.Key, coinDenominationGroup.Value);
            DepositedCoins.Clear();
        }


        #region Take coins by amount operations

        /// <summary>
        /// Takes passed amount from deposited coins 
        /// </summary>
        /// <param name="purchaseItemCost"></param>
        public void TakePurchasedItemCostFromDepositedCoins(double purchaseItemCost)
        {
            try
            {
                GetDepositedCoins(purchaseItemCost);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Provides dictionary with coin denomination as a key and the collection of coins with such denomination as a value to take from deposited coins
        /// </summary>
        /// <param name="amount">The amount needed to be taken</param>
        /// <returns>The dictionary with coin denomination as a key and the collection of coins with such denomination as a value from deposited coins</returns>
        public SortedDictionary<double, List<CoinDenomination>> GetDepositedCoins(double amount)
        {
            try
            {
                return TakeAmountFromCoinsCollection(DepositedCoins, amount);
            }
            catch
            {
                throw new Exception($"Not enough deposited coins in the amount of {amount}");
            }
        }

        /// <summary>
        /// Provides dictionary with coin denomination as a key and the collection of coins with such denomination as a value for a change to a customer
        /// </summary>
        /// <param name="amount">The change amount needed to be returned to a user</param>
        /// <returns>The dictionary with coin denomination as a key and the collection of coins with such denomination as a value to provide to user</returns>
        public SortedDictionary<double, List<CoinDenomination>> GetCoinsForChange(double amount)
        {
            try
            {
                return TakeAmountFromCoinsCollection(Coins, amount);
            }
            catch
            {
                throw new Exception($"Not enough coins for change in the amount of {amount}");
            }
        }

        /// <summary>
        /// Takes provided amount from the provided coins collection
        /// </summary>
        /// <param name="coins">Collection of coins from which to take coins</param>
        /// <param name="amount">The amount needed to be taken from coins</param>
        /// <returns>Taken amount in coins representation</returns>
        private SortedDictionary<double, List<CoinDenomination>> TakeAmountFromCoinsCollection(SortedDictionary<double, List<CoinDenomination>> coins, double amount)
        {
            var result = new SortedDictionary<double, List<CoinDenomination>>();
            foreach (var coinDenominationGroup in coins.OrderByDescending(x => x.Key))
            {
                var takenCoins = TakeMaxCoinsByDenomination(coins, coinDenominationGroup.Key, amount);
                amount = amount - takenCoins.Count * coinDenominationGroup.Key;
                result.Add(coinDenominationGroup.Key, takenCoins);
                coins[coinDenominationGroup.Key] = coins[coinDenominationGroup.Key].Except(takenCoins).ToList();
            }

            if (amount > 0)
            {
                foreach (var coinDenominationGroup in result) DepositCoinsToCollection(coins, coinDenominationGroup.Key, coinDenominationGroup.Value);

                throw new Exception($"Not enough coins in the amount of {amount}");
            }
            else return result;
        }

        /// <summary>
        /// Takes max coins from the provided collection by denomination
        /// </summary>
        /// <param name="coins">Collection of coins from which to take coins by denomination</param>
        /// <param name="coinDenomination">coin denomination</param>
        /// <param name="amount">The total amount left</param>
        /// <returns>Returns taken coins from the provided collection</returns>
        private List<CoinDenomination> TakeMaxCoinsByDenomination(IDictionary<double, List<CoinDenomination>> coins, double coinDenomination, double amount)
        {
            var totalCoins = coins[coinDenomination].Count;
            var coinsNeeded = amount / coinDenomination;
            //needs to be checked

            var takenCoins = totalCoins >= coinsNeeded ? coins[coinDenomination].Take((int)coinsNeeded).ToList() : coins[coinDenomination].Take(totalCoins).ToList();
            coins[coinDenomination] = Coins[coinDenomination].Except(takenCoins).ToList();
            return takenCoins;
        }

        /// <summary>
        /// To deposit coins to a provided collection
        /// </summary>
        /// <param name="coins">Coins you want to deposit</param>
        private void DepositCoinsToCollection(SortedDictionary<double, List<CoinDenomination>> coinsCollection, double coinDenomination, List<CoinDenomination> coins)
        {
            if (coinsCollection[coinDenomination] == null)
                coinsCollection[coinDenomination] = new List<CoinDenomination>(coins);
            else coinsCollection[coinDenomination].AddRange(coins);
        }

        #endregion

        #endregion


        #region Storage items operations

        /// <summary>
        /// Takes all items from vending machine storage
        /// </summary>
        /// <returns>Returns all storage items</returns>
        public List<IStorageItem> TakeAllItemsFromStorage()
        {
            var result = StorageItems.Take(StorageItems.Count).ToList();
            StorageItems = StorageItems.Except(result).ToList();
            return result;
        }

        /// <summary>
        /// Takes item from vending machine storage
        /// </summary>
        /// <param name="item">Item from vending machine storage</param>
        /// <returns>Returns taken item from storage</returns>
        /// <exception cref="Exception">Not found purchase item</exception>
        public IStorageItem TakeItemFromStorageItems(IStorageItem item)
        {
            var storageItem = StorageItems.FirstOrDefault(x => x.Id == item.Id);
            if (storageItem == null) throw new Exception($"Not found purchase item with an Id: {item.Id}");

            return storageItem;
        }

        /// <summary>
        /// Adds passed item to vending machine storage
        /// </summary>
        /// <param name="item">Item to add to vending machine storage</param>
        /// <returns>Returns 1 if successful, 0 if the item is already in storage, -1 if there was error</returns>
        public int AddStorageItem(IStorageItem item)
        {
            var result = 1;
            if (!StorageItems.Contains(item)) StorageItems.Add(item);
            else result = 0;
            return result;
        }

        /// <summary>
        /// Updates storage item
        /// </summary>
        /// <param name="storageItem">storage item update entity</param>
        /// <returns>Returns 1 if successful, 0 if the item is not found, -1 if there was error</returns>
        public int UpdateStorageItem(IStorageItem item)
        {
            var result = 1;
            var storageItem = StorageItems.FirstOrDefault(storageItem => storageItem.Id == item.Id);
                
            if (storageItem == null) result = 0;
            else
            {
                storageItem.Name = item.Name;
                storageItem.Quantity = item.Quantity;
                storageItem.ImageUrl = item.ImageUrl;
                storageItem.Cost = item.Cost;
            }
            return result;
        }

        /// <summary>
        /// Deletes storage item
        /// </summary>
        /// <param name="storageItem">storage item update entity</param>
        /// <returns>Returns 1 if successful, 0 if the item is not found, -1 if there was error</returns>
        public int DeleteStorageItem(IStorageItem item)
        {
            var result = 1;
            var storageItem = StorageItems.FirstOrDefault(storageItem => storageItem.Id == item.Id);

            if (storageItem == null) result = 0;
            else StorageItems.Remove(storageItem);

            return result;
        }

        /// <summary>
        /// Imports new storage items and deletes storage items with ids that are in the passed collection
        /// </summary>
        /// <param name="newStorageItemsList">Collection of new storage items </param>
        public void ImportAndUpdatePassedStorageItems(List<IStorageItem> newStorageItemsList)
        {
            var newStorageItemsIds = newStorageItemsList.Select(storageItem => storageItem.Id).ToList();

            //needs to be checked
            StorageItems.RemoveAll(storageItem => newStorageItemsIds.Contains(storageItem.Id));

            foreach (var newStorageItem in newStorageItemsList) 
                StorageItems.Add(newStorageItem);
        }
        #endregion
    }
}

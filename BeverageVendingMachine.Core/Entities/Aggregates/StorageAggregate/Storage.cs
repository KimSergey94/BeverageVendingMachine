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
    /// Represents vending machine storage. Implementation of storage operations
    /// </summary>
    public class Storage : IAggregateRoot
    {
        public Storage() { }
        public Storage(SortedDictionary<double, List<CoinDenomination>> coins, SortedDictionary<double, List<CoinDenomination>> depositedCoins, List<IStorageItem> items)
        {
            Coins = coins;
            DepositedCoins = depositedCoins;
            InventoryItems = items;
        }


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
        //public Dictionary<double, List<Coin>> Coins { get; } = new Dictionary<double, List<Coin>>();
        public SortedDictionary<double, List<CoinDenomination>> Coins { get; } = new SortedDictionary<double, List<CoinDenomination>>();


        /// <summary>
        /// Dictionary with coin denomination as a key and the collection of coins with such denomination deposited to the vending machine storage as a value
        /// </summary>
        public SortedDictionary<double, List<CoinDenomination>> DepositedCoins { get; } = new SortedDictionary<double, List<CoinDenomination>>();

        /// <summary>
        /// Represents items inside vending machine storage
        /// </summary>
        public List<IStorageItem> InventoryItems { get; set; } = new List<IStorageItem>();


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
        /// To deposit coins to a provided collection
        /// </summary>
        /// <param name="coins">Coins you want to deposit</param>
        private void DepositCoinsToCollection(SortedDictionary<double, List<CoinDenomination>> coinsCollection, double coinDenomination, List<CoinDenomination> coins)
        {
            if (coinsCollection[coinDenomination] == null)
                coinsCollection[coinDenomination] = new List<CoinDenomination>(coins);
            else coinsCollection[coinDenomination].AddRange(coins);
        }

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
        /// Moves deposited coins to primary storage
        /// </summary>
        public void TakeDepositedCoins()
        {
            foreach (var coinDenominationGroup in DepositedCoins)
                Coins.Add(coinDenominationGroup.Key, coinDenominationGroup.Value);
            DepositedCoins.Clear();
        }


        /// <summary>
        /// Releases purchase item from vending machine storage
        /// </summary>
        /// <param name="SelectedItem">Selected item from vending machine storage</param>
        /// <returns>Return released selected item from vending machine storage</returns>
        /// <exception cref="Exception">Not found purchase item</exception>
        public IStorageItem TakePurchaseItemFromInventoryItems(IStorageItem purchaseItem)
        {
            if (InventoryItems.Contains(purchaseItem)) InventoryItems.Remove(purchaseItem);
            else throw new Exception($"Not found purchase item with an Id: {purchaseItem.Id}");

            //needs to be checked
            return purchaseItem;
        }

        /// <summary>
        /// Releases purchase item from vending machine storage
        /// </summary>
        /// <param name="SelectedItem">Selected item from vending machine storage</param>
        /// <returns>Return released selected item from vending machine storage</returns>
        /// <exception cref="Exception">Not found purchase item</exception>
        public void AddPurchaseItemToInventoryItems(IStorageItem purchaseItem)
        {
            if (!InventoryItems.Contains(purchaseItem)) InventoryItems.Add(purchaseItem);
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
            catch (Exception e)
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
            catch (Exception e)
            {
                throw new Exception($"Not enough coins for change in the amount of {amount}");
            }
        }

        /// <summary>
        /// Takes provided amount from the provided coins collection
        /// </summary>
        /// <param name="coins">Collection of coins from which to take coins</param>
        /// <param name="amount">The amount needed to be taken from coins</param>
        /// <returns></returns>
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

        #region TakeMaxCoinsByDenomination
        ///// <summary>
        ///// Takes maximum coins from deposited coin with the passed coin denomination
        ///// </summary>
        ///// <param name="coinDenomination">coin denomination to get coins with</param>
        ///// <param name="amount">the amount to take from deposited coins</param>
        ///// <returns>Returns the coins taken</returns>
        //private List<CoinDenomination> TakeDepositedCoinsByDenomination(double coinDenomination, double amount)
        //{
        //    return TakeMaxCoinsByDenomination(DepositedCoins, coinDenomination, amount);
        //}

        ///// <summary>
        ///// Takes maximum coins from storage with the passed coin denomination
        ///// </summary>
        ///// <param name="coinDenomination">coin denomination to get change</param>
        ///// <param name="change">the change left to collect</param>
        ///// <returns>returns the coins for a change</returns>
        //private List<CoinDenomination> TakeStorageCoinsByDenomination(double coinDenomination, double change)
        //{
        //    return TakeMaxCoinsByDenomination(Coins, coinDenomination, change);
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="coins">Collection of coins from which to take coins by denomination</param>
        /// <param name="coinDenomination">coin denomination</param>
        /// <param name="amount">The total amount left</param>
        /// <returns></returns>
        private List<CoinDenomination> TakeMaxCoinsByDenomination(IDictionary<double, List<CoinDenomination>> coins, double coinDenomination, double amount)
        {
            var totalCoins = coins[coinDenomination].Count;
            var coinsNeeded = amount / coinDenomination;
            //needs to be checked

            var takenCoins = totalCoins >= coinsNeeded ? coins[coinDenomination].Take((int)coinsNeeded).ToList() : coins[coinDenomination].Take(totalCoins).ToList();
            coins[coinDenomination] = Coins[coinDenomination].Except(takenCoins).ToList();
            return takenCoins;
        }
        #endregion
    }
}

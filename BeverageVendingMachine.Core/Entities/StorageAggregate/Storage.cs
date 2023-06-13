using BeverageVendingMachine.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeverageVendingMachine.Core.Entities.StorageAggregate
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
        /// To move deposited coins to primary storage
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
        public IStorageItem TakePurchaseItemFromInventoryItems(IStorageItem PurchaseItem)
        {
            if (InventoryItems.Contains(PurchaseItem)) InventoryItems.Remove(PurchaseItem);
            //else { }

            //needs to be checked
            return PurchaseItem;
        }

        /// <summary>
        /// Provides dictionary with coin denomination as a key and the collection of coins with such denomination as a value. P.S. Can be improved later by calculating the number of coins contained
        /// </summary>
        /// <param name="change">The change amount needed to be returned to a user</param>
        /// <returns>The dictionary with coin denomination as a key and the collection of coins with such denomination as a value to provide to user</returns>
        public SortedDictionary<double, List<CoinDenomination>> GetCoinsForChange(double change)
        {
            var result = new SortedDictionary<double, List<CoinDenomination>>();
            foreach (var coinDenominationGroup in Coins.OrderByDescending(x => x.Key))
            {
                var storageCoins = TakeStorageCoinsByDenomination(coinDenominationGroup.Key, change);
                change = change - storageCoins.Count * coinDenominationGroup.Key;
                result.Add(coinDenominationGroup.Key, storageCoins);
                Coins[coinDenominationGroup.Key] = Coins[coinDenominationGroup.Key].Except(storageCoins).ToList();
            }

            if (change > 0)
            {
                foreach (var coinDenominationGroup in result) DepositCoins(coinDenominationGroup.Key, coinDenominationGroup.Value);

                throw new Exception($"Not enough coins for change the amount of {change}");
            }
            else return result;
        }

        /// <summary>
        /// Takes maximum coins from storage with the passed coin denomination
        /// </summary>
        /// <param name="coinDenomination">coin denomination to get change</param>
        /// <param name="change">the change left to collect</param>
        /// <returns>returns the coins for a change</returns>
        private List<CoinDenomination> TakeStorageCoinsByDenomination(double coinDenomination, double change)
        {
            var totalCoins = Coins[coinDenomination].Count;
            var coinsNeeded = change / coinDenomination;
            //needs to be checked

            if (totalCoins >= coinsNeeded) return Coins[coinDenomination].Take((int)coinsNeeded).ToList();
            else return Coins[coinDenomination].Take(totalCoins).ToList();
        }
    }
}

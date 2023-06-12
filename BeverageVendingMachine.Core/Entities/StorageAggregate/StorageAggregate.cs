using BeverageVendingMachine.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeverageVendingMachine.Core.Entities.StorageAggregate
{
    /// <summary>
    /// Represents vending machine storage. Realisation of storage operations
    /// </summary>
    public class StorageAggregate  : IAggregateRoot
    {
        public StorageAggregate() { }
        public StorageAggregate(SortedDictionary<double, List<Coin>> coins, SortedDictionary<double, List<Coin>> depositedCoins, List<IStorageItem> items)
        {
            Coins = coins;
            DepositedCoins = depositedCoins;
            Items = items;
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
        public SortedDictionary<double, List<Coin>> Coins { get; } = new SortedDictionary<double, List<Coin>>();

        
        /// <summary>
        /// Dictionary with coin denomination as a key and the collection of coins with such denomination deposited to the vending machine storage as a value
        /// </summary>
        public SortedDictionary<double, List<Coin>> DepositedCoins { get; } = new SortedDictionary<double, List<Coin>>();

        /// <summary>
        /// Represents items inside vending machine storage
        /// </summary>
        public List<IStorageItem> Items { get; set; } = new List<IStorageItem>();


        /// <summary>
        /// To deposit a coin to a vending machine temporary storage
        /// </summary>
        /// <param name="coin">Coin you want to deposit</param>
        public void DepositCoin(Coin coin)
        {
            if (DepositedCoins[coin.Denomination] == null)
                DepositedCoins[coin.Denomination] = new List<Coin>() { coin };
            else DepositedCoins[coin.Denomination].Add(coin);
        }

        /// <summary>
        /// To deposit coins to a vending machine storage
        /// </summary>
        /// <param name="coins">Coins you want to deposit</param>
        public void DepositCoins(double coinDenomination, List<Coin> coins)
        {
            if (DepositedCoins[coinDenomination] == null)
                DepositedCoins[coinDenomination] = new List<Coin>(coins);
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
        /// Releases selected item from vending machine storage
        /// </summary>
        /// <param name="SelectedItem">Selected item from vending machine storage</param>
        /// <returns>Return released selected item from vending machine storage</returns>
        public IStorageItem ReleaseSelectedItem(IStorageItem SelectedItem)
        {
            if (Items.Contains(SelectedItem)) Items.Remove(SelectedItem);

            //needs to be checked
            return SelectedItem;
        }

        /// <summary>
        /// Provides dictionary with coin denomination as a key and the collection of coins with such denomination as a value. P.S. Can be improved later by calculating the number of coins contained
        /// </summary>
        /// <param name="change">The change amount needed to be returned to a user</param>
        /// <returns>The dictionary with coin denomination as a key and the collection of coins with such denomination as a value to provide to user</returns>
        public SortedDictionary<double, List<Coin>> GetCoinsForChange(double change)
        {
            var result = new SortedDictionary<double, List<Coin>>();
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
        private List<Coin> TakeStorageCoinsByDenomination(double coinDenomination, double change)
        {
            var totalCoins = Coins[coinDenomination].Count;
            var coinsNeeded = change / coinDenomination;
            //needs to be checked

            if (totalCoins >= coinsNeeded) return Coins[coinDenomination].Take((int)coinsNeeded).ToList();
            else return Coins[coinDenomination].Take(totalCoins).ToList();
        }
    }
}

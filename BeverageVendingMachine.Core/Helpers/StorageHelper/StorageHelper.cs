using BeverageVendingMachine.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeverageVendingMachine.Core.Helpers.StorageHelper
{
    /// <summary>
    /// Represents vending machine storage. Realisation of storage operations
    /// </summary>
    public class StorageHelper : IStorageHelper
    {
        public StorageHelper() { }
        public StorageHelper(Dictionary<int, List<Coin>> coins, Dictionary<int, List<Coin>> depositedCoins, List<IStorageItem> items)
        {
            Coins = coins;
            DepositedCoins = depositedCoins;
            Items = items;
        }


        /// <summary>
        /// The total deposited amount from all deposited coins
        /// </summary>
        public int DepositedAmount
        {
            get
            {
                var result = 0;
                foreach (var depositedCoins in DepositedCoins)
                    result += depositedCoins.Key * depositedCoins.Value.Count;
                return result;
            }
        }

        /// <summary>
        /// Dictionary with coin denomination as a key and the collection of coins with such denomination as a value that are contained inside the vending machine storage 
        /// </summary>
        public Dictionary<int, List<Coin>> Coins { get; } = new Dictionary<int, List<Coin>>();

        /// <summary>
        /// Dictionary with coin denomination as a key and the collection of coins with such denomination deposited to the vending machine storage as a value
        /// </summary>
        public Dictionary<int, List<Coin>> DepositedCoins { get; } = new Dictionary<int, List<Coin>>();

        /// <summary>
        /// Represents items inside vending machine storage
        /// </summary>
        public List<IStorageItem> Items { get; set; } = new List<IStorageItem>();


        /// <summary>
        /// To deposit a coin to a vending machine storage
        /// </summary>
        /// <param name="coin">Coin you want to deposit</param>
        public void DepositCoin(Coin coin)
        {
            if (DepositedCoins[coin.Denomination] == null)
                DepositedCoins[coin.Denomination] = new List<Coin>() { coin };
            else DepositedCoins[coin.Denomination].Add(coin);
        }

        /// <summary>
        /// To clear deposited coins (after purchase)
        /// </summary>
        public void ClearDepositedCoins()
        {
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
        /// Provides dictionary with coin denomination as a key and the collection of coins with such denomination as a value
        /// </summary>
        /// <param name="change">The change amount needed to be returned to a user</param>
        /// <returns>The dictionary with coin denomination as a key and the collection of coins with such denomination as a value to provide to user</returns>
        public Dictionary<int, List<Coin>> GetCoinsForChange(int change)
        {
            throw new NotImplementedException();
        }
    }
}

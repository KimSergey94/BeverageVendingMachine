using BeverageVendingMachine.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeverageVendingMachine.Core.Entities.StorageAggregate
{
    /// <summary>
    /// Represents vending machine storage
    /// </summary>
    public class Storage : IStorage
    {
        public Storage() { }
        public Storage(Dictionary<int, List<Coin>> depositedCoins, List<IStorageItem> items)
        {
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
        /// Dictionary with coin denomination key and the collection of coins with such denomination deposited to the vending machine storage 
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
            if(Items.Contains(SelectedItem)) Items.Remove(SelectedItem);

            //needs to be checked
            return SelectedItem;
        }
    }   
}

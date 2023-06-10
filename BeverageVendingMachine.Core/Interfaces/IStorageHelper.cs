using BeverageVendingMachine.Core.Entities;
using BeverageVendingMachine.Core.Helpers.StorageHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeverageVendingMachine.Core.Interfaces
{
    /// <summary>
    /// Represents vending machine storage interface. Interface of storage operations
    /// </summary>
    public interface IStorageHelper
    {
        /// <summary>
        /// The total deposited amount from all deposited coins
        /// </summary>
        int DepositedAmount { get; }

        /// <summary>
        /// Dictionary with coin denomination as a key and the collection of coins with such denomination deposited to the vending machine storage as a value
        /// </summary>
        Dictionary<int, List<Coin>> DepositedCoins { get; }

        /// <summary>
        /// Dictionary with coin denomination as a key and the collection of coins with such denomination as a value that are contained inside the vending machine storage 
        /// </summary>
        Dictionary<int, List<Coin>> Coins { get; }

        /// <summary>
        /// Represents items inside vending machine storage
        /// </summary>
        List<IStorageItem> Items { get; set; }

        /// <summary>
        /// To deposit a coin to a vending machine storage
        /// </summary>
        /// <param name="coin">Coin you want to deposit</param>
        void DepositCoin(Coin coin);

        /// <summary>
        /// To clear deposited coins (after purchase)
        /// </summary>
        void ClearDepositedCoins();

        /// <summary>
        /// Releases selected item from vending machine storage
        /// </summary>
        /// <param name="SelectedItem">Selected item from vending machine storage</param>
        /// <returns>Return released selected item from vending machine storage</returns>
        IStorageItem ReleaseSelectedItem(IStorageItem SelectedItem);

        /// <summary>
        /// Provides dictionary with coin denomination as a key and the collection of coins with such denomination as a value
        /// </summary>
        Dictionary<int, List<Coin>> GetCoinsForChange(int change);

    }
}

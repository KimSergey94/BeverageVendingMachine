using BeverageVendingMachine.Core.Entities;
using BeverageVendingMachine.Core.Entities.StorageAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeverageVendingMachine.Core.Interfaces
{
    /// <summary>
    /// Represents vending machine storage interface
    /// </summary>
    public interface IStorage
    {
        /// <summary>
        /// The total deposited amount from all deposited coins
        /// </summary>
        int DepositedAmount { get; }

        /// <summary>
        /// Dictionary with coin denomination key and the collection of coins with such denomination deposited to the vending machine storage 
        /// </summary>
        Dictionary<int, List<Coin>> DepositedCoins { get; }

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
    }
}

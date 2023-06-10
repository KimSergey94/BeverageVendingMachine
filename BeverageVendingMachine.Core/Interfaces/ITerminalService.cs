using BeverageVendingMachine.Core.Entities.StorageAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeverageVendingMachine.Core.Interfaces
{
    /// <summary>
    /// Interface for a service to imitate terminal to operate with the vending machine
    /// </summary>
    public interface ITerminalService
    {
        /// <summary>
        /// Selects item for a puchase
        /// </summary>
        /// <param name="selectedItem">Selected storage item</param>
        void SelectItem(IStorageItem selectedItem);

        /// <summary>
        /// Unselects item for a puchase
        /// </summary>
        void UnselectItem();

        /// <summary>
        /// Calculates change by subtracting selected item cost from the deposited amount
        /// </summary>
        /// <returns>Returns amount to be returned to a vending machine user</returns>
        int CalculateChange();

        /// <summary>
        /// Returns the deposited amount
        /// </summary>
        /// <returns>deposited amount</returns>
        int GetDepositedAmount();


        /// <summary>
        /// Blocks passed coin denomination by Id
        /// </summary>
        /// <param name="coinDenominationId">Id of the coin denomination to block</param>
        void BlockCoinDenomination(int coinDenominationId);

        /// <summary>
        /// Unblock passed coin denomination by Id
        /// </summary>
        /// <param name="coinDenominationId">Id of the coin denomination to unblock</param>
        void UnblockCoinDenomination(int coinDenominationId);

        /// <summary>
        /// Releases selected storage item
        /// </summary>
        /// <returns>StorageItem type selectedItem</returns>
        IStorageItem ReleaseSelectedItem();

    }
}

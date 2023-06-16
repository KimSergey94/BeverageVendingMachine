using BeverageVendingMachine.Application.DTOs;
using BeverageVendingMachine.Core.Entities.Aggregates.StorageAggregate;
using BeverageVendingMachine.Core.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeverageVendingMachine.Core.Interfaces.Services
{
    /// <summary>
    /// Interface for a service to imitate terminal (operating system) to operate with the vending machine
    /// </summary>
    public interface ITerminalService
    {
        #region Admin interface

        /// <summary>
        /// Blocks passed coin denomination by Id
        /// </summary>
        /// <param name="coinDenominationId">Id of the coin denomination to block</param>
        /// <returns>Returns 1 if successful, 0 if the coin is already blocked, -1 if there was error</returns>
        int BlockCoinDenomination(int coinDenominationId);

        /// <summary>
        /// Unblock passed coin denomination by Id
        /// </summary>
        /// <param name="coinDenominationId">Id of the coin denomination to unblock</param>
        /// <returns>Returns 1 if successful, 0 if the coin is already unblocked, -1 if there was error</returns>
        int UnblockCoinDenomination(int coinDenominationId);

        #endregion


        #region User interface

        /// <summary>
        /// To deposit a coin to a vending machine temporary storage for a purchase
        /// </summary>
        /// <param name="coin">Coin you want to deposit</param>
        void DepositCoin(CoinDenomination coin);

        /// <summary>
        /// Selects item for a puchase
        /// </summary>
        /// <param name="purchaseItem">Selected purchase item</param>
        void SelectPurchaseItem(IStorageItem purchaseItem);

        /// <summary>
        /// Unselects item for a puchase
        /// </summary>
        void UnselectPurchaseItem();

        /// <summary>
        /// Calculates change by subtracting selected item cost from the deposited amount
        /// </summary>
        /// <returns>Returns amount to be returned to a vending machine user</returns>
        decimal CalculateChange();

        /// <summary>
        /// Returns the amount of deposited coins for a purchase
        /// </summary>
        /// <returns>the amount of deposited coins for a purchase</returns>
        decimal GetDepositedAmount();

        /// <summary>
        /// Takes purchase item from inventory
        /// </summary>
        /// <returns>Returns purchase item from inventory</returns>
        IStorageItem TakePurchaseItemFromInventory();

        /// <summary>
        /// Releases change
        /// </summary>
        /// <returns>Returns change for a customer</returns>
        Task<SortedDictionary<decimal, List<CoinDenomination>>> ReleaseChange();

        /// <summary>
        /// Releases purchase item and change
        /// </summary>
        /// <returns>Returns an object with the purchase item and change inside</returns>
        Task<PurchaseResult> ReleasePurchaseItemAndChange();

        #endregion
    }
}

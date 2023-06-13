using BeverageVendingMachine.Application.DTOs;
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
        double CalculateChange();

        /// <summary>
        /// Returns the amount of deposited coins for a purchase
        /// </summary>
        /// <returns>the amount of deposited coins for a purchase</returns>
        double GetDepositedAmount();

        /// <summary>
        /// Releases purchase item and change
        /// </summary>
        /// <returns>Returns what should be returned to a customer (purchase item and change)</returns>
        Task<PurchaseResult> ReleaseSelectedItemAndChange();


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
    }
}

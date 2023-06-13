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
    /// Service interface to make purchases by clients
    /// </summary>
    public interface IPurchaseService
    {
        /// <summary>
        /// Deposits a coin to make purchase
        /// </summary>
        /// <param name="coin">Coin denomination entity</param>
        void DepositCoin(CoinDenomination coin);

        /// <summary>
        /// Selects an item from inventory for a purchase
        /// </summary>
        /// <param name="purchaseItem">Inventory item to be selected for a purchase</param>
        void SelectPurchaseItem(IStorageItem purchaseItem);

        /// <summary>
        /// Returns object with purchase result items
        /// </summary>
        /// <returns>Purchased item and change</returns>
        Task<PurchaseResult> GetPurchaseResult(double change);
    }
}

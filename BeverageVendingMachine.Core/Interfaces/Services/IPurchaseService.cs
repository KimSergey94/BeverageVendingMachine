using BeverageVendingMachine.Application.DTOs;
using BeverageVendingMachine.Core.DTOs;
using BeverageVendingMachine.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeverageVendingMachine.Core.Interfaces.Services
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
        /// <param name="purchaseItemId">Id of the selected purchase item</param>
        void SelectPurchaseItem(int purchaseItemId);

        /// <summary>
        /// Returns object with purchase result items
        /// </summary>
        /// <returns>Purchased item and change</returns>
        Task<PurchaseResult> ReleasePurchaseItemAndChange();

        /// <summary>
        /// Takes purchase item from inventory
        /// </summary>
        /// <returns>Returns purchased item from inventory</returns>
        StorageItem ReleasePurchaseItem();

        /// <summary>
        /// Gets unused coin from deposited coins
        /// </summary>
        /// <returns>Returns change</returns>
        Task<CoinsCollection> ReleaseChange();
    }
}

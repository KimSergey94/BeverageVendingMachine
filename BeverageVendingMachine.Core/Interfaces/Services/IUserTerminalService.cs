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
    /// Service interface to interact with terminal by clients
    /// </summary>
    public interface IUserTerminalService
    {
        /// <summary>
        /// Gets update data from the vending machine terminal
        /// </summary>
        /// <returns>Update data from the vending machine terminal</returns>
        UpdateData GetUpdateData();

        /// <summary>
        /// To deposit a coin to a vending machine temporary storage for a purchase
        /// </summary>
        /// <param name="coinDenominationId">Id of the coin denomination you want to deposit</param>
        /// <returns>Update data for vending machine terminal</returns>
        Task<UpdateData> DepositCoin(int coinDenominationId);

        /// <summary>
        /// Selects item for a puchase
        /// </summary>
        /// <param name="purchaseItemId">Id of the selected purchase item</param>
        /// <returns>Update data for vending machine terminal</returns>
        Task<UpdateData> SelectPurchaseItem(int purchaseItemId);

        /// <summary>
        /// Unselects item for a puchase
        /// </summary>
        /// <returns>Update data for vending machine terminal</returns>
        Task<UpdateData> UnselectPurchaseItem();

        /// <summary>
        /// Releases purchase item and change
        /// </summary>
        /// <returns>Returns an object with the purchase item and change inside</returns>
        Task<PurchaseResult> ReleasePurchaseItemAndChange();

        /// <summary>
        /// Takes purchase item from inventory
        /// </summary>
        /// <returns>Returns purchase item from inventory</returns>
        Task<StorageItem> TakePurchaseItemFromInventory();

        /// <summary>
        /// Releases change
        /// </summary>
        /// <returns>Returns coins collection for a customer</returns>
        Task<CoinsCollection> ReleaseChange();
    }
}

using BeverageVendingMachine.Application.DTOs;
using BeverageVendingMachine.Core.DTOs;
using BeverageVendingMachine.Core.Entities;
using BeverageVendingMachine.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeverageVendingMachine.Core.Services
{
    /// <summary>
    /// Service interface to interact with terminal by clients
    /// </summary>
    public class UserTerminalService : IUserTerminalService
    {
        private readonly ITerminalService _terminalService;
        public UserTerminalService(ITerminalService terminalService)
        {
            _terminalService = terminalService;
        }

        /// <summary>
        /// Gets update data from the vending machine terminal
        /// </summary>
        /// <returns>Update data from the vending machine terminal</returns>
        public UpdateData GetUpdateData()
        {
            return _terminalService.GetTerminalUpdateData();
        }

        /// <summary>
        /// Deposits a coin to make purchase
        /// </summary>
        /// <param name="coin">Coin denomination entity</param>
        public Task<UpdateData> DepositCoin(int coinDenominationId)
        {
            return Task.Run(() => _terminalService.DepositCoin(coinDenominationId));
        }

        /// <summary>
        /// Selects an item from inventory for a purchase
        /// </summary>
        /// <param name="purchaseItem">Inventory item to be selected for a purchase</param>
        public Task<UpdateData> SelectPurchaseItem(int purchaseItemId)
        {
            return Task.Run(() => _terminalService.SelectPurchaseItem(purchaseItemId));
        }

        /// <summary>
        /// Unselects item for a puchase
        /// </summary>
        /// <returns>Update data for vending machine terminal</returns>
        public Task<UpdateData> UnselectPurchaseItem()
        {
            return Task.Run(() => _terminalService.UnselectPurchaseItem());
        }

        /// <summary>
        /// Returns object with purchase result items
        /// </summary>
        /// <returns>Purchased item and change</returns>
        public async Task<PurchaseResult> ReleasePurchaseItemAndChange()
        {
            return await _terminalService.ReleasePurchaseItemAndChange();
        }


        /// <summary>
        /// Takes purchase item from inventory
        /// </summary>
        /// <returns>Returns purchased item from inventory</returns>
        public async Task<StorageItem> TakePurchaseItemFromInventory()
        {
            return await _terminalService.TakePurchaseItemFromInventory();
        }

        /// <summary>
        /// Gets unused coin from deposited coins
        /// </summary>
        /// <returns>Returns change</returns>
        public async Task<CoinsCollection> ReleaseChange()
        {
            return await _terminalService.ReleaseChange();
        }

    }
}

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
    /// Service to make purchases by clients
    /// </summary>
    public class PurchaseService : IPurchaseService
    {
        private readonly ITerminalService _terminalService;
        public PurchaseService(ITerminalService terminalService)
        {
            _terminalService = terminalService;
        }

        /// <summary>
        /// Deposits a coin to make purchase
        /// </summary>
        /// <param name="coin">Coin denomination entity</param>
        public void DepositCoin(CoinDenomination coin)
        {
            _terminalService.DepositCoin(coin.Id);
        }

        /// <summary>
        /// Selects an item from inventory for a purchase
        /// </summary>
        /// <param name="purchaseItem">Inventory item to be selected for a purchase</param>
        public void SelectPurchaseItem(StorageItem purchaseItem)
        {
            _terminalService.SelectPurchaseItem(purchaseItem);
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
        public StorageItem ReleasePurchaseItem()
        {
            return _terminalService.TakePurchaseItemFromInventory();
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

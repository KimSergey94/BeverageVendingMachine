using BeverageVendingMachine.Application.DTOs;
using BeverageVendingMachine.Core.Entities.StorageAggregate;
using BeverageVendingMachine.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeverageVendingMachine.Application.Services
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
            _terminalService.DepositCoin(coin);
        }

        /// <summary>
        /// Selects an item from inventory for a purchase
        /// </summary>
        /// <param name="purchaseItem">Inventory item to be selected for a purchase</param>
        public void SelectPurchaseItem(IStorageItem purchaseItem)
        {
            _terminalService.SelectPurchaseItem(purchaseItem);
        }

        /// <summary>
        /// Returns object with purchase result items
        /// </summary>
        /// <returns>Purchased item and change</returns>
        public async Task<PurchaseResult> GetPurchaseResult(double change)
        {
            return await _terminalService.ReleaseSelectedItemAndChange();
        }
    }
}

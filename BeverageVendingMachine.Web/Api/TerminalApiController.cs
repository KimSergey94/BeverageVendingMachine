using BeverageVendingMachine.Application.DTOs;
using BeverageVendingMachine.Core.Entities;
using BeverageVendingMachine.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeverageVendingMachine.Web.Api
{
    /// <summary>
    /// Controller for interactions from users section by users
    /// </summary>
    public class TerminalApiController : BaseApiController
    {
        private readonly IUserTerminalService _userTerminalService;

        public TerminalApiController(IUserTerminalService userTerminalService)
        {
            _userTerminalService = userTerminalService;
        }

        // GET: api/TerminalApi/GetUpdateData
        [HttpGet]
        public async Task<IActionResult> GetUpdateData()
        {
            return Ok(await Task.Run(() => _userTerminalService.GetUpdateData()));
        }

        // POST: api/TerminalApi/DepositCoin
        [HttpPost]
        public async Task<IActionResult> DepositCoin([FromBody] int coinDenominationId)
        {
            return Ok(await Task.Run(() => _userTerminalService.DepositCoin(coinDenominationId)));
        }

        // POST: api/TerminalApi/SelectPurchaseItem
        [HttpPost]
        public async Task<IActionResult> SelectPurchaseItem([FromBody] int selectedStorageItemId)
        {
            return Ok(await Task.Run(() => _userTerminalService.SelectPurchaseItem(selectedStorageItemId)));
        }

        // POST: api/TerminalApi/UnselectPurchaseItem
        [HttpPost]
        public async Task<IActionResult> UnselectPurchaseItem()
        {
            return Ok(await Task.Run(() => _userTerminalService.UnselectPurchaseItem()));
        }

        // GET: api/TerminalApi/ReleaseChange
        [HttpGet]
        public async Task<IActionResult> ReleaseChange()
        {
            var purchaseResult = new PurchaseResult(null, await _userTerminalService.ReleaseChange());
            return Ok(purchaseResult);
        }

        // GET: api/TerminalApi/MakePurchase
        [HttpGet]
        public async Task<IActionResult> MakePurchase()
        {
            var purchaseResult = new PurchaseResult(await _userTerminalService.TakePurchaseItemFromInventory(), null);
            return Ok(await Task.Run(() => purchaseResult));
        }

        // GET: api/TerminalApi/ReleasePurchaseItemAndChange
        [HttpGet]
        public async Task<IActionResult> ReleasePurchaseItemAndChange()
        {
            return Ok(await Task.Run(() => _userTerminalService.ReleasePurchaseItemAndChange()));
        }
    }
}

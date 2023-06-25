using BeverageVendingMachine.Core.Entities;
using BeverageVendingMachine.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeverageVendingMachine.Web.Api
{
    public class TerminalApiController : BaseApiController
    {
        private readonly ITerminalService _terminalService;

        public TerminalApiController(ITerminalService terminalService)
        {
            _terminalService = terminalService;
        }

        // GET: api/TerminalApi/GetUpdateData
        [HttpGet]
        public async Task<IActionResult> GetUpdateData()
        {
            return Ok(await _terminalService.GetUpdateData());
        }

        // POST: api/TerminalApi/DepositCoin
        [HttpPost]
        public async Task<IActionResult> DepositCoin([FromBody] int coinDenominationId)
        {
            return Ok(await _terminalService.DepositCoin(coinDenominationId));
        }

        // POST: api/TerminalApi/SelectPurchaseItem
        [HttpPost]
        public async Task<IActionResult> SelectPurchaseItem([FromBody] int selectedStorageItemId)
        {
            return Ok(await _terminalService.SelectPurchaseItem(selectedStorageItemId));
        }

        // POST: api/TerminalApi/UnselectPurchaseItem
        [HttpPost]
        public async Task<IActionResult> UnselectPurchaseItem()
        {
            return Ok(await _terminalService.UnselectPurchaseItem());
        }

        // GET: api/TerminalApi/ReleaseChange
        [HttpGet]
        public async Task<IActionResult> ReleaseChange()
        {
            return Ok(await _terminalService.ReleaseChange());
        }
    }
}

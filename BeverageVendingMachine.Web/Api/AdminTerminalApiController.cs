using BeverageVendingMachine.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System;
using System.Threading.Tasks;

namespace BeverageVendingMachine.Web.Api
{
    public class AdminTerminalApiController : BaseApiController
    {
        private readonly IAdminTerminalService _adminTerminalService;

        public AdminTerminalApiController(IAdminTerminalService adminTerminalService)
        {
            _adminTerminalService = adminTerminalService;
        }


        // POST: api/AdminTerminalApi/BlockCoinDenomination
        [HttpPost]
        public async Task<IActionResult> BlockCoinDenomination([FromBody] int coinDenominationId)
        {
            try
            {
                return Ok(await Task.Run(() => _adminTerminalService.BlockCoinDenomination(coinDenominationId)));
            }
            catch (Exception e)
            {
                var innerException = e.InnerException != null ? "; InnerExceptionErrorMessage: " + e.InnerException.Message : "";
                return BadRequest($"{e.Message}{innerException}");
            }
        }

        // POST: api/AdminTerminalApi/UnblockCoinDenomination
        [HttpPost]
        public async Task<IActionResult> UnblockCoinDenomination([FromBody] int coinDenominationId)
        {
            try
            {
                return Ok(await Task.Run(() => _adminTerminalService.UnblockCoinDenomination(coinDenominationId)));
            }
            catch (Exception e)
            {
                var innerException = e.InnerException != null ? "; InnerExceptionErrorMessage: " + e.InnerException.Message : "";
                return BadRequest($"{e.Message}{innerException}");
            }
        }
    }
}

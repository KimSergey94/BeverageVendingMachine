using BeverageVendingMachine.Core.DTOs;
using BeverageVendingMachine.Core.Entities;
using BeverageVendingMachine.Core.Interfaces.Services;
using BeverageVendingMachine.Core.Services;
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

        // POST: api/AdminTerminalApi/AddNewStorageItem
        [HttpPost]
        public async Task<IActionResult> AddNewStorageItem([FromBody] StorageItem newStorageItem)
        {
            try
            {
                return Ok(await Task.Run(() => _adminTerminalService.AddNewStorageItem(newStorageItem)));
            }
            catch (Exception e)
            {
                var innerException = e.InnerException != null ? "; InnerExceptionErrorMessage: " + e.InnerException.Message : "";
                return BadRequest($"{e.Message}{innerException}");
            }
        }

        // POST: api/AdminTerminalApi/UpdateStorageItem
        [HttpPost]
        public async Task<IActionResult> UpdateStorageItem([FromBody] StorageItem storageItem)
        {
            try
            {
                return Ok(await Task.Run(() => _adminTerminalService.UpdateStorageItem(storageItem)));
            }
            catch (Exception e)
            {
                var innerException = e.InnerException != null ? "; InnerExceptionErrorMessage: " + e.InnerException.Message : "";
                return BadRequest($"{e.Message}{innerException}");
            }
        }

        // POST: api/AdminTerminalApi/DeleteStorageItem
        [HttpPost]
        public async Task<IActionResult> DeleteStorageItem([FromBody] int storageItemId)
        {
            try
            {
                return Ok(await Task.Run(() => _adminTerminalService.DeleteStorageItem(storageItemId)));
            }
            catch (Exception e)
            {
                var innerException = e.InnerException != null ? "; InnerExceptionErrorMessage: " + e.InnerException.Message : "";
                return BadRequest($"{e.Message}{innerException}");
            }
        }
        
    }
}

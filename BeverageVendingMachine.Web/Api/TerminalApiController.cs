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


        // GET: api/TerminalApi/GetCoins
        [HttpGet]
        public async Task<IActionResult> GetCoins()
        {
            var coins = await _terminalService.GetCoins();
            return Ok(coins);
        }

        // GET: api/TerminalApi/GetStorageItems
        [HttpGet]
        public async Task<IActionResult> GetStorageItems()
        {
            var storageItems = await _terminalService.GetStorageItems();
            return Ok(storageItems);
        }

        //// GET: api/Projects
        //[HttpGet("{id:int}")]
        //public async Task<IActionResult> GetById(int id)
        //{
        //    var projectSpec = new ProjectByIdWithItemsSpec(id);
        //    var project = await _repository.FirstOrDefaultAsync(projectSpec);
        //    if (project == null)
        //    {
        //        return NotFound();
        //    }

        //    var result = new ProjectDTO
        //    (
        //        id: project.Id,
        //        name: project.Name,
        //        items: new List<ToDoItemDTO>
        //        (
        //            project.Items.Select(i => ToDoItemDTO.FromToDoItem(i)).ToList()
        //        )
        //    );

        //    return Ok(result);
        //}

        // POST: api/TerminalApi/DepositCoin
        [HttpPost]
        public async Task<IActionResult> DepositCoin([FromBody] int coinDenominationId)
        {
            var updateData = await Task.Run( ()=> _terminalService.DepositCoin(coinDenominationId));
            return Ok(updateData);
        }
    }
}

using BeverageVendingMachine.Core.Interfaces.Services;
using Microsoft.CodeAnalysis;

namespace BeverageVendingMachine.Web.Api
{
    public class TerminalApiController : BaseApiController
    {
        private readonly ITerminalService _terminalService;

        public TerminalApiController(ITerminalService terminalService)
        {
            _terminalService = terminalService;
        }


    }
}

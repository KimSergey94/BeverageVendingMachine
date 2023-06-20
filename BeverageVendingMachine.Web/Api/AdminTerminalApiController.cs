using BeverageVendingMachine.Core.Interfaces.Services;
using Microsoft.CodeAnalysis;

namespace BeverageVendingMachine.Web.Api
{
    public class AdminTerminalApiController : BaseApiController
    {
        private readonly IAdminTerminalService _adminTerminalService;
        private readonly ITerminalService _terminalService;

        public AdminTerminalApiController(IAdminTerminalService adminTerminalService, ITerminalService terminalService)
        {
            _adminTerminalService = adminTerminalService;
            _terminalService = terminalService;
        }
    }
}

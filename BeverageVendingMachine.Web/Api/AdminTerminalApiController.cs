using BeverageVendingMachine.Core.Interfaces.Services;
using Microsoft.CodeAnalysis;

namespace BeverageVendingMachine.Web.Api
{
    public class AdminTerminalApiController : BaseApiController
    {
        private readonly IAdminTerminalService _adminTerminalService;

        public AdminTerminalApiController(IAdminTerminalService adminTerminalService)
        {
            _adminTerminalService = adminTerminalService;
        }

    }
}

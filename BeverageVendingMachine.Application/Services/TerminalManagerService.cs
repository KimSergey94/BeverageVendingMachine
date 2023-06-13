using BeverageVendingMachine.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeverageVendingMachine.Application.Services
{
    /// <summary>
    /// Service to manage vending machine terminal
    /// </summary>
    public class TerminalManagerService : ITerminalManagerService
    {
        private readonly ITerminalService _terminalService;
        public TerminalManagerService(ITerminalService terminalService)
        {
            _terminalService = terminalService;
        }


    }
}

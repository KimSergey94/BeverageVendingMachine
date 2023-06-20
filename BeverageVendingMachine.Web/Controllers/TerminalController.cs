using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeverageVendingMachine.Web.Controllers
{
    public class TerminalController : Controller
    {
        // GET: TerminalController?secretKey
        public ActionResult Index(string secretKey)
        {
            if(string.IsNullOrWhiteSpace(secretKey))
                return View();
            else return View("AdminTerminal");
        }
    }
}

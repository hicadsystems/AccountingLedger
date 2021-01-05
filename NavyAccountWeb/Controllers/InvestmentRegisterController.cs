using Microsoft.AspNetCore.Mvc;
using NavyAccountCore.Core.AuditService;

namespace NavyAccountWeb.Controllers
{
    [SessionTimeout]
    public class InvestmentRegisterController : Controller
    {
        // GET: InvestmentRegister
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewInvestment()
        {
            return View();
        }
    }
}
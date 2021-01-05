using Microsoft.AspNetCore.Mvc;
using NavyAccountCore.Core.AuditService;

namespace NavyAccountWeb.Controllers
{
    [SessionTimeout]
    public class ViewPFFundRateController : Controller
    {
       public ActionResult Index()
       {
            return View();
       }
    }
}
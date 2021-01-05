using Microsoft.AspNetCore.Mvc;
using NavyAccountCore.Core.AuditService;

namespace NavyInvest.Controllers
{
    [SessionTimeout]
    public class RankController : Controller
    {
        // GET: Rank
        public ActionResult CreateRank()
        {
            return View();
        }
    }
}
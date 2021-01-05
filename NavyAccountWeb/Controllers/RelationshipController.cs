using Microsoft.AspNetCore.Mvc;
using NavyAccountCore.Core.AuditService;

namespace NavyInvest.Controllers
{
    [SessionTimeout]
    public class RelationshipController : Controller
    {
        // GET: Relationship
        public ActionResult CreateRelationship()
        {
            return View();
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using NavyAccountCore.Core.AuditService;

namespace NavyAccountWeb.Controllers
{
    [SessionTimeout]
    public class PersonController : Controller
    {
        [HttpGet]
        public ActionResult CreatePerson(int id=0)
        {
            if (id == 0)
            {
                ViewBag.personid = 0;
            }
            else {
                ViewBag.personid = id;
            }
            //ViewBag.Rank = rank.GetAll(); ;
            return View();
        }
        [HttpGet]
        public ActionResult ViewPerson()
        {
           
            return View();
        }

        [HttpGet]
        public ActionResult InActivePerson()
        {

            return View();
        }
    }
}
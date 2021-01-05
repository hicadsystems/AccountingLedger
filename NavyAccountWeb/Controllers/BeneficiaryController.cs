using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using NavyAccountCore.Core.AuditService;

namespace NavyAccountWeb.Controllers
{
    [SessionTimeout]
    public class BeneficiaryController : Controller
    {
       
        // GET: Beneficiary
        public ActionResult Create()
        {
          
            return View();
        }
    }
}
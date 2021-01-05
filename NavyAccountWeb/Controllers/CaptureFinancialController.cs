using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NavyAccountCore.Core.AuditService;

namespace NavyAccountWeb.Controllers
{
    [SessionTimeout]
    public class CaptureFinancialController : Controller
    {
        public IActionResult captureDoc()
        {
            return View();
        }
        public IActionResult Reciept()
        {
            return View();
        }
        public IActionResult DocReversal()
        {
            return View();
        }
        public IActionResult DocReversal2()
        {
            return View();
        }
    }
}
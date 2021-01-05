using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NavyAccountWeb.Controllers.Api.ReferenceTable
{
    public class LoanTypeReviewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using NavyAccountWeb.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.Controllers
{
    public class SRSesssionController : Controller
    {
        private readonly ISessionService sessionService;
        public SRSesssionController(ISessionService sessionService)
        {
            this.sessionService = sessionService;
        }
        public IActionResult session()
        {
            return View();
        }
        public IActionResult term()
        {
            return View();
        }
        [HttpPost]
        public IActionResult MigrateToSession()
        {
            try
            {
                sessionService.MigrateToNewSession(User.Identity.Name);
                TempData["returnMessage"] = "Successfull";
            }
            catch (Exception ex)
            {
                TempData["returnMessage"] = ex.Message;
                throw;
            }
            return View();
        }
        [HttpPost]
        public IActionResult MigrateToTerm()
        {
            try
            {
                sessionService.MigratetoNewterm();
                TempData["returnMessage"] = "Successfull";
            }
            catch (Exception ex)
            {
                TempData["returnMessage"] = ex.Message;
                throw;
            }
            return View();
        }
    }
}

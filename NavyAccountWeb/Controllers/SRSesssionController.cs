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
            var ses = sessionService.GetCurrentSession();
            return View(ses);
        }
        public IActionResult term()
        {
            var ses = sessionService.GetCurrentSession();
            return View(ses);
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
            return RedirectToAction("session");
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
            return RedirectToAction("term");
        }
    }
}

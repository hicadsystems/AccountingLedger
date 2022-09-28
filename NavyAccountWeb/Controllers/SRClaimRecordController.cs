using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.Controllers
{
    public class SRClaimRecordController : Controller
    {
        // GET: SRClaimRecordController
        public ActionResult Index()
        {
            return View();
        }

        // GET: SRClaimRecordController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SRClaimRecordController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SRClaimRecordController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SRClaimRecordController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SRClaimRecordController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SRClaimRecordController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SRClaimRecordController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

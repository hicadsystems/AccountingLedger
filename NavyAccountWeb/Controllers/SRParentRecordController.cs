using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.Controllers
{
    public class SRParentRecordController : Controller
    {
        // GET: SRParentRecordController
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Index2()
        {
            return View();
        }

        // GET: SRParentRecordController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SRParentRecordController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SRParentRecordController/Create
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

        // GET: SRParentRecordController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SRParentRecordController/Edit/5
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

        // GET: SRParentRecordController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SRParentRecordController/Delete/5
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

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.Controllers
{
    public class SRSchoolFeeController : Controller
    {
        // GET: SchoolRecordController
        public ActionResult Index()
        {
            return View();
        }

        // GET: SchoolRecordController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SchoolRecordController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SchoolRecordController/Create
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

        // GET: SchoolRecordController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SchoolRecordController/Edit/5
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

        // GET: SchoolRecordController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SchoolRecordController/Delete/5
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

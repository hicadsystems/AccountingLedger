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
        public SRClaimRecordController()
        {

        }
        // GET: SRClaimRecordController
        public ActionResult InitiateClaim()
        {
            return View();
        }

        // GET: SRClaimRecordController/Details/5
        public ActionResult CliamUpload(int id)
        {
            return View();
        }

        // GET: SRClaimRecordController/Create
        public ActionResult Create()
        {
            return View();
        }

         // GET: SRClaimRecordController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

    }
}

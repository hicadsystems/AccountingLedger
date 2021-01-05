using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MoreLinq.Extensions;
using NavyAccountCore.Core.AuditService;
using NavyAccountCore.Core.Data;
using NavyAccountCore.Models;
using NavyAccountWeb.IServices;
using NavyAccountWeb.Models;
using OfficeOpenXml;
using Wkhtmltopdf.NetCore;

namespace NavyAccountWeb.Controllers
{
    [SessionTimeout]
    public class NavipsController : Controller
    {
        private readonly IPersonService personService;
        private readonly INavipService navipservice;
        private readonly IGeneratePdf generatePdf;
        private readonly INavyAccountDbContext context;
        private readonly IUnitOfWork unitofwork;
        public NavipsController(IUnitOfWork unitofwork,IPersonService personService, INavipService navipservice, IGeneratePdf generatePdf, INavyAccountDbContext context)
        {
            this.personService = personService;
            this.navipservice = navipservice;
            this.generatePdf = generatePdf;
            this.context = context;
            this.unitofwork = unitofwork;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Index2()
        {
            return View();
        }
        public IActionResult NavipReport()
        {
            var bb = context.npf_navip.DistinctBy(x => x.Batch).ToList();
            ViewBag.Batch = bb;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> NavipReport(string batch)
        {
           var personlist = navipservice.getallnavip(batch);
           return await generatePdf.GetPdf("Views/Navips/NavipReportList.cshtml", personlist);

        }
        public IActionResult NavipReportByDate()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> NavipReportByDate(balanceViewModel model)
        {
            if (model.excel==true)
            {
                var opo = navipservice.getnavipbydate(model.startdate, model.enddate);

                var stream = new MemoryStream();


                int row = 2;
                using (var package = new ExcelPackage(stream))
                {
                    var workSheet = package.Workbook.Worksheets.Add("Sheet2");
                    workSheet.Cells.LoadFromCollection(opo, true);
                    package.Save();
                }


                string excelname = "Navip.xlsx";

                stream.Position = 0;
                string excelName = $"LoanSchedule-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelname);
            }
            else if (model.startdate <= model.enddate)
            {
                var opo = navipservice.getnavipbydate(model.startdate, model.enddate);

                return await generatePdf.GetPdf("Views/Navips/NavipReportListByDate.cshtml", opo);
            }

            //HttpContext.Session.SetString("claimdesc", "start date must be less than end date");

            return RedirectToAction("NavipReport", "Navips");

        }
        [HttpGet]
        public ActionResult downloadnavip()
        {
            return View();
        }
        [HttpPost]
        public ActionResult downloadnavip(balanceViewModel model)
        {
            var opo = navipservice.getnavipbydate(model.startdate, model.enddate);

            var stream = new MemoryStream();


            int row = 2;
            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet2");
                workSheet.Cells.LoadFromCollection(opo, true);
                package.Save();
            }


            string excelname = "Navip.xlsx";

            stream.Position = 0;
            string excelName = $"LoanSchedule-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelname);
        }
    }
}
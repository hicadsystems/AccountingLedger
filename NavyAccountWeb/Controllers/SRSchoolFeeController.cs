using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NavyAccountWeb.IServices;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Wkhtmltopdf.NetCore;

namespace NavyAccountWeb.Controllers
{
    public class SRSchoolFeeController : Controller
    {
        private readonly ISchoolFeeService schoolFeeService;
        private readonly ISessionService sessionService;
        private readonly IGeneratePdf generatePdf;
        public SRSchoolFeeController(ISchoolFeeService schoolFeeService, IGeneratePdf generatePdf, ISessionService sessionService)
        {
            this.schoolFeeService = schoolFeeService;
            this.generatePdf = generatePdf;
            this.sessionService = sessionService;
        }
        // GET: SchoolRecordController
        public ActionResult Index()
        {
            return View();
        }

        [Route("SRSchoolFee/SchoolFeeByPdf")]
        public async Task<IActionResult> SchoolFeeByPdf()
        {
            var ses = sessionService.GetCurrentSession().FirstOrDefault();
            var op = await schoolFeeService.GetAllSchoolFeeByPeriod(ses.Period);
            return await generatePdf.GetPdf("Views/SRSchoolFee/SchoolFeePDF.cshtml", op);
        }
        [Route("SRSchoolFee/SchoolFeeByPdfExcel")]
        public async Task<IActionResult> SchoolFeeByExcel()
        {
            var ses = sessionService.GetCurrentSession().FirstOrDefault();
            var op = await schoolFeeService.GetAllSchoolFeeByPeriod(ses.Period);
            var stream = new MemoryStream();

            int row = 2;
            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet2");
                workSheet.Cells.LoadFromCollection(op, true);
                package.Save();
            }

            stream.Position = 0;
            string excelName = $"SchoolFeeTable-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
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

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoreLinq;
using NavyAccountWeb.IServices;
using NavyAccountWeb.Models;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Wkhtmltopdf.NetCore;

namespace NavyAccountWeb.Controllers
{
    public class SRPaymentRecordController : Controller
    {
        // GET: SRPaymentRecordController
        private readonly IPaymentRecordService paymentRecordService;

        private readonly IGeneratePdf GeneratePdf;
        private readonly ILogger<SRPaymentRecordController> logger;

        public SRPaymentRecordController(IPaymentRecordService paymentRecordService, IGeneratePdf generatePdf, ILogger<SRPaymentRecordController> logger)
        {
            this.paymentRecordService = paymentRecordService;
            GeneratePdf = generatePdf;
            this.logger = logger;
        }

        [Route("SRPaymentRecord/PrintPaymentProposalAsPdf")]
        public async Task<IActionResult> PrintPaymentProposalAsPdf()
        {
                var op = await paymentRecordService.GetStudentpaymentProposal();
                var distinctRecord = op.DistinctBy(x => x.Schoolname).ToList();
                var studentRecord = await paymentRecordService.moveRecord(op);
                var oq = new StudentPaymentProposal
                {
                    distintrecord = distinctRecord,
                    studentRecord = studentRecord
                };

                return await GeneratePdf.GetPdf("Views/SRPaymentRecord/PaymentProposal.cshtml", oq);
        }


        [Route("SRPaymentRecord/PrintPaymentProposalAsExcel")]
        public async Task<IActionResult> PrintPaymentProposalAsExcel()
        {

            var op = await paymentRecordService.GetStudentpaymentProposal();
            var oq = await paymentRecordService.moveRecord(op);
            var stream = new MemoryStream();

            int row = 2;
            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet2");
                workSheet.Cells.LoadFromCollection(oq, true);
                package.Save();
            }

            string excelname = "PaymentProposal.xlsx";

            stream.Position = 0;
            string excelName = $"PaymentProposal-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelname);

        }

        [Route("SRPaymentRecord/PrintPaymentProposalAsExcelBySchool/{schoolName}")]
        public async Task<IActionResult> PrintPaymentProposalAsExcel(string schoolName)
        {

            var op = await paymentRecordService.GetStudentpaymentProposalbySchool(schoolName);
            var stream = new MemoryStream();

            int row = 2;
            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet2");
                workSheet.Cells.LoadFromCollection(op, true);
                package.Save();
            }

            string excelname = "PaymentProposal.xlsx";

            stream.Position = 0;
            string excelName = $"PaymentProposal-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelname);

        }

        [Route("SRPaymentRecord/PrintPaymentProposalAsPdfBySchool/{schoolName}")]
        public async Task<IActionResult> PrintPaymentProposalAsPdf(string schoolName)
        {
                var op = await paymentRecordService.GetStudentpaymentProposalbySchool(schoolName);
                return await GeneratePdf.GetPdf("Views/SRPaymentRecord/PaymentProposal.cshtml", op);
        }


        public ActionResult Index()
        {

            return View();
        }

        // GET: SRPaymentRecordController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SRPaymentRecordController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SRPaymentRecordController/Create
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

        // GET: SRPaymentRecordController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SRPaymentRecordController/Edit/5
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

        // GET: SRPaymentRecordController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SRPaymentRecordController/Delete/5
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

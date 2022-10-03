using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoreLinq;
using NavyAccountWeb.IServices;
using NavyAccountWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wkhtmltopdf.NetCore;

namespace NavyAccountWeb.Controllers
{
    public class SRPaymentRecordController : Controller
    {
        // GET: SRPaymentRecordController
        private readonly IPaymentRecordService paymentRecordService;

        public IGeneratePdf GeneratePdf { get; }

        public SRPaymentRecordController(IPaymentRecordService paymentRecordService, IGeneratePdf generatePdf)
        {
            this.paymentRecordService = paymentRecordService;
            GeneratePdf = generatePdf;
        }


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

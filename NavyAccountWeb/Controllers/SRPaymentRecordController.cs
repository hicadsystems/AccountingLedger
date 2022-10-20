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
using NavyAccountCore.Core.Data;
using NavyAccountCore.Models;
using NavyAccountWeb.Services;
using System.Threading;
using System.Data.SqlClient;
using Dapper;

namespace NavyAccountWeb.Controllers
{
    public class SRPaymentRecordController : Controller
    {
        // GET: SRPaymentRecordController
        private readonly IPaymentRecordService paymentRecordService;
        private readonly IDapper dapperService;

        private readonly IGeneratePdf GeneratePdf;
       

        public SRPaymentRecordController(IPaymentRecordService paymentRecordService, IDapper dapperService, IGeneratePdf generatePdf)
        {
            this.paymentRecordService = paymentRecordService;
            GeneratePdf = generatePdf;
            this.dapperService = dapperService;

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

        [Route("SRPaymentRecord/PrintDefaultersAsExcel")]
        public async Task<IActionResult> PrintDefaultersAsExcel()
        {

            var op = await paymentRecordService.GetdefaulterRecord();
            var oq = op.OrderBy(x => x.Term);

            var stream = new MemoryStream();
            int row = 2;
            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet2");
                workSheet.Cells.LoadFromCollection(oq, true);
                package.Save();
            }

            string excelname = "Defaulters.xlsx";

            stream.Position = 0;
            string excelName = $"Defaulters-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelname);

        }

        [Route("SRPaymentRecord/PrintDefaultersAsExcel/{period}")]
        public async Task<IActionResult> PrintDefaultersAsExcel(string period)
        {
            period = period.Substring(0, 4) + '/' + period.Substring(period.Length - 4, 4);
            var op = await paymentRecordService.GetdefaulterRecord();
            var oq = op.Where(x=>x.Period==period).OrderBy(x => x.Term);

            var stream = new MemoryStream();
            int row = 2;
            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet2");
                workSheet.Cells.LoadFromCollection(oq, true);
                package.Save();
            }

            string excelname = "Defaulters.xlsx";

            stream.Position = 0;
            string excelName = $"Defaulters-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelname);

        }

        [Route("SRPaymentRecord/PrintDefaultersAsPdf")]
        public async Task<IActionResult> PrintDefaultersAsPdf()
        {
            var result = new DefaulterViewModel();
            var op = await paymentRecordService.GetdefaulterRecord();
            var oq = op.DistinctBy(x => x.Period);
            result.distinctRecord = oq.ToList();
            result.data = op.OrderBy(x => x.Term).ToList();

            return await GeneratePdf.GetPdf("Views/SRPaymentRecord/DefaultersReport.cshtml",result);
        }

        [Route("SRPaymentRecord/PrintDefaultersAsPdf/{period}")]
        public async Task<IActionResult> PrintDefaultersAsPdf(string period)
        {
            
            period = period.Substring(0,4) + '/' + period.Substring(period.Length - 4, 4);
            var result = new DefaulterViewModel();
            var op = await paymentRecordService.GetdefaulterRecord();
            op = op.Where(x => x.Period == period).ToList();
         
            return await GeneratePdf.GetPdf("Views/SRPaymentRecord/DefaultersReport2.cshtml", op);
        }

        [Route("SRPaymentRecord/PrintPaymentProposalAsExcelBySchool/{schoolName}")]
        public async Task<IActionResult> PrintPaymentProposalAsExcel(string schoolName)
        {

            var op = await paymentRecordService.GetStudentpaymentProposalbySchool(schoolName);
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

        [Route("SRPaymentRecord/PrintPaymentProposalAsPdfBySchool/{schoolName}")]
        public async Task<IActionResult> PrintPaymentProposalAsPdf(string schoolName)
        {
            try
            {

                var op = await paymentRecordService.GetStudentpaymentProposalbySchool(schoolName);
                var oq = await paymentRecordService.moveRecord(op);
                return await GeneratePdf.GetPdf("Views/SRPaymentRecord/PaymentProposalBySchool.cshtml", oq);

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                throw;
            }
        }


        [Route("SRPaymentRecord/PrintFilterSchoolWithStudentPdf")]
        public async Task<IActionResult> PrintFilterSchoolWithStudentPdf()
        {
            var op = await paymentRecordService.filterSchoolWithStudent();
            return await GeneratePdf.GetPdf("Views/SRPaymentRecord/FilterStudentWithSchool.cshtml", op);
        }

        [Route("SRPaymentRecord/PrintFilterSchoolWithStudentAsExcel")]
        public async Task<IActionResult> PrintFilterSchoolWithStudentAsExcel()
        {
            var op = await paymentRecordService.filterSchoolWithStudent();
            var stream = new MemoryStream();

            int row = 2;
            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet2");
                workSheet.Cells.LoadFromCollection(op, true);
                package.Save();
            }

            string excelname = "SchoolReport.xlsx";

            stream.Position = 0;
            string excelName = $"SchoolReport-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelname);
        }

        public async Task<ActionResult> UpdatePaymentProposal()
        {
            TempData["count"] = await paymentRecordService.GetStudentCountUnderDescrepancy();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePayment()
        {
            
            int count = await paymentRecordService.GetStudentCountUnderDescrepancy();
            if (count == 0)
            {
                var param = new DynamicParameters();
                param.Add("@username", User.Identity.Name);
                dapperService.Execute("sp_UpdatePaymentProposal", param, commandType: System.Data.CommandType.StoredProcedure);

                TempData["message"] = "Uploaded Successfully";
            }
           

            TempData["count"] = count;
            return RedirectToAction("UpdatePaymentProposal");


        }


        public IActionResult PaymentProposalupload()
        {
            return View();
        }

        [Route("SRPaymentRecord/PrintDescrepancyReportAsExcel")]
        public async Task<IActionResult> PrintDescrepancyReportAsExcel(string schoolName)
        {
            var oq = await paymentRecordService.GetDiscrepancyRecordAsExcel();
            var stream = new MemoryStream();

            int row = 2;
            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet2");
                workSheet.Cells.LoadFromCollection(oq, true);
                package.Save();
            }

            string excelname = "DecrepancyReport.xlsx";

            stream.Position = 0;
            string excelName = $"DecrepancyReport-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelname);

        }


        public IActionResult ViewDiscrepancyReport()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PaymentProposalupload(IFormFile formFile, CancellationToken cancellationToken)
        {
            try
            {
                if (formFile == null || formFile.Length <= 0)
                {
                    TempData["message"] = "No File Uploaded";
                    //return BadRequest("File not an Excel Format");
                    return View();
                    //return BadRequest("No File Uploaded");
                }

                if (!Path.GetExtension(formFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                {
                    TempData["message"] = "File not an Excel Format";
                    //return BadRequest("File not an Excel Format");
                    return View();
                }
               
                var listapplication = new List<PaymentProposalCapture>();
                var listapplicationofrecordnotavailable = new List<PaymentProposalCapture>();

                using (var stream = new MemoryStream())
                {
                    await formFile.CopyToAsync(stream, cancellationToken);

                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets["Sheet1"];
                        var rowCount = worksheet.Dimension.Rows;
                        string Req_No = String.IsNullOrEmpty(worksheet.Cells[1, 1].ToString()) ? "" : worksheet.Cells[1, 1].Value.ToString().Trim();
                        string Schoolname = String.IsNullOrEmpty(worksheet.Cells[1, 2].ToString()) ? "" : worksheet.Cells[1, 2].Value.ToString().Trim();
                        string Amount = String.IsNullOrEmpty(worksheet.Cells[1, 3].ToString()) ? "" : worksheet.Cells[1, 3].Value.ToString().Trim();
                      

                        if (Req_No != "REG_NUMBER"  || Schoolname != "SCHOOLNAME" || Amount != "AMOUNT"
                            )
                        {
                            return BadRequest("File not in the Right format");
                        }
                        for (int row = 2; row <= rowCount; row++)
                        {
                            if (worksheet.Cells[1, 1].Value == null)
                                worksheet.Cells[1, 1].Value = "";

                            if (worksheet.Cells[1, 2].Value == null)
                                worksheet.Cells[1, 2].Value = "";

                            if (worksheet.Cells[1, 3].Value == null)
                                worksheet.Cells[1, 3].Value = "";


                            if (worksheet.Cells[row, 1].Value == null)
                                worksheet.Cells[row, 1].Value = "";

                            if (worksheet.Cells[row, 2].Value == null)
                                worksheet.Cells[row, 2].Value = "";

                            if (worksheet.Cells[row, 3].Value == null)
                                worksheet.Cells[row, 3].Value = "";


                            string req_num = String.IsNullOrEmpty(worksheet.Cells[row, 1].Value.ToString()) ? "" : worksheet.Cells[row, 1].Value.ToString().Trim();
                            string schName = String.IsNullOrEmpty(worksheet.Cells[row, 2].Value.ToString()) ? "" : worksheet.Cells[row, 2].Value.ToString().Trim();
                            string amount = String.IsNullOrEmpty(worksheet.Cells[row, 3].Value.ToString()) ? "" : worksheet.Cells[row, 3].Value.ToString().Trim();
                        

                            

                            if (String.IsNullOrEmpty(worksheet.Cells[row, 1].Value.ToString()) ||
                               String.IsNullOrEmpty(worksheet.Cells[row, 2].Value.ToString()) ||
                               String.IsNullOrEmpty(worksheet.Cells[row, 3].Value.ToString()))
                            {
                                listapplicationofrecordnotavailable.Add(new PaymentProposalCapture
                                {
                                    REG_NUMBER = req_num,
                                    SCHOOLNAME = schName,
                                    AMOUNT = amount
                                });

                            }
                            else
                            {
                                //check if already in the list -- a possibility
                                listapplication.Add(new PaymentProposalCapture
                                {
                                    REG_NUMBER = req_num,
                                    SCHOOLNAME = schName,
                                    AMOUNT = amount
                                });
                            }
                        }
                        foreach(var j in listapplication)
                        {
                            var op = new PaymentPoposalExcelRecord
                            {
                                Amount = decimal.Parse(j.AMOUNT),
                                Schoolname = j.SCHOOLNAME,
                                Req_Number = j.REG_NUMBER
                            };

                            await paymentRecordService.UpdatePaymentProposal(op);
                        }
                        TempData["message"] = "Uploaded Successfully";
                    }
                }
                if (listapplicationofrecordnotavailable.Count > 0)
                {

                    var stream = new MemoryStream();

                    using (var package = new ExcelPackage(stream))
                    {
                        var workSheet = package.Workbook.Worksheets.Add("Sheet2");
                        workSheet.Cells.LoadFromCollection(listapplicationofrecordnotavailable, true);
                        package.Save();
                    }
                    stream.Position = 0;
                    string excelName = $"paymentproposal-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";

                    //return File(stream, "application/octet-stream", excelName);  
                    return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
                }


                return View();
            }
            catch (Exception ex)
            {
                TempData["message"] = "Fail To Uplaod";
                throw;
            }


        }

        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Index2()
        {

            return View();
        }


        public ActionResult Index3()
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

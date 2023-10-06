using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MoreLinq;
using NavyAccountCore.Core.Data;
using NavyAccountCore.Models;
using NavyAccountWeb.IServices;
using NavyAccountWeb.Models;
using NavyAccountWeb.Services;
using NavyAccountWeb.ViewModels;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Wkhtmltopdf.NetCore;

namespace NavyAccountWeb.Controllers
{
    public class SRClaimRecordController : Controller
    {
        private readonly string _connectionstring;
        private readonly IGeneratePdf generatePdf;
        private readonly IUnitOfWork unitofWork;
        private readonly INavyAccountDbContext context;
        private readonly IStudentRecordService recordService;
        private readonly IClaimRecordService clrecordService;

        public SRClaimRecordController(IClaimRecordService clrecordService,IStudentRecordService recordService,INavyAccountDbContext context, IGeneratePdf generatePdf, IConfiguration configuration, IUnitOfWork unitofWork)
        {
            this.generatePdf = generatePdf;
            this.unitofWork = unitofWork;
            this.context = context;
            this.recordService = recordService;
            this.clrecordService = clrecordService;
            _connectionstring = configuration.GetConnectionString("DefaultConnection");
        }
        // GET: SRClaimRecordController
        public ActionResult ClaimSummary()
        {
            return View();
        }
        [Route("SRClaimRecord/CLaimSummaryByPdf/{studentid}")]
        public async Task<IActionResult> CLaimSummaryByPdf(int studentid)
        {
            var op = await clrecordService.GetStudentSummary(studentid);
            return await generatePdf.GetPdf("Views/SRClaimRecord/ClaimSummarypdf.cshtml", op);
         }
        
        public ActionResult ClaimPayment()
        {
            return View();
        }
        public ActionResult ClaimReimbursment()
        {
            return View();
        }
        public ActionResult InitiateClaim()
        {
            return View();
        }


        // GET: SRClaimRecordController/Create
        public ActionResult CliamUpload()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CliamUpload(IFormFile formFile, CancellationToken cancellationToken, string batch)
        {
            if (formFile == null || formFile.Length <= 0)
            {
                TempData["message"] = "No File Uploaded";
                return View();
            }

            if (!Path.GetExtension(formFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                TempData["message"] = "File not an Excel Format";
                //return BadRequest("File not an Excel Format");
                return View();
            }
            string user = User.Identity.Name;
            var listofstudent = new List<ClaimRecordVM>();
            var liststudentofrecordnotavailable = new List<ClaimRecordVM>();

            using (var stream = new MemoryStream())
            {
                await formFile.CopyToAsync(stream, cancellationToken);

                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets["Sheet1"];
                    var rowCount = worksheet.Dimension.Rows;
                    string REG_NUMBER = String.IsNullOrEmpty(worksheet.Cells[1, 1].ToString()) ? "" : worksheet.Cells[1, 1].Value.ToString().Trim();
                    string CLASS = String.IsNullOrEmpty(worksheet.Cells[1, 2].ToString()) ? "" : worksheet.Cells[1, 2].Value.ToString().Trim();
                    string SCHOOL = String.IsNullOrEmpty(worksheet.Cells[1, 3].ToString()) ? "" : worksheet.Cells[1, 3].Value.ToString().Trim();
                    string TERM = String.IsNullOrEmpty(worksheet.Cells[1, 4].ToString()) ? "" : worksheet.Cells[1, 4].Value.ToString().Trim();
                    string PERIOD = String.IsNullOrEmpty(worksheet.Cells[1, 5].ToString()) ? "" : worksheet.Cells[1, 5].Value.ToString().Trim();





                    if (REG_NUMBER != "Reg_Number" )
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
                        if (worksheet.Cells[1, 4].Value == null)
                            worksheet.Cells[1, 4].Value = "";
                        if (worksheet.Cells[1, 5].Value == null)
                            worksheet.Cells[1, 5].Value = "";



                        if (worksheet.Cells[row, 1].Value == null)
                            worksheet.Cells[row, 1].Value = "";

                        if (worksheet.Cells[row, 2].Value == null)
                            worksheet.Cells[row, 2].Value = "";

                        if (worksheet.Cells[row, 3].Value == null)
                            worksheet.Cells[row, 3].Value = "";
                        if (worksheet.Cells[row, 4].Value == null)
                            worksheet.Cells[row, 4].Value = "";
                        if (worksheet.Cells[row, 5].Value == null)
                            worksheet.Cells[row, 5].Value = "";



                        string reg_number = String.IsNullOrEmpty(worksheet.Cells[row, 1].Value.ToString()) ? "" : worksheet.Cells[row, 1].Value.ToString().Trim();
                        string class1 = String.IsNullOrEmpty(worksheet.Cells[row, 2].Value.ToString()) ? "" : worksheet.Cells[row, 2].Value.ToString().Trim();
                        string school = String.IsNullOrEmpty(worksheet.Cells[row, 3].Value.ToString()) ? "" : worksheet.Cells[row, 3].Value.ToString().Trim();
                        string term = String.IsNullOrEmpty(worksheet.Cells[row, 4].Value.ToString()) ? "" : worksheet.Cells[row, 4].Value.ToString().Trim();
                        string period = String.IsNullOrEmpty(worksheet.Cells[row, 5].Value.ToString()) ? "" : worksheet.Cells[row, 5].Value.ToString().Trim();



                        if (String.IsNullOrEmpty(worksheet.Cells[row, 1].Value.ToString()))
                        {
                            liststudentofrecordnotavailable.Add(new ClaimRecordVM
                            {
                                Reg_Number = reg_number,
                                Class = class1,
                                School = school,
                                Term= term,
                                Period= period,
                               
                            });

                        }
                        else
                        {
                            listofstudent.Add(new ClaimRecordVM
                            {
                                Reg_Number = reg_number,
                                Class = class1,
                                School = school,
                                Term= term,
                                Period= period,

                            });
                        }

                    }
                    string userp = User.Identity.Name;
                    string errormessage="";
                    ClaimRecordUpload procesUpload2 = new ClaimRecordUpload(listofstudent, unitofWork, context, recordService, null,userp, errormessage);
                    await procesUpload2.processUploadInThread();
                    TempData["message"] = procesUpload2.errormessage;

                }

            }
            if (liststudentofrecordnotavailable.Count > 0)
            {

                var stream = new MemoryStream();

                using (var package = new ExcelPackage(stream))
                {
                    var workSheet = package.Workbook.Worksheets.Add("Sheet2");
                    workSheet.Cells.LoadFromCollection(liststudentofrecordnotavailable, true);
                    package.Save();
                }
                stream.Position = 0;
                string excelName = $"ClaimErrorList-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";

                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
            }
            return View();
        }

        public ActionResult CliamReinUpload()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CliamReinUpload(IFormFile formFile, CancellationToken cancellationToken)
        {
            if (formFile == null || formFile.Length <= 0)
            {
                TempData["message"] = "No File Uploaded";
                return View();
            }

            if (!Path.GetExtension(formFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                TempData["message"] = "File not an Excel Format";
                //return BadRequest("File not an Excel Format");
                return View();
            }
            string user = User.Identity.Name;
            var listofstudent = new List<ClaimRecordVM>();
            var liststudentofrecordnotavailable = new List<ClaimRecordVM>();

            using (var stream = new MemoryStream())
            {
                await formFile.CopyToAsync(stream, cancellationToken);

                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets["Sheet1"];
                    var rowCount = worksheet.Dimension.Rows;
                    string REG_NUMBER = String.IsNullOrEmpty(worksheet.Cells[1, 1].ToString()) ? "" : worksheet.Cells[1, 1].Value.ToString().Trim();
                    string CLASS = String.IsNullOrEmpty(worksheet.Cells[1, 2].ToString()) ? "" : worksheet.Cells[1, 2].Value.ToString().Trim();
                    string SCHOOL = String.IsNullOrEmpty(worksheet.Cells[1, 3].ToString()) ? "" : worksheet.Cells[1, 3].Value.ToString().Trim();
                    string AMOUNT = String.IsNullOrEmpty(worksheet.Cells[1, 4].ToString()) ? "" : worksheet.Cells[1, 4].Value.ToString().Trim();
                    string REMARK = String.IsNullOrEmpty(worksheet.Cells[1, 5].ToString()) ? "" : worksheet.Cells[1, 5].Value.ToString().Trim();




                    if (REG_NUMBER != "Reg_Number")
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

                        if (worksheet.Cells[1, 4].Value == null)
                            worksheet.Cells[1, 4].Value = "";
                        if (worksheet.Cells[1, 5].Value == null)
                            worksheet.Cells[1, 5].Value = "";



                        if (worksheet.Cells[row, 1].Value == null)
                            worksheet.Cells[row, 1].Value = "";

                        if (worksheet.Cells[row, 2].Value == null)
                            worksheet.Cells[row, 2].Value = "";

                        if (worksheet.Cells[row, 3].Value == null)
                            worksheet.Cells[row, 3].Value = "";
                        if (worksheet.Cells[row, 4].Value == null)
                            worksheet.Cells[row, 4].Value = "";
                        if (worksheet.Cells[row, 5].Value == null)
                            worksheet.Cells[row, 5].Value = "";



                        string reg_number = String.IsNullOrEmpty(worksheet.Cells[row, 1].Value.ToString()) ? "" : worksheet.Cells[row, 1].Value.ToString().Trim();
                        string class1 = String.IsNullOrEmpty(worksheet.Cells[row, 2].Value.ToString()) ? "" : worksheet.Cells[row, 2].Value.ToString().Trim();
                        string school = String.IsNullOrEmpty(worksheet.Cells[row, 3].Value.ToString()) ? "" : worksheet.Cells[row, 3].Value.ToString().Trim();
                        decimal amount = String.IsNullOrEmpty(worksheet.Cells[row, 4].Value.ToString()) ? 0 : Decimal.Parse(worksheet.Cells[row, 4].Value.ToString().Trim());
                        string remark = String.IsNullOrEmpty(worksheet.Cells[row, 5].Value.ToString()) ? "" : worksheet.Cells[row, 5].Value.ToString().Trim();



                        if (String.IsNullOrEmpty(worksheet.Cells[row, 1].Value.ToString()))
                        {
                            liststudentofrecordnotavailable.Add(new ClaimRecordVM
                            {
                                Reg_Number = reg_number,
                                Class = class1,
                                School = school,
                                amount=amount,
                                remark = remark

                            });

                        }
                        else
                        {
                            listofstudent.Add(new ClaimRecordVM
                            {
                                Reg_Number = reg_number,
                                Class = class1,
                                School = school,
                                amount =amount,
                                remark=remark

                            });
                        }

                    }
                    string userp = User.Identity.Name;
                    string errormessage = "";
                    ClaimRecordUpload procesUpload2 = new ClaimRecordUpload(listofstudent, unitofWork, context, recordService, clrecordService, userp, errormessage);
                    await procesUpload2.reinUploadInThread();
                    TempData["message"] = procesUpload2.errormessage;

                }

            }
            if (liststudentofrecordnotavailable.Count > 0)
            {

                var stream = new MemoryStream();

                using (var package = new ExcelPackage(stream))
                {
                    var workSheet = package.Workbook.Worksheets.Add("Sheet2");
                    workSheet.Cells.LoadFromCollection(liststudentofrecordnotavailable, true);
                    package.Save();
                }
                stream.Position = 0;
                string excelName = $"ClaimErrorList-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";

                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
            }
            return View();
        }


        [Route("SRClaimRecord/CLaimPaymentByPdf/{schoolname}")]
        public async Task<IActionResult> ClaimPaymentPdf(string schoolname)
        {
            try
            {
            if (schoolname == "NULL")
            {
                var op = await clrecordService.GetStudentClaim();
                var distinctRecord = op.DistinctBy(x => x.Schoolname).ToList();
                var allRecord = op.ToList();
                var oq = new ClaimPaymentRecordReport
                {
                    distintrecord = distinctRecord,
                    allRecord = allRecord

                };
                return await generatePdf.GetPdf("Views/SRClaimRecord/ClaimReportpdf.cshtml", oq);
            }
            else
            {
                var op = await clrecordService.GetStudentClaimBySchool(schoolname);
                var distinctRecord = op.DistinctBy(x => x.Schoolname).ToList();
                var allRecord = op.ToList();
                var oq = new ClaimPaymentRecordReport
                {
                    distintrecord = distinctRecord,
                    allRecord = allRecord

                };
                return await generatePdf.GetPdf("Views/SRClaimRecord/ClaimReportpdf.cshtml", oq);
            }

            }
            catch (Exception ex)
            {
                string err = ex.Message;
                throw;
            }
        }

        [Route("SRClaimRecord/CLaimPaymentByExcel/{schoolname}")]
        public async Task<IActionResult> ClaimPaymentByExcel(string schoolname)
        {
            if(schoolname == "NULL")
            {
                var op = await clrecordService.GetStudentClaim();
                var stream = new MemoryStream();

                int row = 2;
                using (var package = new ExcelPackage(stream))
                {
                    var workSheet = package.Workbook.Worksheets.Add("Sheet2");
                    workSheet.Cells.LoadFromCollection(op, true);
                    package.Save();
                }

                stream.Position = 0;
                string excelName = $"ClaimPaymentReport-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
            }
            else
            {
                var op = await clrecordService.GetStudentClaimBySchool(schoolname);
                var stream = new MemoryStream();

                int row = 2;
                using (var package = new ExcelPackage(stream))
                {
                    var workSheet = package.Workbook.Worksheets.Add("Sheet2");
                    workSheet.Cells.LoadFromCollection(op, true);
                    package.Save();
                }

                stream.Position = 0;
                string excelName = $"ClaimPaymentReport-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
            }
        }

      


    }
}

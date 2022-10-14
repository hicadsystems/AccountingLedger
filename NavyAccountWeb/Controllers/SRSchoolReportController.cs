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
using static NavyAccountWeb.Models.SchoolFilterModels;

namespace NavyAccountWeb.Controllers
{
    public class SRSchoolReportController : Controller
    {
        private readonly string _connectionstring;
        private readonly IGeneratePdf generatePdf;
        private readonly IUnitOfWork unitofWork;
        private readonly INavyAccountDbContext context;
        private readonly IStudentRecordService recordService;
        private readonly IClaimRecordService clrecordService;
        private readonly IPaymentRecordService payrecordService;
        public SRSchoolReportController(IPaymentRecordService payrecordService,IClaimRecordService clrecordService,IStudentRecordService recordService,INavyAccountDbContext context, IGeneratePdf generatePdf, IConfiguration configuration, IUnitOfWork unitofWork)
        {
            this.generatePdf = generatePdf;
            this.unitofWork = unitofWork;
            this.context = context;
            this.recordService = recordService;
            this.clrecordService = clrecordService;
            this.payrecordService = payrecordService;
            _connectionstring = configuration.GetConnectionString("DefaultConnection");
        }
        // GET: SRClaimRecordController
        public ActionResult NominalRoll()
        {
            return View();
        }
        [Route("SRSchoolReport/PrintStudentByPdf/{SchoolId}/{ClassId}/{Status}/{ParentalStatus}/{sortby}")]
        public async Task<IActionResult> GetStudentReport(int SchoolId, int ClassId, string Status, string ParentalStatus, string sortby)
        {
            StudentFilterModel value = new StudentFilterModel();
            value.SchoolId = SchoolId;
            value.ClassId = ClassId;
            value.Status = Status;
            value.ParentalStatus = ParentalStatus;
            value.sortby = sortby;

            try
            {
                var sn= await recordService.GetStudentReport(value);
                return await generatePdf.GetPdf("Views/SRSchoolReport/NominalRollInpdf.cshtml", sn);

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [Route("SRSchoolReport/PrintStudentByExcel/{SchoolId}/{ClassId}/{Status}/{ParentalStatus}/{sortby}")]
        public async Task<IActionResult> GetStudentReportExcel(int SchoolId, int ClassId, string Status, string ParentalStatus, string sortby)
        {
            StudentFilterModel value = new StudentFilterModel();
            value.SchoolId = SchoolId;
            value.ClassId = ClassId;
            value.Status = Status;
            value.ParentalStatus = ParentalStatus;
            value.sortby = sortby;

            try
            {
                var sn= await recordService.GetStudentReport(value);
                var stream = new MemoryStream();

                int row = 2;
                using (var package = new ExcelPackage(stream))
                {
                    var workSheet = package.Workbook.Worksheets.Add("Sheet2");
                    workSheet.Cells.LoadFromCollection(sn, true);
                    package.Save();
                }

                stream.Position = 0;
                string excelName = $"NominalRollReport-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public ActionResult ClaimPaySummary()
        {
            return View();
        }
        [Route("SRSchoolReport/CLaimAndPaymentSummaryByPdf/{studentid}")]
        public async Task<IActionResult> CLaimSummaryByPdf(int studentid)
        {
            try
            {

            var claim = await clrecordService.GetStudentSummary(studentid);
                var pay = await payrecordService.GetStudentPaySummary(studentid);
                  var oq = new PaymentAndCalimReport
                 {
                     claimRecord = claim,
                     paymentRecord = pay

                 };
                 return await generatePdf.GetPdf("Views/SRSchoolReport/ClaimAndPaymentSummarypdf.cshtml", oq);

            }
            catch (Exception ex)
            {
                string message = ex.Message;
                throw;
            }
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

                       

                        if (worksheet.Cells[row, 1].Value == null)
                            worksheet.Cells[row, 1].Value = "";

                        if (worksheet.Cells[row, 2].Value == null)
                            worksheet.Cells[row, 2].Value = "";

                        if (worksheet.Cells[row, 3].Value == null)
                            worksheet.Cells[row, 3].Value = "";

                       

                        string reg_number = String.IsNullOrEmpty(worksheet.Cells[row, 1].Value.ToString()) ? "" : worksheet.Cells[row, 1].Value.ToString().Trim();
                        string class1 = String.IsNullOrEmpty(worksheet.Cells[row, 2].Value.ToString()) ? "" : worksheet.Cells[row, 2].Value.ToString().Trim();
                        string school = String.IsNullOrEmpty(worksheet.Cells[row, 3].Value.ToString()) ? "" : worksheet.Cells[row, 3].Value.ToString().Trim();
                        


                        if (String.IsNullOrEmpty(worksheet.Cells[row, 1].Value.ToString()))
                        {
                            liststudentofrecordnotavailable.Add(new ClaimRecordVM
                            {
                                Reg_Number = reg_number,
                                Class = class1,
                                School = school
                               
                            });

                        }
                        else
                        {
                            listofstudent.Add(new ClaimRecordVM
                            {
                                Reg_Number = reg_number,
                                Class = class1,
                                School = school

                            });
                        }

                    }
                    string userp = User.Identity.Name;
                    string errormessage="";
                    ClaimRecordUpload procesUpload2 = new ClaimRecordUpload(listofstudent, unitofWork, context, recordService,userp, errormessage);
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

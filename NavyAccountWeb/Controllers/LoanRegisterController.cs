using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MoreLinq;
using NavyAccountCore.Core.AuditService;
using NavyAccountCore.Core.Data;
using NavyAccountCore.Models;
using NavyAccountWeb.IServices;
using NavyAccountWeb.Services;
using OfficeOpenXml;
using Wkhtmltopdf.NetCore;

namespace NavyAccountWeb.Controllers
{


    [SessionTimeout]
    public class LoanRegisterController : Controller
    {
        //private readonly IPersonService personService;
        //private readonly ILoanTypeService loanTypeService;
        private readonly INavyAccountDbContext context;
        private readonly IUnitOfWork unitofWork;
        private readonly IGeneratePdf generatePdf;
        private readonly ILoanRegisterService loanRegisterService;
        private readonly string _connectionstring;

        public LoanRegisterController(ILoanRegisterService loanRegisterService,IGeneratePdf generatePdf,IConfiguration configuration,IUnitOfWork unitofWork, INavyAccountDbContext context)
        {
            this.unitofWork = unitofWork;
            this.context = context;
            this.loanRegisterService = loanRegisterService;
            this.generatePdf = generatePdf;
            _connectionstring = configuration.GetConnectionString("DefaultConnection");
        }

        public ActionResult Index()
        {
           
            return View();
        }

        public ActionResult loanApplication()
        {
            //ViewBag.fund = GetFund();
            //ViewBag.loan = GetLoan();
            return View();
        }

        [HttpGet]
        public IActionResult loanUpload()
        {
            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> loanUpload(IFormFile formFile, CancellationToken cancellationToken)
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
                string user = User.Identity.Name;
                int fundTypeId = (int)HttpContext.Session.GetInt32("fundtypeid");
                string fundTypeCode = HttpContext.Session.GetString("fundtypecode");
                var listapplication = new List<LoanCapture>();
                var listapplicationofrecordnotavailable = new List<LoanCapture>();

                using (var stream = new MemoryStream())
                {
                    await formFile.CopyToAsync(stream, cancellationToken);

                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets["Sheet1"];
                        var rowCount = worksheet.Dimension.Rows;
                        string SVC_NO = String.IsNullOrEmpty(worksheet.Cells[1, 1].ToString()) ? "" : worksheet.Cells[1, 1].Value.ToString().Trim();
                        string RANK = String.IsNullOrEmpty(worksheet.Cells[1, 2].ToString()) ? "" : worksheet.Cells[1, 2].Value.ToString().Trim();
                        string SURNAME = String.IsNullOrEmpty(worksheet.Cells[1, 3].ToString()) ? "" : worksheet.Cells[1, 3].Value.ToString().Trim();
                        string OTHERNAME = String.IsNullOrEmpty(worksheet.Cells[1, 4].ToString()) ? "" : worksheet.Cells[1, 4].Value.ToString().Trim();
                        string LOAN_TYPE = String.IsNullOrEmpty(worksheet.Cells[1, 5].ToString()) ? "" : worksheet.Cells[1, 5].Value.ToString().Trim();
                        string AMOUNT = String.IsNullOrEmpty(worksheet.Cells[1, 6].ToString()) ? "" : worksheet.Cells[1, 6].Value.ToString().Trim();
                        string TENURE = String.IsNullOrEmpty(worksheet.Cells[1, 7].ToString()) ? "" : worksheet.Cells[1, 7].Value.ToString().Trim();
                        string STATUS = String.IsNullOrEmpty(worksheet.Cells[1, 8].ToString()) ? "" : worksheet.Cells[1, 8].Value.ToString().Trim();
                        string MONTHS_PAID = String.IsNullOrEmpty(worksheet.Cells[1, 9].ToString()) ? "" : worksheet.Cells[1, 9].Value.ToString().Trim();
                        string BATCH = String.IsNullOrEmpty(worksheet.Cells[1, 10].ToString()) ? "" : worksheet.Cells[1, 10].Value.ToString().Trim();
                        string EFFECTIVE_DATE = String.IsNullOrEmpty(worksheet.Cells[1, 11].ToString()) ? "" : worksheet.Cells[1, 11].Value.ToString().Trim();

                        if (SVC_NO != "SVC_NO" || RANK != "RANK" || SURNAME != "SURNAME" || OTHERNAME != "OTHERNAME" || LOAN_TYPE != "LOAN_TYPE" || AMOUNT != "AMOUNT" || TENURE != "TENURE"
                            || STATUS != "STATUS" || MONTHS_PAID != "MONTHS_PAID" || BATCH != "BATCH")
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
                            if (worksheet.Cells[1, 6].Value == null)
                                worksheet.Cells[1, 6].Value = "";
                            if (worksheet.Cells[1, 7].Value == null)
                                worksheet.Cells[1, 7].Value = "";
                            if (worksheet.Cells[1, 8].Value == null)
                                worksheet.Cells[1, 8].Value = "";
                            if (worksheet.Cells[1, 9].Value == null)
                                worksheet.Cells[1, 9].Value = "";
                            if (worksheet.Cells[1, 10].Value == null)
                                worksheet.Cells[1, 10].Value = "";
                            if (worksheet.Cells[1, 11].Value == null)
                                worksheet.Cells[1, 11].Value = "";

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

                            if (worksheet.Cells[row, 6].Value == null)
                                worksheet.Cells[row, 6].Value = "";
                            if (worksheet.Cells[row, 7].Value == null)
                                worksheet.Cells[row, 7].Value = "";

                            if (worksheet.Cells[row, 8].Value == null)
                                worksheet.Cells[row, 8].Value = "";

                            if (worksheet.Cells[row, 9].Value == null)
                                worksheet.Cells[row, 9].Value = "";
                            if (worksheet.Cells[row, 10].Value == null)
                                worksheet.Cells[row, 10].Value = "";
                            if (worksheet.Cells[row, 11].Value == null)
                                worksheet.Cells[row, 11].Value = "";

                            string svcno1 = String.IsNullOrEmpty(worksheet.Cells[row, 1].Value.ToString()) ? "" : worksheet.Cells[row, 1].Value.ToString().Trim();
                            string rank1 = String.IsNullOrEmpty(worksheet.Cells[row, 2].Value.ToString()) ? "" : worksheet.Cells[row, 2].Value.ToString().Trim();
                            string surname1 = String.IsNullOrEmpty(worksheet.Cells[row, 3].Value.ToString()) ? "" : worksheet.Cells[row, 3].Value.ToString().Trim();
                            string othername1 = String.IsNullOrEmpty(worksheet.Cells[row, 4].Value.ToString()) ? "" : worksheet.Cells[row, 4].Value.ToString().Trim();
                            string loantype1 = String.IsNullOrEmpty(worksheet.Cells[row, 5].Value.ToString()) ? "" : worksheet.Cells[row, 5].Value.ToString().Trim();
                            decimal amount1 = String.IsNullOrEmpty(worksheet.Cells[row, 6].Value.ToString()) ? 0 : Decimal.Parse(worksheet.Cells[row, 6].Value.ToString().Trim());
                            int tenure1 = String.IsNullOrEmpty(worksheet.Cells[row, 7].Value.ToString()) ? 0 : int.Parse(worksheet.Cells[row, 7].Value.ToString().Trim());

                            string status1 = String.IsNullOrEmpty(worksheet.Cells[row, 8].Value.ToString()) ? "" : worksheet.Cells[row, 8].Value.ToString().Trim();
                            string monthpaid = String.IsNullOrEmpty(worksheet.Cells[row, 9].Value.ToString()) ? "" : worksheet.Cells[row, 9].Value.ToString().Trim();
                            string batch = String.IsNullOrEmpty(worksheet.Cells[row, 10].Value.ToString()) ? "" : worksheet.Cells[row, 10].Value.ToString().Trim();
                            string effective_date = String.IsNullOrEmpty(worksheet.Cells[row, 11].Value.ToString()) ? "" : worksheet.Cells[row, 11].Value.ToString().Trim();


                            if (String.IsNullOrEmpty(worksheet.Cells[row, 1].Value.ToString()) ||
                               String.IsNullOrEmpty(worksheet.Cells[row, 2].Value.ToString()) ||
                               String.IsNullOrEmpty(worksheet.Cells[row, 3].Value.ToString()) ||
                               String.IsNullOrEmpty(worksheet.Cells[row, 4].Value.ToString()) ||
                               String.IsNullOrEmpty(worksheet.Cells[row, 5].Value.ToString()) ||
                               String.IsNullOrEmpty(worksheet.Cells[row, 6].Value.ToString()) ||
                               String.IsNullOrEmpty(worksheet.Cells[row, 7].Value.ToString()) ||
                               String.IsNullOrEmpty(worksheet.Cells[row, 8].Value.ToString()) ||
                               String.IsNullOrEmpty(worksheet.Cells[row, 9].Value.ToString()) ||
                               String.IsNullOrEmpty(worksheet.Cells[row, 10].Value.ToString()))
                            {
                                listapplicationofrecordnotavailable.Add(new LoanCapture
                                {
                                    SVC_NO = svcno1,
                                    RANK = rank1,
                                    SURNAME = surname1,
                                    OTHERNAME = othername1,
                                    LOAN_TYPE = loantype1,
                                    AMOUNT = amount1,
                                    TENURE = tenure1,
                                    STATUS = status1,
                                    MONTHS_PAID = monthpaid,
                                    BATCH_NO = batch,
                                    EFFECTIVE_DATE = Convert.ToDateTime(effective_date),


                                });

                            }
                            else
                            {
                                //check if already in the list -- a possibility
                                listapplication.Add(new LoanCapture
                                {
                                    SVC_NO = svcno1,
                                    RANK = rank1,
                                    SURNAME = surname1,
                                    OTHERNAME = othername1,
                                    LOAN_TYPE = loantype1,
                                    AMOUNT = amount1,
                                    TENURE = tenure1,
                                    STATUS = status1,
                                    MONTHS_PAID = monthpaid,
                                    BATCH_NO = batch,
                                    EFFECTIVE_DATE = Convert.ToDateTime(effective_date),

                                });
                            }

                        }
                        string userp = User.Identity.Name;

                        ProcesUpload procesUpload2 = new ProcesUpload(listapplication, unitofWork, fundTypeId, fundTypeCode, userp, "");
                        await procesUpload2.processUploadInThread();
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
                    string excelName = $"UserList-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";

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

        public ActionResult ViewLoanRegister()
        {
            return View();
        }

        public ActionResult loanEnquiry()
        {
            return View();
        }
        public ActionResult loanRegisterReport()
        {
            ViewBag.loanstatus = context.loanStatus.ToList();
            return View();
        }
        public async Task<IActionResult> printloanregister(LoanRegisterViewModel loan)
        {
            var fundcode = HttpContext.Session.GetInt32("fundtypeid");

            if (loan.Status != "9")
            {
                var listloan1 = loanRegisterService.getListofLoanRegisterByStatus(Convert.ToInt32(fundcode), loan.Status).Result;
                return await generatePdf.GetPdf("Views/LoanRegister/LoanRegisterReportPage.cshtml", listloan1);
            }
            else
            {
                var listloan = loanRegisterService.getListofLoanReport(Convert.ToInt32(fundcode), loan.Status,loan.startdate,loan.enddate).Result;
                return await generatePdf.GetPdf("Views/LoanRegister/LoanRegisterReportPage.cshtml", listloan);

            }
            //return View();
        }
        [HttpGet]
        public IActionResult Batchlist()
        {
            var loan = context.pf_LoanRegisters.DistinctBy(x=>x.batchNo).ToList();
            ViewBag.getloanbatch = loan;
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Batchlist(LoanRegisterViewModel batch)
        {
            var loan = context.pf_LoanRegisters.DistinctBy(x => x.batchNo).ToList();
            ViewBag.getloanbatch = loan;
            var loanlist = loanRegisterService.getListofLoanRegisterByBatch(batch.batchNo).Result;
            //return View(loanlist);
            return await generatePdf.GetPdf("Views/LoanRegister/BatchListReport.cshtml", loanlist);

     

        }
        [HttpGet]
        public IActionResult loandownloadinbatch()
        {
            ViewBag.getstatus = context.loanStatus.Where(x => x.Id ==3||x.Id==6).ToList();
            var loan = context.pf_LoanRegisters.DistinctBy(x => x.batchNo).ToList();
            ViewBag.getloanbatch = loan.Where(x=>Convert.ToInt32(x.Status)<8);
            return View();

        }
        [HttpPost]
        public ActionResult loandownloadinbatch(LoanRegisterViewModel batch)
        {
          
            if (batch.VoucherNo == "3")
            {
                var getloanregister = unitofWork.loanRegisterRepository.GetloanregisterByCode(x=>x.batchNo==batch.batchNo);
                if (getloanregister != null)
                {
                    getloanregister.Status = "3";
                    getloanregister.datecreated = DateTime.Now;
                    unitofWork.loanRegisterRepository.Update(getloanregister);
                    unitofWork.Done().Wait();
                }
                var result = loanRegisterService.getListofLoanRegisterByBatch2(batch.batchNo).Result;
       
                var stream = new MemoryStream();


                int row = 2;
                using (var package = new ExcelPackage(stream))
                {
                    var workSheet = package.Workbook.Worksheets.Add("Sheet2");
                    workSheet.Cells.LoadFromCollection(result, true);
                    package.Save();
                }


                string excelname = "LoanShedule.xlsx";

                stream.Position = 0;
                string excelName = $"LoanToCPO-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelname);

            }
           else
            {
                var getloanregister = unitofWork.loanRegisterRepository.GetloanregisterByCode(x => x.batchNo == batch.batchNo);
                if (getloanregister != null)
                {
                    getloanregister.Status = batch.Status;
                    getloanregister.datecreated = DateTime.Now;
                    unitofWork.loanRegisterRepository.Update(getloanregister);
                    unitofWork.Done().Wait();
                }
                var result = loanRegisterService.getListofLoanRegisterByBatch(batch.batchNo).Result;

                var stream = new MemoryStream();


                int row = 2;
                using (var package = new ExcelPackage(stream))
                {
                    var workSheet = package.Workbook.Worksheets.Add("Sheet2");
                    workSheet.Cells.LoadFromCollection(result, true);
                    package.Save();
                }


                string excelname = "LoanShedule.xlsx";

                stream.Position = 0;
                string excelName = $"LoanSchedule-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelname);
            }
            //return View();
        }
        [HttpGet]
        public IActionResult loanBatchUpload()
        {
            ViewBag.batch = loanRegisterService.GetLoanRegisterByApplication().Result;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> loanBatchUpload(IFormFile formFile, CancellationToken cancellationToken,string batch)
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
            string user = User.Identity.Name;
            int fundTypeId = (int)HttpContext.Session.GetInt32("fundtypeid");
            string fundTypeCode = HttpContext.Session.GetString("fundtypecode");
            var listapplication = new List<LoanCapture>();
            var listapplicationofrecordnotavailable = new List<LoanCapture>();

            using (var stream = new MemoryStream())
            {
                await formFile.CopyToAsync(stream, cancellationToken);

                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets["Sheet1"];
                    var rowCount = worksheet.Dimension.Rows;
                    string SVC_NO = String.IsNullOrEmpty(worksheet.Cells[1, 1].ToString()) ? "" : worksheet.Cells[1, 1].Value.ToString().Trim();
                    string RANK = String.IsNullOrEmpty(worksheet.Cells[1, 2].ToString()) ? "" : worksheet.Cells[1, 2].Value.ToString().Trim();
                    string SURNAME = String.IsNullOrEmpty(worksheet.Cells[1, 3].ToString()) ? "" : worksheet.Cells[1, 3].Value.ToString().Trim();
                    string OTHERNAME = String.IsNullOrEmpty(worksheet.Cells[1, 4].ToString()) ? "" : worksheet.Cells[1, 4].Value.ToString().Trim();
                    string LOAN_TYPE = String.IsNullOrEmpty(worksheet.Cells[1, 5].ToString()) ? "" : worksheet.Cells[1, 5].Value.ToString().Trim();
                    string AMOUNT = String.IsNullOrEmpty(worksheet.Cells[1, 6].ToString()) ? "" : worksheet.Cells[1, 6].Value.ToString().Trim();
                    string TENURE = String.IsNullOrEmpty(worksheet.Cells[1, 7].ToString()) ? "" : worksheet.Cells[1, 7].Value.ToString().Trim();
                    string STATUS = String.IsNullOrEmpty(worksheet.Cells[1, 8].ToString()) ? "" : worksheet.Cells[1, 8].Value.ToString().Trim();
                    string BATCH_NO = String.IsNullOrEmpty(worksheet.Cells[1, 9].ToString()) ? "" : worksheet.Cells[1, 9].Value.ToString().Trim();
                                      
                    if (SVC_NO != "SVC_NO" || RANK != "RANK" || SURNAME != "SURNAME" || OTHERNAME != "OTHERNAME" || LOAN_TYPE != "LOAN_TYPE" || AMOUNT != "AMOUNT" || TENURE != "TENURE"
                        || STATUS != "STATUS" || BATCH_NO != "BATCH_NO")
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
                        if (worksheet.Cells[1, 6].Value == null)
                            worksheet.Cells[1, 6].Value = "";
                        if (worksheet.Cells[1, 7].Value == null)
                            worksheet.Cells[1, 7].Value = "";
                        if (worksheet.Cells[1, 8].Value == null)
                            worksheet.Cells[1, 8].Value = "";
                        if (worksheet.Cells[1, 9].Value == null)
                            worksheet.Cells[1, 9].Value = "";

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

                        if (worksheet.Cells[row, 6].Value == null)
                            worksheet.Cells[row, 6].Value = "";
                        if (worksheet.Cells[row, 7].Value == null)
                            worksheet.Cells[row, 7].Value = "";

                        if (worksheet.Cells[row, 8].Value == null)
                            worksheet.Cells[row, 8].Value = "";

                        if (worksheet.Cells[row, 9].Value == null)
                            worksheet.Cells[row, 9].Value = "";

                        string svcno1 = String.IsNullOrEmpty(worksheet.Cells[row, 1].Value.ToString()) ? "" : worksheet.Cells[row, 1].Value.ToString().Trim();
                        string rank1 = String.IsNullOrEmpty(worksheet.Cells[row, 2].Value.ToString()) ? "" : worksheet.Cells[row, 2].Value.ToString().Trim();
                        string surname1 = String.IsNullOrEmpty(worksheet.Cells[row, 3].Value.ToString()) ? "" : worksheet.Cells[row, 3].Value.ToString().Trim();
                        string othername1 = String.IsNullOrEmpty(worksheet.Cells[row, 4].Value.ToString()) ? "" : worksheet.Cells[row, 4].Value.ToString().Trim();
                        string loantype1 = String.IsNullOrEmpty(worksheet.Cells[row, 5].Value.ToString()) ? "" : worksheet.Cells[row, 5].Value.ToString().Trim();
                        decimal amount1 = String.IsNullOrEmpty(worksheet.Cells[row, 6].Value.ToString()) ? 0 : Decimal.Parse(worksheet.Cells[row, 6].Value.ToString().Trim());
                        int tenure1 = String.IsNullOrEmpty(worksheet.Cells[row, 7].Value.ToString()) ? 0 : int.Parse(worksheet.Cells[row, 7].Value.ToString().Trim());

                        string status1 = String.IsNullOrEmpty(worksheet.Cells[row, 8].Value.ToString()) ? "" : worksheet.Cells[row, 8].Value.ToString().Trim();
                        string batchno = String.IsNullOrEmpty(worksheet.Cells[row, 9].Value.ToString()) ? "" : worksheet.Cells[row, 9].Value.ToString().Trim();
                       

                        if (String.IsNullOrEmpty(worksheet.Cells[row, 1].Value.ToString()) ||
                           String.IsNullOrEmpty(worksheet.Cells[row, 2].Value.ToString()) ||
                           String.IsNullOrEmpty(worksheet.Cells[row, 3].Value.ToString()) ||
                           String.IsNullOrEmpty(worksheet.Cells[row, 5].Value.ToString()) ||
                           String.IsNullOrEmpty(worksheet.Cells[row, 6].Value.ToString()) ||
                           String.IsNullOrEmpty(worksheet.Cells[row, 7].Value.ToString()) ||
                           String.IsNullOrEmpty(worksheet.Cells[row, 8].Value.ToString()))
                        {
                            listapplicationofrecordnotavailable.Add(new LoanCapture
                            {
                                SVC_NO = svcno1,
                                RANK = rank1,
                                SURNAME = surname1,
                                OTHERNAME = othername1,
                                LOAN_TYPE = loantype1,
                                AMOUNT = amount1,
                                TENURE = tenure1,
                                STATUS = status1,
                                BATCH_NO = batchno,


                            });

                        }
                        else
                        {
                            //check if already in the list -- a possibility
                            listapplication.Add(new LoanCapture
                            {
                                SVC_NO = svcno1,
                                RANK = rank1,
                                SURNAME = surname1,
                                OTHERNAME = othername1,
                                LOAN_TYPE = loantype1,
                                AMOUNT = amount1,
                                TENURE = tenure1,
                                STATUS = status1,
                                BATCH_NO = batchno,
                             

                            });
                        }

                    }
                    string userp = User.Identity.Name;

                    ProcesUpload procesUpload2 = new ProcesUpload(listapplication, unitofWork, fundTypeId, fundTypeCode, userp,batch);
                    await procesUpload2.processUploadInThreadB();
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
                string excelName = $"UserList-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";

                //return File(stream, "application/octet-stream", excelName);  
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
            }
            return View();
        }


        [HttpGet]
        public IActionResult PutBatchApprove()
        {
            ViewBag.getbank = context.npf_Charts.Where(x => x.subtype=="4").ToList();
            ViewBag.getloanbatch = context.pf_LoanRegisters.DistinctBy(x => x.batchNo).ToList();

            return View();

        }
        [HttpPost]
        public async Task<IActionResult> updatestatus(LoanRegisterViewModel batch)
        {
            using (SqlConnection sqls = new SqlConnection(_connectionstring))
            {
                using (SqlCommand cmd = new SqlCommand("npf_Batch_status_loan", sqls))
                {
                    cmd.CommandTimeout = 1200;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@globaluser", User.Identity.Name));
                    cmd.Parameters.Add(new SqlParameter("@fundcode", HttpContext.Session.GetString("fundtypecode")));
                    cmd.Parameters.Add(new SqlParameter("@status", batch.VoucherNo));
                    cmd.Parameters.Add(new SqlParameter("@batch", batch.batchNo));



                    await sqls.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();

                    return Ok(new { responseCode = 200, responseDescription = "Updated Successfully" });
                }
            }

        }
        
        public IActionResult LoanRecievable()
        {
            var batch = context.pf_LoanRegisters.DistinctBy(x => x.batchNo).ToList();
            var loan = context.Pf_loanType.ToList();

            ViewBag.getloantype = loan;
            ViewBag.getloanbatch = batch;
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> LoanRecievable(int loantype,string batch,bool ckforoutst)
        {
            try
            {
                var batchno = context.pf_LoanRegisters.DistinctBy(x => x.batchNo).ToList();
                var loan = context.Pf_loanType.ToList();

                ViewBag.getloantype = loan;
                ViewBag.getloanbatch = batchno;

                var loanRecieveableList = loanRegisterService.getListofLoanRegisterRecieveable(loantype, batch, ckforoutst).Result;

                return await generatePdf.GetPdf("Views/LoanRegister/RecieveableListReport.cshtml", loanRecieveableList);

            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [HttpGet]
        public IActionResult downloadRecieveable()
        {
            var batchno = context.pf_LoanRegisters.DistinctBy(x => x.batchNo).ToList();
            var loan = context.Pf_loanType.ToList();

            ViewBag.getloantype = loan;
            ViewBag.getloanbatch = batchno;
            return View();

        }
        [HttpPost]
        public async Task<ActionResult> downloadRecieveable(int loantype, string batch, bool ckforoutst)
        {
          
            if (ckforoutst == true)
            { 

                var loanRecieveableList = context.pf_LoanRegisters
                    .Where(x => x.batchNo == batch && x.LoanTypeID == loantype 
                    && x.loancount<Convert.ToInt32(x.Tenure)).ToList();

                var stream = new MemoryStream();

                int row = 2;
                using (var package = new ExcelPackage(stream))
                {
                    var workSheet = package.Workbook.Worksheets.Add("Sheet2");
                    workSheet.Cells.LoadFromCollection(loanRecieveableList, true);
                    package.Save();
                }


                string excelname = "LoanRecieveable.xlsx";

                stream.Position = 0;
                string excelName = $"LoanToCPO-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelname);



            }
            else
            {
                var loanRecieveableList = loanRegisterService.getListofLoanRegisterRecieveable(loantype, batch,ckforoutst).Result;

                var stream = new MemoryStream();

                int row = 2;
                using (var package = new ExcelPackage(stream))
                {
                    var workSheet = package.Workbook.Worksheets.Add("Sheet2");
                    workSheet.Cells.LoadFromCollection(loanRecieveableList, true);
                    package.Save();
                }


                string excelname = "LoanRecieveable.xlsx";

                stream.Position = 0;
                string excelName = $"LoanToCPO-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelname);


            }


        }

        public IActionResult LoanBatchupdate()
        {

            return View();

        }
        public ActionResult LoanReversal()
        {
            return View();
        }
        public IActionResult LoanSummarybyBatch()
        {
            var batchno = context.pf_LoanRegisters.DistinctBy(x => x.batchNo).ToList();
            ViewBag.getloanbatch = batchno;

            return View();

        }
        [HttpPost]
        public async Task<IActionResult> LoanSummarybyBatch2(string batch)
        {

            //var loanrg = context.pf_LoanRegisters.Where(x => x.batchNo == batchNo);
           
            var loans = loanRegisterService.getListofLoanReportSummary(batch).Result;
            int count = getloancount(loans.FirstOrDefault().EffectiveDate);
            if(count>Convert.ToInt32(loans.FirstOrDefault().Tenure))
                count=Convert.ToInt32(loans.FirstOrDefault().Tenure);
            var loanlist =loans.Where(x=>x.LoanTypeID!=count && Convert.ToInt32(x.Tenure)>count && Convert.ToInt32(x.Tenure)!= x.LoanTypeID).ToList();
            return await generatePdf.GetPdf("Views/LoanPosition/LoanRegisterReportPage.cshtml", loanlist);

        }
        public int getloancount(DateTime effectivedate)
        {
            var LS = new List<LoanSummaryVM>();
            DateTime compareTo = effectivedate.Date;
            DateTime now = DateTime.Now.Date;


            int dateSpan = (now.Year- compareTo.Year)*12; 
            int datespan2= (now.Month- compareTo.Month);
            int months = dateSpan + datespan2;
            int finalamt = months;
            
            return finalamt;
        }
        public IActionResult LoanSummary()
        {
            var loan = context.Pf_loanType.ToList();

            ViewBag.getloantype = loan;
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> LoanSummary2(string year,int type)
        {
            var LS = new List<LoanSummaryVM>();
            var batchno = context.pf_LoanRegisters.DistinctBy(x => x.batchNo).Where(x=>x.LoanTypeID==type).ToList();
            foreach (var a in batchno)
            {
                               
                var d = new LoanSummaryVM
                {
                    noofpeople = context.pf_LoanRegisters.Where(x => x.batchNo == a.batchNo).Count(),
                    totalloan = getinterest(a.batchNo),//(decimal)context.pf_LoanRegisters.Where(x => x.batchNo == a.batchNo).ToList().Sum(x => x.Amount),
                    amountpaid = getAmountPaid(a.batchNo),
                    curcount =a.loancount,
                    term=Convert.ToInt32(a.Tenure),
                    amountperyearR = (decimal)context.npf_Histories.Where(x => x.batchNo == a.batchNo && x.doctype != "JV" && x.period.Substring(0, 4) == year
                                    && x.refno.Substring(0,3)==a.batchNo.Substring(0,3) && x.remarks== "LOAN REVERSAL").ToList().Sum(x => x.cramt),
                    amountperyear = (decimal)context.npf_Histories.Where(x => x.batchNo == a.batchNo && x.doctype != "JV" && x.period.Substring(0, 4) == year).ToList().Sum(x => x.cramt),
                    amountpaidP = (decimal)context.npf_Histories.Where(x => x.batchNo == a.batchNo && x.doctype != "JV" ).ToList().Sum(x => x.cramt),
                    amountpaidOP = (decimal)context.npf_Histories.Where(x => x.batchNo == a.batchNo && x.doctype == "JV").ToList().Sum(x => x.cramt),
                    batchno = a.batchNo,
                    Effectivedate=a.EffectiveDate,
                    loantype=(int)a.LoanTypeID,
                    //noofde=faulter = hcount
                };
                if (d.amountperyear == 0)
                {
                    continue;
                }
                d.amountperyear = d.amountperyear - d.amountperyearR;
                d.amountpaidH = d.totalloan - d.amountpaidOP + d.amountpaidP;
                if (d.amountpaidH >= d.totalloan)
                {
                    d.amountpaidH = d.amountpaidH - d.totalloan;
                    d.amountowned = d.amountowned - d.totalloan;
                }
                d.amountowned = d.amountpaid - d.amountpaidH;
                LS.Add(d);
            }
            decimal total = LS.ToList().Sum(x => x.amountperyear);
            
            return await generatePdf.GetPdf("Views/LoanPosition/LoansummaryReport.cshtml", LS.OrderBy(x=>x.batchno));
            ////return View(loanRecieveableList);

        }
        public decimal getinterest(string batchNO)
        {
            var LS = new List<LoanSummaryVM>();
            decimal finalamt = 0;
            var loan= context.pf_LoanRegisters.Where(x => x.batchNo==batchNO).ToList();
            foreach (var a in loan)
            {
                int interest=0;
                var loantypeint = context.Pf_loanType.FirstOrDefault(x => x.Id == a.LoanTypeID);
                var loanreview = context.npf_loantypereview.Where(x => x.LoanType == loantypeint.Code);
                foreach (var r in loanreview)
                {
                    var d = new LoanSummaryVM();
                    if (r.ReviewDate.Date > a.EffectiveDate.Date)
                    {
                        continue;
                    }
                    else
                    {
                        interest = r.Interestrate;
                        d.amountpaid = (decimal)a.Amount + (((decimal)a.Amount * interest) / 100);
                    }
                    LS.Add(d);
                }   
            }
            finalamt = LS.Sum(x => x.amountpaid);
            return finalamt;
        }
        public decimal getAmountPaid(string batchNO)
        {

            var LS = new List<LoanSummaryVM>();
            decimal finalamt = 0;
            var loan = context.pf_LoanRegisters.Where(x => x.batchNo == batchNO).ToList();
            foreach (var a in loan)
            {
                int interest = 0;
                var loantypeint = context.Pf_loanType.FirstOrDefault(x => x.Id == a.LoanTypeID);
                var loanreview = context.npf_loantypereview.Where(x => x.LoanType == loantypeint.Code);
                foreach (var r in loanreview)
                {
                    var d = new LoanSummaryVM();
                    if (r.ReviewDate.Date > a.EffectiveDate.Date)
                    {
                        continue;
                    }
                    else
                    {
                        interest = r.Interestrate;
                        d.amountpaid = (((decimal)a.Amount + (((decimal)a.Amount * interest) / 100))/Convert.ToInt32(a.Tenure))*a.loancount;
                    }
                    LS.Add(d);
                }
            }
            finalamt = LS.Sum(x => x.amountpaid);
            return finalamt;
        }
    }
}
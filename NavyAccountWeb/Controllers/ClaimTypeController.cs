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
using MoreLinq.Extensions;
using NavyAccountCore.Core.AuditService;
using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Entities;
using NavyAccountCore.Models;
using NavyAccountWeb.IServices;
using NavyAccountWeb.Models;
using NavyAccountWeb.Services;
using OfficeOpenXml;
using Wkhtmltopdf.NetCore;

namespace NavyAccountWeb.Controllers
{
    [SessionTimeout]
    public class ClaimTypeController : Controller
    {
        private readonly IGeneratePdf generatePdf;
        private readonly IFundTypeService fundTypeService;
        private readonly IUnitOfWork unitOfWork;
        private readonly ILedgerService ledgerService;
        private readonly IClaimRegisterService claimRegisterService;
        private readonly IClaimTypeServices claimTypeService;
        private readonly IFundTypeCodeService fundService;
        private readonly INavipService navipservice;
        private readonly IChartofAccountService chartofAccountService;
        private readonly string _connectionstring;


        public ClaimTypeController(ILedgerService ledgerService, IConfiguration configuration,
            IFundTypeService fundTypeService, IClaimRegisterService claimRegisterService, 
            IGeneratePdf generatePdf, IFundTypeCodeService fundService, 
            IClaimTypeServices claimTypeService,IUnitOfWork unitOfWork, 
            IChartofAccountService chartofAccountService,INavipService navipservice)
        {
            this.claimRegisterService = claimRegisterService;
            this.ledgerService = ledgerService;
            this.generatePdf = generatePdf;
            this.fundService = fundService;
            this.claimTypeService = claimTypeService;
            this.fundTypeService = fundTypeService;
            this.unitOfWork = unitOfWork;
            this.chartofAccountService = chartofAccountService;
            this.navipservice = navipservice;
            _connectionstring = configuration.GetConnectionString("DefaultConnection");
        } 
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult claimreport()
        {
            return View();
        }
        [Route("ClaimType/finishClaimRequest/{svcno}/{fundcode}")]
        public async Task<IActionResult> finishClaimRequestAsync(string svcno, string fundcode)
        {

            if (fundcode == "0")
            {
                var getclaim2 = claimRegisterService.GetclaimBysvcNo(svcno).ToList();

                return await generatePdf.GetPdf("Views/ClaimType/ReportPage2.cshtml", getclaim2);
            }
            else
            {
                var getclaim = claimRegisterService.Getclaim(svcno, fundcode).ToList();

                return await generatePdf.GetPdf("Views/ClaimType/ReportPage.cshtml", getclaim);
            }
        }


        public IActionResult claimReportByDate()
        {


            var result=claimRegisterService.GetClaimRegister();
            var result2= fundTypeService.GetFundTypes();
            ViewBag.status = result2.DistinctBy(x => x.Code);
            //ViewBag.status = result.DistinctBy(x => x.status);
            if (HttpContext.Session.GetString("claimdesc") != null)
            {
                ViewBag.claimdesc = HttpContext.Session.GetString("claimdesc");

                HttpContext.Session.Remove("claimdesc");
            }
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> claimReportByDate(balanceViewModel model)
        {
            if (model.Indicator == "10")
            {
                if (model.excel == true)
                {
                    var opo = navipservice.getnavipbydate2(model.startdate, model.enddate);

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
                else
                {
                    var opo = navipservice.getnavipbydate2(model.startdate, model.enddate);
                    return await generatePdf.GetPdf("Views/ClaimType/ReportPage2.cshtml", opo);
                }

            }
            else
            {
                if (model.excel == true)
                {
                    var opo = claimRegisterService.Getclaimbydate(model.startdate, model.enddate, model.Indicator);

                    var stream = new MemoryStream();


                    int row = 2;
                    using (var package = new ExcelPackage(stream))
                    {
                        var workSheet = package.Workbook.Worksheets.Add("Sheet2");
                        workSheet.Cells.LoadFromCollection(opo, true);
                        package.Save();
                    }


                    string excelname = "Dependant.xlsx";

                    stream.Position = 0;
                    string excelName = $"LoanSchedule-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
                    return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelname);
                }
                //else if (model.Indicator == null)
                //{
                //    model.Indicator = "All";
                //}
                //if (model.startdate <= model.enddate)
                else
                {
                    var opo = claimRegisterService.Getclaimbydate(model.startdate, model.enddate, model.Indicator);

                    return await generatePdf.GetPdf("Views/ClaimType/ReportPage3.cshtml", opo);
                }
            }
            //HttpContext.Session.SetString("claimdesc", "start date must be less than end date");

            //return RedirectToAction("claimReportByDate", "ClaimType");

        }


        public async Task<ActionResult> Index3()
        {
            ViewBag.batchList = claimTypeService.GetBatchList();
            var jk =await chartofAccountService.GetChartofAccountByCode4("4");
            ViewBag.bank = jk;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async  Task<ActionResult> Index3(Npf_ClaimRegister model)
        {
            var result = claimTypeService.GetSingleBatchno(model.BatchNo);
            var result2 = new List<Npf_ClaimRegister>();
            var result3 = new List<Npf_ClaimRegister>();

            if (result.Count > 0)
            {
                foreach (var j in result)
                {
                    using (SqlConnection sqls = new SqlConnection(_connectionstring))
                    {
                        using (SqlCommand cmd = new SqlCommand("npf_update_claim", sqls))
                        {
                            cmd.CommandTimeout = 1200;
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@globaluser", User.Identity.Name));
                            cmd.Parameters.Add(new SqlParameter("@claimtype", j.FundTypeID));
                            cmd.Parameters.Add(new SqlParameter("@fundtype", HttpContext.Session.GetInt32("fundtypeid")));
                            cmd.Parameters.Add(new SqlParameter("@bankcode", model.Beneficiary));
                            cmd.Parameters.Add(new SqlParameter("@persid", j.PersonID));


                            try
                            {
                                await sqls.OpenAsync();
                                await cmd.ExecuteNonQueryAsync();

                                result3.Add(j);
                            }
                            catch
                            {
                                result2.Add(j);
                            }
                        }

                    }
                }

                TempData["message"] = result3.Count.ToString() + " successfully approved";
            }
            else
            {
                TempData["message"] = "batch no does not exist";
            }
            
            return RedirectToAction("Index3","ClaimType");
        }





        public ActionResult Index2()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index2(IFormFile formFile, CancellationToken cancellationToken)
        {
            if (formFile == null || formFile.Length <= 0)
            {
                TempData["message"] = "No File Uploaded";

                return View();
            }

            if (!Path.GetExtension(formFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                TempData["message"] = "File not an Excel Format";

                return View();
            }

            string user = User.Identity.Name;

            var listApplication = new List<ClaimCapture>();
            int fundTypeId = (int)HttpContext.Session.GetInt32("fundtypeid");
            string fundTypeCode = HttpContext.Session.GetString("fundtypecode");
            using (var stream = new MemoryStream())
            {
                await formFile.CopyToAsync(stream, cancellationToken);

                using (var package = new ExcelPackage(stream))
                {

                    ExcelWorksheet worksheet = package.Workbook.Worksheets["Sheet1"];
                    var rowCount = worksheet.Dimension.Rows;
                    string Rank = String.IsNullOrEmpty(worksheet.Cells[1, 1].ToString()) ? "" : worksheet.Cells[1, 1].Value.ToString().Trim();
                    string SvcNo = String.IsNullOrEmpty(worksheet.Cells[1, 2].ToString()) ? "" : worksheet.Cells[1, 2].Value.ToString().Trim();
                    string AccountName = String.IsNullOrEmpty(worksheet.Cells[1, 3].ToString()) ? "" : worksheet.Cells[1, 3].Value.ToString().Trim();
                    string Bank = String.IsNullOrEmpty(worksheet.Cells[1, 4].ToString()) ? "" : worksheet.Cells[1, 4].Value.ToString().Trim();
                    string AccountNo = String.IsNullOrEmpty(worksheet.Cells[1, 5].ToString()) ? "" : worksheet.Cells[1, 5].Value.ToString().Trim();
                    string Amount = String.IsNullOrEmpty(worksheet.Cells[1, 6].ToString()) ? "" : worksheet.Cells[1, 6].Value.ToString().Trim();
                    string Remark = String.IsNullOrEmpty(worksheet.Cells[1, 7].ToString()) ? "" : worksheet.Cells[1, 7].Value.ToString().Trim();
                    string BatchNo = String.IsNullOrEmpty(worksheet.Cells[1, 8].ToString()) ? "" : worksheet.Cells[1, 8].Value.ToString().Trim();
                    string Type = String.IsNullOrEmpty(worksheet.Cells[1, 9].ToString()) ? "" : worksheet.Cells[1, 9].Value.ToString().Trim();



                    if (AccountName != "AccountName" || Bank != "Bank" || AccountNo != "AccountNo" || Remark != "Remark")
                    {
                        return BadRequest("File not in the Right format");
                    }

                  



                    for (int j = 2; j <= rowCount; j++)
                    {
                        var jp = new ClaimCapture();
                        string rank = String.IsNullOrEmpty(worksheet.Cells[j, 1].Value.ToString()) ? "" : worksheet.Cells[j, 1].Value.ToString().Trim();
                        string svcno = String.IsNullOrEmpty(worksheet.Cells[j, 2].Value.ToString()) ? "" : worksheet.Cells[j, 2].Value.ToString().Trim();
                        string accountname = String.IsNullOrEmpty(worksheet.Cells[j, 3].Value.ToString()) ? "" : worksheet.Cells[j, 3].Value.ToString().Trim();
                        string bank = String.IsNullOrEmpty(worksheet.Cells[j, 4].Value.ToString()) ? "" : worksheet.Cells[j, 4].Value.ToString().Trim();
                        string accountno = String.IsNullOrEmpty(worksheet.Cells[j, 5].Value.ToString()) ? "" : worksheet.Cells[j, 5].Value.ToString().Trim();

                        decimal amount =Convert.ToDecimal(String.IsNullOrEmpty(worksheet.Cells[j, 6].Value.ToString()) ? "" : worksheet.Cells[j, 6].Value.ToString().Trim());
                        string remark = String.IsNullOrEmpty(worksheet.Cells[j, 7].Value.ToString()) ? "" : worksheet.Cells[j, 7].Value.ToString().Trim();
                        string batchno = String.IsNullOrEmpty(worksheet.Cells[j, 8].Value.ToString()) ? "" : worksheet.Cells[j, 8].Value.ToString().Trim();
                        string type = String.IsNullOrEmpty(worksheet.Cells[j, 9].Value.ToString()) ? "" : worksheet.Cells[j, 9].Value.ToString().Trim();


                        if (String.IsNullOrEmpty(worksheet.Cells[j, 1].Value.ToString()) ||
                          String.IsNullOrEmpty(worksheet.Cells[j, 2].Value.ToString()) ||
                          String.IsNullOrEmpty(worksheet.Cells[j, 3].Value.ToString()) ||
                          String.IsNullOrEmpty(worksheet.Cells[j, 4].Value.ToString())||
                          String.IsNullOrEmpty(worksheet.Cells[j, 5].Value.ToString()) ||
                          String.IsNullOrEmpty(worksheet.Cells[j, 6].Value.ToString()) ||
                          String.IsNullOrEmpty(worksheet.Cells[j, 7].Value.ToString())||
                          String.IsNullOrEmpty(worksheet.Cells[j, 8].Value.ToString()))
                        {
                            jp.rank = rank;
                            jp.svcno = svcno;
                            jp.accountname = accountname;
                            jp.bank = bank;
                            jp.accountno = accountno;
                            jp.amount = amount;
                            jp.remark = remark;
                            jp.batchno = batchno;
                            jp.type = type;

                        }


                        if (worksheet.Cells[j, 1].Value.ToString().Trim() != null)
                        {
                            jp.rank = worksheet.Cells[j, 1].Value.ToString().Trim();
                            jp.svcno = worksheet.Cells[j, 2].Value.ToString().Trim();
                            jp.accountname = worksheet.Cells[j, 3].Value.ToString().Trim();
                            jp.bank = worksheet.Cells[j, 4].Value.ToString().Trim();
                            jp.accountno = worksheet.Cells[j, 5].Value.ToString().Trim();
                            jp.amount =Convert.ToDecimal(worksheet.Cells[j, 6].Value.ToString().Trim());
                            jp.remark = worksheet.Cells[j, 7].Value.ToString().Trim();
                            jp.batchno = worksheet.Cells[j, 8].Value.ToString().Trim();
                            jp.type = worksheet.Cells[j, 9].Value.ToString().Trim();


                        }


                        listApplication.Add(jp);
                       
                    }

                    var p = fundService.GetFundTypeCodeByCode(fundTypeCode);

                    List<Npf_ClaimRegister> successRecord = new List<Npf_ClaimRegister>();
                    List<ClaimCapture> errorRecord = new List<ClaimCapture>();
                    string batch = listApplication.FirstOrDefault().batchno;
                    if (claimTypeService.checkClaimBatchnoExist(batch))
                    {
                        TempData["message"] = "Batch number exist";
                    }
                    else
                    {
                        ClaimProcessUpload upload = new ClaimProcessUpload(listApplication,unitOfWork , claimTypeService, fundTypeService);
                        errorRecord = await upload.claimUploadInThread();

                        if (errorRecord.Count > 0)
                        {
                            var stream2 = new MemoryStream();

                            using (var package2 = new ExcelPackage(stream2))
                            {
                                var workSheet = package2.Workbook.Worksheets.Add("Sheet2");
                                workSheet.Cells.LoadFromCollection(errorRecord, true);
                                package2.Save();
                            }
                            stream2.Position = 0;
                            string excelName = $"UploadERROR-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";

                            return File(stream2, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
                        }
                        else 
                        {
                            //successRecord = await upload.claimProcessUploadInThread();
                            TempData["message"]="Successfully Uploaded";
                        }
                       
                    }
                  


                 

                    //TrialBalanceUpload upk = new TrialBalanceUpload(listApplication, unitOfWork, fundTypeCode, fundTypeId, user, m, year);
                    //var listapplicationofrecordnotavailable = await upk.UploadHistoryInThread();
                    //await upk.TrialbalanceUploadInThread();

                    //TempData["message"] = "Uploaded Successfully";


                    //if (listapplicationofrecordnotavailable.Count > 0)
                    //{

                    //    var stream2 = new MemoryStream();

                    //    using (var package2 = new ExcelPackage(stream2))
                    //    {
                    //        var workSheet = package2.Workbook.Worksheets.Add("Sheet2");
                    //        workSheet.Cells.LoadFromCollection(listapplicationofrecordnotavailable, true);
                    //        package2.Save();
                    //    }
                    //    stream2.Position = 0;
                    //    string excelName = $"Trialbalance-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";

                    //    //return File(stream, "application/octet-stream", excelName);  
                    //    return File(stream2, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
                    //}
                }



            }


            return RedirectToAction("Index2");
        }
        public ActionResult Index4()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index4(IFormFile formFile, CancellationToken cancellationToken)
        {
            if (formFile == null || formFile.Length <= 0)
            {
                TempData["message"] = "No File Uploaded";

                return View();
            }

            if (!Path.GetExtension(formFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                TempData["message"] = "File not an Excel Format";

                return View();
            }

            string user = User.Identity.Name;

            var listApplication = new List<ClaimCapture>();
            int fundTypeId = (int)HttpContext.Session.GetInt32("fundtypeid");
            string fundTypeCode = HttpContext.Session.GetString("fundtypecode");
            using (var stream = new MemoryStream())
            {
                await formFile.CopyToAsync(stream, cancellationToken);

                using (var package = new ExcelPackage(stream))
                {

                    ExcelWorksheet worksheet = package.Workbook.Worksheets["Sheet1"];
                    var rowCount = worksheet.Dimension.Rows;
                    string SvcNo = String.IsNullOrEmpty(worksheet.Cells[1, 1].ToString()) ? "" : worksheet.Cells[1, 1].Value.ToString().Trim();
                    string AccountName = String.IsNullOrEmpty(worksheet.Cells[1, 2].ToString()) ? "" : worksheet.Cells[1, 2].Value.ToString().Trim();
                    string Bank = String.IsNullOrEmpty(worksheet.Cells[1, 3].ToString()) ? "" : worksheet.Cells[1, 3].Value.ToString().Trim();
                    string AccountNo = String.IsNullOrEmpty(worksheet.Cells[1, 4].ToString()) ? "" : worksheet.Cells[1, 4].Value.ToString().Trim();
                    string Amount = String.IsNullOrEmpty(worksheet.Cells[1, 5].ToString()) ? "" : worksheet.Cells[1, 5].Value.ToString().Trim();
                    string Remark = String.IsNullOrEmpty(worksheet.Cells[1, 6].ToString()) ? "" : worksheet.Cells[1, 6].Value.ToString().Trim();
                    string BatchNo = String.IsNullOrEmpty(worksheet.Cells[1, 7].ToString()) ? "" : worksheet.Cells[1, 7].Value.ToString().Trim();
                    string Status = String.IsNullOrEmpty(worksheet.Cells[1, 8].ToString()) ? "" : worksheet.Cells[1, 8].Value.ToString().Trim();



                    if (AccountName != "AccountName" || Bank != "Bank" || AccountNo != "AccountNo" || Remark != "Remark")
                    {
                        return BadRequest("File not in the Right format");
                    }





                    for (int j = 2; j <= rowCount; j++)
                    {
                        var jp = new ClaimCapture();

                        string svcno = String.IsNullOrEmpty(worksheet.Cells[j, 1].Value.ToString()) ? "" : worksheet.Cells[j, 1].Value.ToString().Trim();
                        string accountname = String.IsNullOrEmpty(worksheet.Cells[j, 2].Value.ToString()) ? "" : worksheet.Cells[j, 2].Value.ToString().Trim();
                        string bank = String.IsNullOrEmpty(worksheet.Cells[j, 3].Value.ToString()) ? "" : worksheet.Cells[j, 3].Value.ToString().Trim();
                        string accountno = String.IsNullOrEmpty(worksheet.Cells[j, 4].Value.ToString()) ? "" : worksheet.Cells[j, 4].Value.ToString().Trim();

                        decimal amount = Convert.ToDecimal(String.IsNullOrEmpty(worksheet.Cells[j, 5].Value.ToString()) ? "" : worksheet.Cells[j, 5].Value.ToString().Trim());
                        string remark = String.IsNullOrEmpty(worksheet.Cells[j, 6].Value.ToString()) ? "" : worksheet.Cells[j, 6].Value.ToString().Trim();
                        string batchno = String.IsNullOrEmpty(worksheet.Cells[j, 7].Value.ToString()) ? "" : worksheet.Cells[j, 7].Value.ToString().Trim();
                        string status = String.IsNullOrEmpty(worksheet.Cells[j, 8].Value.ToString()) ? "" : worksheet.Cells[j, 8].Value.ToString().Trim();


                        if (String.IsNullOrEmpty(worksheet.Cells[j, 1].Value.ToString()) ||
                          String.IsNullOrEmpty(worksheet.Cells[j, 2].Value.ToString()) ||
                          String.IsNullOrEmpty(worksheet.Cells[j, 3].Value.ToString()) ||
                          String.IsNullOrEmpty(worksheet.Cells[j, 4].Value.ToString()) ||
                          String.IsNullOrEmpty(worksheet.Cells[j, 5].Value.ToString()) ||
                          String.IsNullOrEmpty(worksheet.Cells[j, 6].Value.ToString()) ||
                          String.IsNullOrEmpty(worksheet.Cells[j, 7].Value.ToString()) ||
                          String.IsNullOrEmpty(worksheet.Cells[j, 8].Value.ToString()))
                        {
                            jp.svcno = svcno;
                            jp.accountname = accountname;
                            jp.bank = bank;
                            jp.accountno = accountno;
                            jp.amount = amount;
                            jp.remark = remark;
                            jp.batchno = batchno;
                            jp.type = status;

                        }


                        if (worksheet.Cells[j, 1].Value.ToString().Trim() != null)
                        {
                            jp.svcno = worksheet.Cells[j, 1].Value.ToString().Trim();
                            jp.accountname = worksheet.Cells[j, 2].Value.ToString().Trim();
                            jp.bank = worksheet.Cells[j, 3].Value.ToString().Trim();
                            jp.accountno = worksheet.Cells[j, 4].Value.ToString().Trim();
                            jp.amount = Convert.ToDecimal(worksheet.Cells[j, 5].Value.ToString().Trim());
                            jp.remark = worksheet.Cells[j, 6].Value.ToString().Trim();
                            jp.batchno = worksheet.Cells[j, 7].Value.ToString().Trim();
                            jp.type = worksheet.Cells[j, 8].Value.ToString().Trim();


                        }


                        listApplication.Add(jp);

                    }

                    var p = fundService.GetFundTypeCodeByCode(fundTypeCode);

                    List<Npf_ClaimRegister> successRecord = new List<Npf_ClaimRegister>();
                    List<ClaimCapture> errorRecord = new List<ClaimCapture>();
                    string batch = listApplication.FirstOrDefault().batchno;
                    if (claimTypeService.checkClaimBatchnoExist(batch))
                    {
                        TempData["message"] = "Batch number exist";
                    }
                    else
                    {
                        ClaimProcessUpload upload = new ClaimProcessUpload(listApplication, unitOfWork, claimTypeService, fundTypeService);
                        errorRecord = await upload.claimUploadInThread();

                        if (errorRecord.Count > 0)
                        {
                            var stream2 = new MemoryStream();

                            using (var package2 = new ExcelPackage(stream2))
                            {
                                var workSheet = package2.Workbook.Worksheets.Add("Sheet2");
                                workSheet.Cells.LoadFromCollection(errorRecord, true);
                                package2.Save();
                            }
                            stream2.Position = 0;
                            string excelName = $"DFERROR-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";

                            return File(stream2, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
                        }
                        else
                        {
                            successRecord = await upload.claimProcessUploadInThread();
                            return await generatePdf.GetPdf("Views/ClaimType/ReportPage4.cshtml", successRecord);
                        }

                    }





                    //TrialBalanceUpload upk = new TrialBalanceUpload(listApplication, unitOfWork, fundTypeCode, fundTypeId, user, m, year);
                    //var listapplicationofrecordnotavailable = await upk.UploadHistoryInThread();
                    //await upk.TrialbalanceUploadInThread();

                    //TempData["message"] = "Uploaded Successfully";


                    //if (listapplicationofrecordnotavailable.Count > 0)
                    //{

                    //    var stream2 = new MemoryStream();

                    //    using (var package2 = new ExcelPackage(stream2))
                    //    {
                    //        var workSheet = package2.Workbook.Worksheets.Add("Sheet2");
                    //        workSheet.Cells.LoadFromCollection(listapplicationofrecordnotavailable, true);
                    //        package2.Save();
                    //    }
                    //    stream2.Position = 0;
                    //    string excelName = $"Trialbalance-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";

                    //    //return File(stream, "application/octet-stream", excelName);  
                    //    return File(stream2, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
                    //}
                }



            }


            return RedirectToAction("Index");
        }


    }
}
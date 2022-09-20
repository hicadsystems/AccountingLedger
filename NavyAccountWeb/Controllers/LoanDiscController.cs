using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NavyAccountCore.Core.Data;
using NavyAccountCore.Entities;
using NavyAccountCore.Models;
using OfficeOpenXml;
using NavyAccountWeb.IServices;
using Wkhtmltopdf.NetCore;
using NavyAccountWeb.Services;
using NavyAccountCore.Core.AuditService;
using NavyAccountWeb.Models;
using MoreLinq.Extensions;
using Microsoft.AspNetCore.Routing;

namespace NavyAccountWeb.Controllers
{
    [SessionTimeout]
    public class LoanDiscController : Controller
    {
        private readonly ILoandiscService loandiscService;
        private readonly ILoanRegisterService loanRegisterService;
        private readonly ILoanTypeService loantypeService;
        private readonly string _connectionstring;
        private readonly INavyAccountDbContext context;
        private readonly IUnitOfWork unitofWork;
        private readonly IGeneratePdf generatePdf;
        public LoanDiscController(ILoanRegisterService loanRegisterService,ILoanTypeService loantypeService,IUnitOfWork unitofWork, INavyAccountDbContext context, IConfiguration configuration, ILoandiscService loandiscService, IGeneratePdf generatePdf)
        {
            _connectionstring = configuration.GetConnectionString("DefaultConnection");
            this.unitofWork = unitofWork;
            this.context = context;
            this.loandiscService = loandiscService;
            this.loantypeService = loantypeService;
            this.generatePdf = generatePdf;
            this.loanRegisterService = loanRegisterService;
        }

        [HttpGet]
        public async Task<IActionResult> Loanrepayment(string batchNo,string svcno,int? pageNumber)
        {
            if (pageNumber == null)
            {
                pageNumber = 1;
            }
            var loan = loanRegisterService.getListofLoanRegisterByBatchDrp().Result;
            ViewBag.batch = loan.OrderBy(x => x.LoanTypeID);
            var fundcode = HttpContext.Session.GetString("fundtypecode");
            if(batchNo!=null)
            HttpContext.Session.SetString("batchNo", batchNo);
            ViewBag.getbatch = batchNo;
            int? loanid = 0;
             loanid = HttpContext.Session.GetInt32("deleted");
            if (batchNo != null &&  loanid==0 && svcno==null)
            {
                string dd = HttpContext.Session.GetString("fundtypecode").ToString();
                string k = dd;
                using (SqlConnection sqls = new SqlConnection(_connectionstring))
                {
                    using (SqlCommand cmd = new SqlCommand("npf_generate_contr_loanrepay", sqls))
                    {
                        cmd.CommandTimeout = 1200;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@globaluser", User.Identity.Name));
                        cmd.Parameters.Add(new SqlParameter("@fundcode", HttpContext.Session.GetString("fundtypecode")));
                        cmd.Parameters.Add(new SqlParameter("@batchno", batchNo));

                        await sqls.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();

                        TempData["message"] = "Uploaded Successfully";
                    }
                }
                var listloanrepay = loandiscService.GetAllbyFundcode(fundcode,batchNo).OrderByDescending(x => x.principal).AsQueryable();
                var m = PaginatedList<LoandiscVM>.CreateAsync(listloanrepay, (int)pageNumber, 10);
                return View(m);
            }
            HttpContext.Session.SetInt32("deleted",0);
            if (svcno != null)
            {
                var listloanrepay2 = loandiscService.GetAllbyFundcodeandsvcno( fundcode, svcno,batchNo).OrderByDescending(x => x.principal).AsQueryable();
                var m2 = PaginatedList<LoandiscVM>.CreateAsync(listloanrepay2, (int)pageNumber, 10);
                return View(m2);
            }
            else
            {
                var listloanrepay = loandiscService.GetAllbyFundcode(fundcode, batchNo).OrderByDescending(x => x.principal).AsQueryable();
                var m = PaginatedList<LoandiscVM>.CreateAsync(listloanrepay, (int)pageNumber, 10);
                return View(m);
            }
        }
        [HttpGet]
        public async Task<IActionResult> LoanrepaymentByType(string loantype, string svcno, int? pageNumber)
        {
            var getloantype = context.Pf_loanType.ToList();

            ViewBag.getloantype = getloantype;

            if (pageNumber == null)
            {
                pageNumber = 1;
            }
            var loan = loanRegisterService.getListofLoanRegisterByBatchDrp().Result;
            ViewBag.batch = loan.OrderBy(x => x.LoanTypeID);
            var fundcode = HttpContext.Session.GetString("fundtypecode");
            if (loantype != null)
                HttpContext.Session.SetString("batchNo", loantype);
           // ViewBag.getbatch = batchNo;
            int? loanid = 0;
            loanid = HttpContext.Session.GetInt32("deleted");
            if (loantype != null && loanid == 0 && svcno == null)
            {
                string dd = HttpContext.Session.GetString("fundtypecode").ToString();
                string k = dd;
                using (SqlConnection sqls = new SqlConnection(_connectionstring))
                {
                    using (SqlCommand cmd = new SqlCommand("npf_generate_contr_loanrepay", sqls))
                    {
                        cmd.CommandTimeout = 1200;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@globaluser", User.Identity.Name));
                        cmd.Parameters.Add(new SqlParameter("@fundcode", HttpContext.Session.GetString("fundtypecode")));
                        cmd.Parameters.Add(new SqlParameter("@loantype", loantype));

                        await sqls.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();

                        TempData["message"] = "Uploaded Successfully";
                    }
                }
                var listloanrepay = loandiscService.GetAllbyFundcode(fundcode, loantype).OrderByDescending(x => x.principal).AsQueryable();
                var m = PaginatedList<LoandiscVM>.CreateAsync(listloanrepay, (int)pageNumber, 10);
                return View(m);
            }
            HttpContext.Session.SetInt32("deleted", 0);
            if (svcno != null)
            {
                var listloanrepay2 = loandiscService.GetAllbyFundcodeandsvcno(fundcode, svcno, loantype).OrderByDescending(x => x.principal).AsQueryable();
                var m2 = PaginatedList<LoandiscVM>.CreateAsync(listloanrepay2, (int)pageNumber, 10);
                return View(m2);
            }
            else
            {
                var listloanrepay = loandiscService.GetAllbyFundcode(fundcode, loantype).OrderByDescending(x => x.principal).AsQueryable();
                var m = PaginatedList<LoandiscVM>.CreateAsync(listloanrepay, (int)pageNumber, 10);
                return View(m);
            }
        }
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pfloandisc = await context.pf_loandisc.FindAsync(id);
           // await loandiscService.DeleteLoandisc(pfloandisc);
            using (SqlConnection sqls = new SqlConnection(_connectionstring))
            {
                using (SqlCommand cmd = new SqlCommand("deleteloandisc", sqls))
                {
                    cmd.CommandTimeout = 1200;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@id", pfloandisc.Id));
                    cmd.Parameters.Add(new SqlParameter("@batchno", pfloandisc.batchno));

                    await sqls.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();

                    TempData["message"] = "Uploaded Successfully";
                }
            }
            HttpContext.Session.SetInt32("deleted", pfloandisc.Id);
            return RedirectToAction("Loanrepayment", new RouteValueDictionary(
                     new
                     {
                         controller = "LoanDisc",
                         action = "Loanrepayment",
                         batchNo = pfloandisc.batchno
                     }));
           
        }
        [HttpPost]
        public async Task<IActionResult> LoanrepaymentPost(string batchNo)
        {
            string dd = HttpContext.Session.GetString("fundtypecode").ToString();
            string k = dd;
            using (SqlConnection sqls = new SqlConnection(_connectionstring))
            {
                using (SqlCommand cmd = new SqlCommand("npf_generate_contr_loanrepay", sqls))
                {
                    cmd.CommandTimeout = 1200;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@globaluser", User.Identity.Name));
                    cmd.Parameters.Add(new SqlParameter("@fundcode", HttpContext.Session.GetString("fundtypecode")));
                    cmd.Parameters.Add(new SqlParameter("@batchno", batchNo));

                    await sqls.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();

                    TempData["messagedd"] = "Uploaded Successfully";
                }
            }
            return RedirectToAction("Loanrepayment");


        }
        [HttpGet]
        public async Task<IActionResult> variance(int? pageNumber, string batchNo)
        {
            if (batchNo != null)
            {
                using (SqlConnection sqls = new SqlConnection(_connectionstring))
                {

                    using (SqlCommand cmd = new SqlCommand("npf_generate_variance", sqls))
                    {
                        cmd.CommandTimeout = 1200;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@globaluser", User.Identity.Name));
                        cmd.Parameters.Add(new SqlParameter("@fundcode", HttpContext.Session.GetString("fundtypecode")));
                        cmd.Parameters.Add(new SqlParameter("@batchno2", batchNo));


                        await sqls.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();

                    }

                    TempData["message"] = "Uploaded Successfully";
                }
            }
            var loan = loandiscService.getListofLoandiscByBatchDrp().Result;
            ViewBag.batch = loan.OrderBy(x => x.batchno);
            ViewBag.getbatch = batchNo;
            if (pageNumber == null)
            {
                pageNumber = 1;
            }
            var fundcode = HttpContext.Session.GetString("fundtypecode");
            var listloanrepay = loandiscService.GetAllbyFundcode(fundcode, batchNo).OrderByDescending(x => x.principal).AsQueryable();

            var m = PaginatedList<LoandiscVM>.CreateAsync(listloanrepay, (int)pageNumber, 10);
            return View(m);
        }
        [HttpPost]
        public async Task<IActionResult> variance(string batchno)
        {
                using (SqlConnection sqls = new SqlConnection(_connectionstring))
                {
                  
                    using (SqlCommand cmd = new SqlCommand("npf_generate_variance", sqls))
                    {
                        cmd.CommandTimeout = 1200;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@globaluser", User.Identity.Name));
                        cmd.Parameters.Add(new SqlParameter("@fundcode", HttpContext.Session.GetString("fundtypecode")));
                        cmd.Parameters.Add(new SqlParameter("@batchno2", batchno));


                    await sqls.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();

                    }

                   TempData["message"] = "Uploaded Successfully";
               }
            return RedirectToAction("variance");
            
        }

        public IActionResult loadLoanDisc()
        {
            var loan = loanRegisterService.getListofLoanRegisterByBatchDrp().Result;
            ViewBag.batch = loan.OrderBy(x => x.LoanTypeID);
            var con = new ContrDiscUpload
            {
               Batchno = context.pf_loandisc.FirstOrDefault().batchno,
               processingperiod = HttpContext.Session.GetInt32("processingYear") + "" + HttpContext.Session.GetInt32("processingMonth"),
               getloan = loantypeService.GetLoanTypes().ToList()
            };
            return View(con);
        }
        [HttpPost]
        public async Task<IActionResult> loanDiscUpload(IFormFile formFile, CancellationToken cancellationToken,string loanacct,string batchNo)
        {

            if (formFile == null || formFile.Length <= 0)
            {
                TempData["message2"] = "No File Uploaded";
                //return BadRequest("File not an Excel Format");
                return View();
                //return BadRequest("No File Uploaded");
            }

            if (!Path.GetExtension(formFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                TempData["message2"] = "File not an Excel Format";
                //return BadRequest("File not an Excel Format");
                return View();
            }
            string user = User.Identity.Name;
            int fundTypeId = (int)HttpContext.Session.GetInt32("fundtypeid");
            string fundTypeCode = HttpContext.Session.GetString("fundtypecode");

            string processingperiod = HttpContext.Session.GetInt32("processingYear") + "" + HttpContext.Session.GetInt32("processingMonth");
            var listapplication = new List<LoanDiscUploadVM>();
            var listapplicationofrecordnotavailable = new List<LoanDiscUploadVM>();

            using (var stream = new MemoryStream())
            {
                await formFile.CopyToAsync(stream, cancellationToken);

                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets["Sheet1"];
                    var rowCount = worksheet.Dimension.Rows;

                    if (worksheet.Cells[1, 1].Value == null)
                        worksheet.Cells[1, 1].Value = "";

                    if (worksheet.Cells[1, 2].Value == null)
                        worksheet.Cells[1, 2].Value = "";

                    if (worksheet.Cells[1, 3].Value == null)
                        worksheet.Cells[1, 3].Value = "";



                    string svcno = String.IsNullOrEmpty(worksheet.Cells[1, 1].ToString()) ? "" : worksheet.Cells[1, 1].Value.ToString().Trim();
                    string amount = String.IsNullOrEmpty(worksheet.Cells[1, 2].ToString()) ? "" : worksheet.Cells[1, 2].Value.ToString().Trim();
                    string batch = String.IsNullOrEmpty(worksheet.Cells[1, 3].ToString()) ? "" : worksheet.Cells[1, 3].Value.ToString().Trim();



                    if (svcno.ToLower() != "svc_no" || amount.ToLower() != "amount")
                    {
                        return BadRequest("File not in the Right format");
                    }
                    for (int row = 2; row <= rowCount; row++)
                    {
                        if (worksheet.Cells[row, 1].Value == null)
                            worksheet.Cells[row, 1].Value = "";

                        if (worksheet.Cells[row, 2].Value == null)
                            worksheet.Cells[row, 2].Value = "";

                        if (worksheet.Cells[row, 3].Value == null)
                            worksheet.Cells[row, 3].Value = "";



                        string svcnoVal = String.IsNullOrEmpty(worksheet.Cells[row, 1].Value.ToString()) ? "" : worksheet.Cells[row, 1].Value.ToString().Trim();
                        decimal amountVal = String.IsNullOrEmpty(worksheet.Cells[row, 2].Value.ToString()) ? 0 : Decimal.Parse(worksheet.Cells[row, 2].Value.ToString().Trim());
                        string batchval = String.IsNullOrEmpty(worksheet.Cells[row, 3].Value.ToString()) ? "" : worksheet.Cells[row, 3].Value.ToString().Trim();


                        if (String.IsNullOrEmpty(worksheet.Cells[row, 1].Value.ToString()) ||
                           String.IsNullOrEmpty(worksheet.Cells[row, 2].Value.ToString()))
                        {
                            listapplicationofrecordnotavailable.Add(new LoanDiscUploadVM
                            {
                                svcno = svcnoVal,
                                amount = amountVal,
                                batchno=batchval

                            });

                        }
                        else
                        {
                            //check if already in the list -- a possibility
                            listapplication.Add(new LoanDiscUploadVM
                            {
                                svcno = svcnoVal,
                                amount = amountVal,
                                batchno=batchval

                            });
                        }

                    }

                    

                    var proyear = context.npffundType.FirstOrDefault().processingYear;
                    var promonth = context.npffundType.FirstOrDefault().processingMonth;
                    var priod = proyear.ToString() +""+ promonth.ToString();
                    var checkbatch = context.pf_loandisc.FirstOrDefault().batchno;
                    if (checkbatch != batchNo)
                    {
                        TempData["message2"] = "Batch Mixmatch";
                        return RedirectToAction("loadLoanDisc");

                    }
                    else if (priod.ToString() != processingperiod)
                    {
                        TempData["message2"] = "It Seems You Have Not Done Month End. Process Month End and Try Again";
                        return RedirectToAction("loadLoanDisc");
                    }
                    else
                    {
                        ProcesLoanDiscUpload procesUpload2 = new ProcesLoanDiscUpload(listapplication, unitofWork, fundTypeId, fundTypeCode, user, processingperiod, loanacct, batchNo);
                        await procesUpload2.processUploadInThread();
                        TempData["message2"] = "Uploaded Successfully";
                    }
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
            return RedirectToAction("loadLoanDisc");
        }


        public ActionResult UpdateRepayment()
        {
            var loan = loandiscService.getListofLoandiscByBatchDrp().Result;
            ViewBag.batch = loan.OrderBy(x => x.batchno);

            var fundcode = HttpContext.Session.GetString("fundtypecode");
            var listloanrepay = loandiscService.GetAllbyFundcode(fundcode);
            return View(listloanrepay);
        }
        [HttpGet]
        public async Task<IActionResult> Discrepancy(int? pageNumber, string batchNo)
        {
            var loan = loandiscService.getListofLoandiscByBatchDrp().Result;
            ViewBag.batch = loan.OrderBy(x =>x.batchno);
            
            ViewBag.getbatch = batchNo;
          
            if (batchNo != null) {
                using (SqlConnection sqls = new SqlConnection(_connectionstring))
                {
                    using (SqlCommand cmd = new SqlCommand("npf_generate_Discrepancy", sqls))
                    {
                        cmd.CommandTimeout = 1200;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@globaluser", User.Identity.Name));
                        cmd.Parameters.Add(new SqlParameter("@fundcode", HttpContext.Session.GetString("fundtypecode")));
                        cmd.Parameters.Add(new SqlParameter("@batchno", batchNo));
                        //push
                        await sqls.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();

                    }
                }
            }
            if (pageNumber == null)
            {
                pageNumber = 1;
            }
            var fundcode = HttpContext.Session.GetString("fundtypecode");
            var listloanrepay = loandiscService.GetAllbyFundcode(fundcode, batchNo).OrderByDescending(x => x.principal).AsQueryable();

            var m = PaginatedList<LoandiscVM>.CreateAsync(listloanrepay, (int)pageNumber, 10);
            return View(m);

        }
        //[HttpPost]
        //public async Task<IActionResult> Discrepancy(string batchno)
        //{
        //    using (SqlConnection sqls = new SqlConnection(_connectionstring))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("npf_generate_Discrepancy", sqls))
        //        {
        //            cmd.CommandTimeout = 1200;
        //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //            cmd.Parameters.Add(new SqlParameter("@globaluser", User.Identity.Name));
        //            cmd.Parameters.Add(new SqlParameter("@fundcode", HttpContext.Session.GetString("fundtypecode")));
        //            cmd.Parameters.Add(new SqlParameter("@batchno", batchno));
        //            //push
        //            await sqls.OpenAsync();
        //           await cmd.ExecuteNonQueryAsync();

        //        }
        //    }
        //    var fundcode = HttpContext.Session.GetString("fundtypecode");
        //    var listloanrepay = loandiscService.GetAllbyFundcode(fundcode);
        //    return View(listloanrepay);
        //}

       
        [HttpPost]
        public async Task<IActionResult> Loanledgerupdate(string batchno)
        {
            using (SqlConnection sqls = new SqlConnection(_connectionstring))
            {
                using (SqlCommand cmd = new SqlCommand("npf_update_loancontr", sqls))
                {
                    cmd.CommandTimeout = 1200;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@globaluser", User.Identity.Name));
                    cmd.Parameters.Add(new SqlParameter("@fundcode", HttpContext.Session.GetString("fundtypecode")));
                    cmd.Parameters.Add(new SqlParameter("@batchnos", batchno));

                    await sqls.OpenAsync();
                   await cmd.ExecuteNonQueryAsync();

                }
            }

            TempData["message"] = "Uploaded Successfully";
            return RedirectToAction("UpdateRepayment");


        }
        [HttpPost]
        public async Task<IActionResult> Loanledgervariance()
        {
            using (SqlConnection sqls = new SqlConnection(_connectionstring))
            {
                using (SqlCommand cmd = new SqlCommand("npf_generate_variance", sqls))
                {
                    cmd.CommandTimeout = 1200;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@globaluser", User.Identity.Name));
                    cmd.Parameters.Add(new SqlParameter("@fundcode", HttpContext.Session.GetString("fundtypecode")));

                   await sqls.OpenAsync();
                  await cmd.ExecuteNonQueryAsync();

                }
            }

            TempData["message"] = "Uploaded Successfully";
            return RedirectToAction("variance");
        }

       


        [HttpGet]
        public async Task<IActionResult> printloandisc(string batchNo)
        {
            var fundcode = HttpContext.Session.GetString("fundtypecode");
            var listloanrepay = loandiscService.GetAllbyFundcode(fundcode,batchNo).OrderByDescending(x => x.principal);

            return await generatePdf.GetPdf("Views/LoanDisc/loanRepaymentReport.cshtml", listloanrepay);
        }
        public async Task<IActionResult> printloandiscVariance(string batchNo)
        {
            var fundcode = HttpContext.Session.GetString("fundtypecode");
            var listloanrepay = loandiscService.GetAllbyFundcode(fundcode, batchNo).OrderByDescending(x => x.principal);

            return await generatePdf.GetPdf("Views/LoanDisc/LoanVarianceReport.cshtml", listloanrepay);
        }
        public async Task<IActionResult> printloandiscdiscrepancy(string batchNo)
        {
            var fundcode = HttpContext.Session.GetString("fundtypecode");
            var listloanrepay = loandiscService.GetAllbyFundcode(fundcode, batchNo).OrderByDescending(x => x.principal);
            if (listloanrepay.Count()!=0)
            {
                return await generatePdf.GetPdf("Views/LoanDisc/LoanDiscrepancyReport.cshtml", listloanrepay);
            }
            return RedirectToAction("Discrepancy");
        }
        public ActionResult LoanStatus()
        {
            return View();
        }
        }
}
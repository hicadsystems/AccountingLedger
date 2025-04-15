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

namespace NavyAccountWeb.Controllers
{
    [SessionTimeout]
    public class ContrDiscController : Controller
    {
        private readonly IContrService contrService;
        private readonly string _connectionstring;
        private readonly INavyAccountDbContext context;
        private readonly IUnitOfWork unitofWork;
        private readonly IGeneratePdf generatePdf;
        private readonly IFundTypeService fundTypeService;
        public ContrDiscController(IFundTypeService fundTypeService,IUnitOfWork unitofWork, INavyAccountDbContext context, IConfiguration configuration, IContrService contrService, IGeneratePdf generatePdf)
        {
            _connectionstring = configuration.GetConnectionString("DefaultConnection");
            this.unitofWork = unitofWork;
            this.context = context;
            this.contrService = contrService;
            this.generatePdf = generatePdf;
            this.fundTypeService = fundTypeService;
        }
        public IActionResult loadLoanDisc()
        {
            return View();
        }

        public async Task<IActionResult> variance()
        {
            using (SqlConnection sqls = new SqlConnection(_connectionstring))
            {

                using (SqlCommand cmd = new SqlCommand("npf_generate_variance_contr", sqls))
                {
                    cmd.CommandTimeout = 1200;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@globaluser", User.Identity.Name));
                    cmd.Parameters.Add(new SqlParameter("@fundcode", HttpContext.Session.GetString("fundtypecode")));

                    await sqls.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();

                }

                TempData["message"] = "Uploaded Successfully";
            }

            var fundcode = HttpContext.Session.GetString("fundtypecode");
            var listloanrepay = contrService.GetAllbyFundcode(fundcode);
            return View(listloanrepay);
        }



        public ActionResult Update()
        {
            var fundcode = HttpContext.Session.GetString("fundtypecode");
            var listloanrepay = contrService.GetAllbyFundcode(fundcode);
            return View(listloanrepay);
        }
        public async Task<IActionResult> Discrepancy()
        {
            using (SqlConnection sqls = new SqlConnection(_connectionstring))
            {
                using (SqlCommand cmd = new SqlCommand("npf_generate_Discrepancy_contr", sqls))
                {
                    cmd.CommandTimeout = 1200;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@globaluser", User.Identity.Name));
                    cmd.Parameters.Add(new SqlParameter("@fundcode", HttpContext.Session.GetString("fundtypecode")));
                    //push
                    await sqls.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();

                }
            }
            var fundcode = HttpContext.Session.GetString("fundtypecode");
            var listloanrepay = contrService.GetAllbyFundcode(fundcode);
            return View(listloanrepay);
        }
        public IActionResult contributiongen()
        {

            using (SqlConnection sqls = new SqlConnection(_connectionstring))
            {
                using (SqlCommand cmd = new SqlCommand("npf_generate_contribution", sqls))
                {
                    cmd.CommandTimeout = 1200;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@globaluser", User.Identity.Name));
                    cmd.Parameters.Add(new SqlParameter("@fundcode", HttpContext.Session.GetString("fundtypecode")));

                     sqls.OpenAsync();
                     cmd.ExecuteNonQueryAsync();

                    TempData["message"] = "Uploaded Successfully";
                }
            }
            var fundcode = HttpContext.Session.GetString("fundtypecode");
            var listloanrepay = contrService.GetAllbyFundcode(fundcode);
            return View(listloanrepay);
        }

        [HttpPost]
        public async Task<IActionResult> GenContributionPost()
        {
            using (SqlConnection sqls = new SqlConnection(_connectionstring))
            {
                using (SqlCommand cmd = new SqlCommand("npf_generate_contribution", sqls))
                {
                    cmd.CommandTimeout = 1200;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@globaluser", User.Identity.Name));
                    cmd.Parameters.Add(new SqlParameter("@fundcode", HttpContext.Session.GetString("fundtypecode")));

                    await sqls.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();

                    TempData["message"] = "Uploaded Successfully";
                }
            }
            return RedirectToAction("Loanrepayment");


        }
        [HttpPost]
        public async Task<IActionResult> contrledgerupdate()
        {
            var fundcode = HttpContext.Session.GetString("fundtypecode");
            var listloanrepay = contrService.GetAllbyFundcode(fundcode);

            var fcode = unitofWork.fundTypeCode.Getfundtypebycode(c=>c.Code==fundcode);
            string period = fcode.processingYear.ToString() + fcode.processingMonth.ToString();
            var contr = unitofWork.npf_contrdisc.Getbyfundcode(fundcode).ToList();
            var wgroup = contr.FirstOrDefault().groupcode;
            
            string chequeno = period + contr.FirstOrDefault().groupcode.Substring(0,4);
            decimal tamt = 0;
            foreach (var dd in contr)
            {
              

                if (wgroup != dd.groupcode)
                {

                    //decimal tamt = 0;
                    using (SqlConnection sqls = new SqlConnection(_connectionstring))
                    {
                        using (SqlCommand cmd = new SqlCommand("npf_Update_ledgerDB", sqls))
                        {
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@globaluser", User.Identity.Name));
                            cmd.Parameters.Add(new SqlParameter("@fundcode", fundcode));
                            cmd.Parameters.Add(new SqlParameter("@Doctype", "FUND"));
                            cmd.Parameters.Add(new SqlParameter("@Debitcode", dd.trusteeact));
                            cmd.Parameters.Add(new SqlParameter("@Debitamt", tamt));
                            cmd.Parameters.Add(new SqlParameter("@Docdate", DateTime.Now));
                            cmd.Parameters.Add(new SqlParameter("@docRem", "Fund Contribution"));
                            cmd.Parameters.Add(new SqlParameter("@Docno", chequeno));
                            cmd.Parameters.Add(new SqlParameter("@Refno", '0'));



                            await sqls.OpenAsync();
                            await cmd.ExecuteNonQueryAsync();
                            

                            wgroup = dd.groupcode;

                        }
                    }
                }

                using (SqlConnection sqls = new SqlConnection(_connectionstring))
                {
                    using (SqlCommand cmd = new SqlCommand("npf_Update_ledgerCR", sqls))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@globaluser", User.Identity.Name));
                        cmd.Parameters.Add(new SqlParameter("@fundcode", fundcode));
                        cmd.Parameters.Add(new SqlParameter("@Doctype", "FUND"));
                        cmd.Parameters.Add(new SqlParameter("@Creditcode", dd.contract));
                        cmd.Parameters.Add(new SqlParameter("@Creditamt", dd.amountpay));
                        cmd.Parameters.Add(new SqlParameter("@Docdate", DateTime.Now));
                        cmd.Parameters.Add(new SqlParameter("@docRem", "Fund Contribution"));
                        cmd.Parameters.Add(new SqlParameter("@Docno", chequeno));
                        cmd.Parameters.Add(new SqlParameter("@Refno", "0"));



                        await sqls.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();

                    }
                }
               tamt = tamt + dd.amountpay;
            }


            using (SqlConnection sqls = new SqlConnection(_connectionstring))
            {
                using (SqlCommand cmd = new SqlCommand("npf_Update_ledgerDB", sqls))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@globaluser", User.Identity.Name));
                    cmd.Parameters.Add(new SqlParameter("@fundcode", fundcode));
                    cmd.Parameters.Add(new SqlParameter("@Doctype", "FUND"));
                    cmd.Parameters.Add(new SqlParameter("@Debitcode", contr.FirstOrDefault().trusteeact));
                    cmd.Parameters.Add(new SqlParameter("@Debitamt", tamt));
                    cmd.Parameters.Add(new SqlParameter("@Docdate", DateTime.Now));
                    cmd.Parameters.Add(new SqlParameter("@docRem", "Fund Contribution"));
                    cmd.Parameters.Add(new SqlParameter("@Docno", chequeno));
                    cmd.Parameters.Add(new SqlParameter("@Refno", '0'));



                    await sqls.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                 

                }
            }

            TempData["message"] = "Uploaded Successfully";
            return RedirectToAction("Update");


        }
        [HttpPost]
        public async Task<IActionResult> contrledgerupdate2()
        {
            var fundcode = HttpContext.Session.GetString("fundtypecode");
            var listloanrepay = contrService.GetAllbyFundcode(fundcode);

            var fcode = fundTypeService.GetFundTypes().ToList();
            foreach (var dd in fcode)
            {

                    //decimal tamt = 0;
                    using (SqlConnection sqls = new SqlConnection(_connectionstring))
                    {
                    using (SqlCommand cmd = new SqlCommand("npf_update_contr2", sqls))
                        {
                            cmd.CommandTimeout = 1200;
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@globaluser", User.Identity.Name));
                            cmd.Parameters.Add(new SqlParameter("@fundcode", fundcode));
                            cmd.Parameters.Add(new SqlParameter("@funds", dd.fundacct));

                            await sqls.OpenAsync();
                            await cmd.ExecuteNonQueryAsync();

                        }
                    }
            }
            TempData["message"] = "Uploaded Successfully";
            return RedirectToAction("Update");
        }

        public async Task<IActionResult> printloandisc()
        {
            var fundcode = HttpContext.Session.GetString("fundtypecode");
            var listloanrepay = contrService.GetAllbyFundcode(fundcode);

            return await generatePdf.GetPdf("Views/ContrDisc/LoanRepayReport.cshtml", listloanrepay);
        }
        public async Task<IActionResult> printloandiscVariance()
        {
            var fundcode = HttpContext.Session.GetString("fundtypecode");
            var listloanrepay = contrService.GetAllbyFundcode(fundcode);

            return await generatePdf.GetPdf("Views/ContrDisc/LoanVarianceReport.cshtml", listloanrepay);
        }
        public async Task<IActionResult> printloandiscdiscrepancy()
        {
            var fundcode = HttpContext.Session.GetString("fundtypecode");
            var listloanrepay = contrService.GetAllbyFundcode(fundcode);

            return await generatePdf.GetPdf("Views/ContrDisc/LoanDiscrepancyReport.cshtml", listloanrepay);
        }
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pfloandisc = await context.npf_contrdisc.FindAsync(id);
            await contrService.DeleteLoandisc(pfloandisc);

            return RedirectToAction("Loanrepayment");
        }

        public IActionResult loadContrDisc()
        {
            var con = new ContrDiscUpload
            {
                getfund = fundTypeService.GetFundTypes().ToList()
            };
            return View(con);
        }
        [HttpPost]
        public async Task<IActionResult> ContrDiscUpload(IFormFile formFile, CancellationToken cancellationToken,string Contraccount,string batchNo)
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



                    string svcno = String.IsNullOrEmpty(worksheet.Cells[1, 1].ToString()) ? "" : worksheet.Cells[1, 1].Value.ToString().Trim();
                    string amount = String.IsNullOrEmpty(worksheet.Cells[1, 2].ToString()) ? "" : worksheet.Cells[1, 2].Value.ToString().Trim();



                    if (svcno.ToLower() != "svcno" || amount.ToLower() != "amount")
                    {
                        return BadRequest("File not in the Right format");
                    }
                    for (int row = 2; row <= rowCount; row++)
                    {
                        if (worksheet.Cells[row, 1].Value == null)
                            worksheet.Cells[row, 1].Value = "";

                        if (worksheet.Cells[row, 2].Value == null)
                            worksheet.Cells[row, 2].Value = "";

                   

                        string svcnoVal = String.IsNullOrEmpty(worksheet.Cells[row, 1].Value.ToString()) ? "" : worksheet.Cells[row, 1].Value.ToString().Trim();
                        decimal amountVal = String.IsNullOrEmpty(worksheet.Cells[row, 2].Value.ToString()) ? 0 : Decimal.Parse(worksheet.Cells[row, 2].Value.ToString().Trim());
                        

                        if (String.IsNullOrEmpty(worksheet.Cells[row, 1].Value.ToString()) ||
                           String.IsNullOrEmpty(worksheet.Cells[row, 2].Value.ToString()))
                            {
                            listapplicationofrecordnotavailable.Add(new LoanDiscUploadVM
                            {
                                svcno = svcnoVal,
                                amount = amountVal
                                
                            });

                        }
                        else
                        {
                            //check if already in the list -- a possibility
                            listapplication.Add(new LoanDiscUploadVM
                            {
                                svcno = svcnoVal,
                                amount = amountVal

                            });
                        }

                    }


                    ProcesLoanDiscUpload procesUpload2 = new ProcesLoanDiscUpload(listapplication, unitofWork, fundTypeId, fundTypeCode, user, processingperiod, Contraccount);
                    procesUpload2.processUploadInThreadContr();
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
            return RedirectToAction("loadContrDisc");
        }

    }
}
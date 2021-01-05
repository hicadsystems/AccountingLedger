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
using NavyAccountCore.Core.AuditService;
using NavyAccountCore.Core.Data;
using NavyAccountCore.Models;
using NavyAccountWeb.IServices;
using NavyAccountWeb.Services;
using OfficeOpenXml;

namespace NavyAccountWeb.Controllers
{
    [SessionTimeout]
    public class TrialBalanceUploadController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly string _connectionString;
        private readonly INavyAccountDbContext context;
        private readonly IFundTypeCodeService fundService;

        public TrialBalanceUploadController(IUnitOfWork unitOfWork,IConfiguration configuration, INavyAccountDbContext context, IFundTypeCodeService fundService)
        {
            this.unitOfWork = unitOfWork;
            this.context = context;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            this.fundService = fundService;

        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(IFormFile formFile, CancellationToken cancellationToken)
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

            var listApplication = new List<TrialBalanceCapture>();
            int fundTypeId = (int)HttpContext.Session.GetInt32("fundtypeid");
            string fundTypeCode = HttpContext.Session.GetString("fundtypecode");
            using (var stream = new MemoryStream())
            {
                await formFile.CopyToAsync(stream, cancellationToken);

                using (var package = new ExcelPackage(stream))
                {

                    ExcelWorksheet worksheet = package.Workbook.Worksheets["Sheet1"];
                    var rowCount = worksheet.Dimension.Rows;

                    string Code = String.IsNullOrEmpty(worksheet.Cells[1, 2].ToString()) ? "" : worksheet.Cells[1, 2].Value.ToString().Trim();
                    string Description = String.IsNullOrEmpty(worksheet.Cells[1, 3].ToString()) ? "" : worksheet.Cells[1, 3].Value.ToString().Trim();
                    string Debit = String.IsNullOrEmpty(worksheet.Cells[1, 4].ToString()) ? "" : worksheet.Cells[1, 4].Value.ToString().Trim();
                    string Credit = String.IsNullOrEmpty(worksheet.Cells[1, 5].ToString()) ? "" : worksheet.Cells[1, 5].Value.ToString().Trim();
                    string Batchno = String.IsNullOrEmpty(worksheet.Cells[1, 6].ToString()) ? "" : worksheet.Cells[1, 6].Value.ToString().Trim();


                    if (Code != "Code" || Description != "Description" || Debit != "Debit" || Credit != "Credit")
                    {
                        return BadRequest("File not in the Right format");
                    }

                    using (SqlConnection sqls = new SqlConnection(_connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("DropNPFLedger", sqls))
                        {
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            //push problem
                            sqls.Open();
                            cmd.ExecuteNonQuery();
                        }

                    }



                    for (int j = 2; j <= rowCount; j++)
                    {
                        var jp = new TrialBalanceCapture();

                        string code = String.IsNullOrEmpty(worksheet.Cells[j, 2].Value.ToString()) ? "" : worksheet.Cells[j, 2].Value.ToString().Trim();
                        string description = String.IsNullOrEmpty(worksheet.Cells[j, 3].Value.ToString()) ? "" : worksheet.Cells[j, 3].Value.ToString().Trim();
                        decimal credit = String.IsNullOrEmpty(worksheet.Cells[j, 4].Value.ToString()) ? 0M : Convert.ToDecimal(worksheet.Cells[j,4].Value.ToString().Trim());
                        decimal debit = String.IsNullOrEmpty(worksheet.Cells[j, 5].Value.ToString()) ? 0M : Convert.ToDecimal(worksheet.Cells[j, 5].Value.ToString().Trim());
                        string batchno = String.IsNullOrEmpty(worksheet.Cells[j, 6].Value.ToString()) ? "" : worksheet.Cells[j, 6].Value.ToString().Trim();

                        if (String.IsNullOrEmpty(worksheet.Cells[j, 2].Value.ToString()) ||
                          String.IsNullOrEmpty(worksheet.Cells[j, 3].Value.ToString()) ||
                          String.IsNullOrEmpty(worksheet.Cells[j, 4].Value.ToString()) ||
                          String.IsNullOrEmpty(worksheet.Cells[j, 5].Value.ToString()))
                        {
                            jp.ACCode = code;
                            jp.CREDIT = credit;
                            jp.DEBIT = debit;
                            jp.description = description;
                            jp.BatchNo = batchno;
                            
                        }

                            
                        if (worksheet.Cells[j, 2].Value.ToString().Trim() != null)
                        {
                            jp.ACCode = worksheet.Cells[j, 2].Value.ToString().Trim();
                            jp.CREDIT = CheckIsNull(worksheet.Cells[j, 5].Value);
                            jp.DEBIT = CheckIsNull(worksheet.Cells[j, 4].Value);
                            jp.description = worksheet.Cells[j, 3].Value.ToString().Trim(); 
                            jp.BatchNo= worksheet.Cells[j, 6].Value.ToString().Trim();
                        }
                       
                        
                        listApplication.Add(jp);
                    }
                    var p = fundService.GetFundTypeCodeByCode(fundTypeCode);
                    string m = getMonth2(p.processingMonth);
                    

                    string year = p.processingYear.ToString();

                    TrialBalanceUpload upk = new TrialBalanceUpload(listApplication,unitOfWork,fundTypeCode,fundTypeId,user,m,year);
                    var listapplicationofrecordnotavailable =await upk.UploadHistoryInThread();
                    await upk.TrialbalanceUploadInThread();

                    TempData["message"] = "Uploaded Successfully";


                     if (listapplicationofrecordnotavailable.Count > 0)
                    {

                        var stream2 = new MemoryStream();

                        using (var package2 = new ExcelPackage(stream2))
                        {
                            var workSheet = package2.Workbook.Worksheets.Add("Sheet2");
                            workSheet.Cells.LoadFromCollection(listapplicationofrecordnotavailable, true);
                            package2.Save();
                        }
                        stream2.Position = 0;
                        string excelName = $"Trialbalance-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";

                        //return File(stream, "application/octet-stream", excelName);  
                        return File(stream2, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
                    }
                }



            }
          

            return RedirectToAction("Index");
        }

        

        private decimal CheckIsNull(object value)
        {
            decimal result = 0M;
            if (value==null)
            {
                result = 0M;
            }
            else
            {
                result = Convert.ToDecimal(value);
            }

            return result;
        }

        public string getMonth2(int month)
        {
            string result = "";

            if (month == 1)
                result = "01";
            if (month == 2)
                result = "02";
            if (month == 3)
                result = "03";
            if (month == 4)
                result = "04";
            if (month == 5)
                result = "05";
            if (month == 6)
                result = "06";
            if (month == 7)
                result = "07";
            if (month == 8)
                result = "08";
            if (month == 9)
                result = "09";
            if (month == 10)
                result = "10";
            if (month == 11)
                result = "11";
            if (month == 12)
                result = "12";

            return result;
        }
    }
}
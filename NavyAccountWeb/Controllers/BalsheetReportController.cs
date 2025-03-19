using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NavyAccountCore.Models;
using NavyAccountWeb.IServices;
using NavyAccountWeb.ViewModels;
using OfficeOpenXml;
using Wkhtmltopdf.NetCore;

namespace NavyAccountWeb.Controllers
{
    public class BalsheetReportController : Controller
    {
        private readonly IMainAccountService _mainAccountService;
        private readonly ISurplusService service;
        private readonly IFundTypeCodeService fundService;
        private readonly IGeneratePdf generatePdf;

        public BalsheetReportController(IFundTypeCodeService fundService,IMainAccountService mainAccountService, ISurplusService service, IGeneratePdf generatePdf)
        {
            this.fundService = fundService;
            this.service = service;
            this._mainAccountService = mainAccountService;
            this.generatePdf = generatePdf;
        }

        public IActionResult Index()
        {
            string fundTypeCode = HttpContext.Session.GetString("fundtypecode");
            var p = fundService.GetFundTypeCodeByCode(fundTypeCode);
            int y = p.processingYear;

            var jk = new AuditViewModel
            {
                year = y.ToString()
            };

            return View(jk);
        }

        public IActionResult trialbalance()
        {
            string fundTypeCode = HttpContext.Session.GetString("fundtypecode");
            var p = fundService.GetFundTypeCodeByCode(fundTypeCode);
            int y = p.processingYear;

            var jk = new AuditViewModel
            {
                year = y.ToString()
            };

            return View(jk);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> trialbalance(AuditViewModel model)
        {

            LedgersView2 mainAcct = new LedgersView2();
            mainAcct.mainAccountModel = _mainAccountService.GetMainAccounts();

            var result = new List<V_TRIALBALANCE>();
            string fundTypeCode = HttpContext.Session.GetString("fundtypecode");
            var p = fundService.GetFundTypeCodeByCode(fundTypeCode);
            int m = p.processingMonth;
            int y = p.processingYear;

            string fperiod = model.year;
            string fundperiod = y.ToString();

            if (fperiod == fundperiod)
            {
                if(model.excel == true)
                {
                    var exc = service.GetTrialBalanceProcedure(fundTypeCode).ToList();

                    var stream = new MemoryStream();
                    
                    int row = 2;
                    using (var package = new ExcelPackage(stream))
                    {
                        var workSheet = package.Workbook.Worksheets.Add("Sheet2");
                        workSheet.Cells.LoadFromCollection(exc, true);
                        package.Save();
                    }

                    string excelname = "TrialBalance.xlsx";

                    stream.Position = 0;
                    string excelName = $"LoanSchedule-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
                    return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelname);
                }

                else
                {
                    //get list from surplus or deficit nd get total
                    var res = service.GetAllSurplus();
                    decimal total2 = GroupBybalsheetcode(res.ToList());

                    //result = service.GetTrialBalanceView().ToList();

                    result = service.GetTrialBalanceProcedure(fundTypeCode).ToList();

                    //result = service.GetBalSheet_TrialBalance().ToList();
                    //result.Insert(0, mainAcct);
                    return await generatePdf.GetPdf("Views/BalsheetReport/BalsheetReport_trialbalance.cshtml", result);
                }
            }

            return View();

        }

        public IActionResult maintrialbalance()
        {
            string fundTypeCode = HttpContext.Session.GetString("fundtypecode");
            var p = fundService.GetFundTypeCodeByCode(fundTypeCode);
            int y = p.processingYear;

            var jk = new AuditViewModel
            {
                year = y.ToString()
            };

            return View(jk);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> maintrialbalance(AuditViewModel model)
        {


            var result = new List<V_TRIALBALANCE>();
            string fundTypeCode = HttpContext.Session.GetString("fundtypecode");
            var p = fundService.GetFundTypeCodeByCode(fundTypeCode);
            int m = p.processingMonth;
            int y = p.processingYear;

            string fperiod = model.year;
            string fundperiod = y.ToString();

            if (fperiod == fundperiod)
            {
                if (model.excel == true)
                {
                    var exc2 = service.GetBalSheet_MainTrialBalanceProcedure(fundTypeCode).ToList();

                    var stream = new MemoryStream();

                    int row = 2;
                    using (var package = new ExcelPackage(stream))
                    {
                        var workSheet = package.Workbook.Worksheets.Add("Sheet2");
                        workSheet.Cells.LoadFromCollection(exc2, true);
                        package.Save();
                    }

                    string excelname = "MainTrialBalance.xlsx";

                    stream.Position = 0;
                    string excelName = $"LoanSchedule-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
                    return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelname);
                }

                else
                {
                    //get list from surplus or deficit nd get total
                    var res = service.GetAllSurplus();
                    decimal total2 = GroupBybalsheetcode(res.ToList());


                    result = service.GetBalSheet_MainTrialBalanceProcedure(fundTypeCode).ToList();
                    return await generatePdf.GetPdf("Views/BalsheetReport/BalsheetReport_maintrialbalance.cshtml", result);
                }
                
            }

            return View();


            //    decimal total3 = 0M;
            //    var result2 = GroupBybalsheetcode2(result.ToList());

            //    var result3 = new List<LedgersView2>();
            //    var result4 = new List<ReportViewModel4>();





            //    foreach (var j in result2)
            //    {
            //        foreach (var k in result)
            //        {
            //            if (j.balSheetCode == k.balSheetCode)
            //            {
            //                var z = new LedgersView2
            //                {
            //                    description = k.description,
            //                    Amount = k.Amount,
            //                    acctcode = k.balSheetCode
            //                };

            //                result3.Add(z);
            //            }

            //        }
            //        var fh = new ReportViewModel4
            //        {
            //            bl_desc = j.bl_desc,
            //            code = j.balSheetCode,
            //            tolly = result3,
            //            total = (j.Amount == null) ? 0M : (decimal)j.Amount
            //        };

            //        result4.Add(fh);
            //        total3 += fh.total;

            //    }


            //    gh.deficit = total3;
            //    gh.colly = result4;

            //    return await generatePdf.GetPdf("Views/BalsheetReport/BalsheetReport.cshtml", gh);
            //}
            //else
            //{
            //    string year = model.year;
            //    string wdoc = "Open" + fundTypeCode + year.Substring(2, 2);

            //    var res = service.GetAllSurplus2();
            //    var res2 = res.ToList();
            //    //       res2 = Refractlist(res2, wdoc);
            //    decimal total2 = GroupBybalsheetcode(res2.ToList());


            //    var result = service.GetBalSheet2();
            //    var resultM = Refractlist(result.ToList(), wdoc);
            //    var resultj = resultM.ToList();
            //    var r2 = FilterRecord(resultj, total2).ToList();


            //    resultj.AddRange(r2);
            //    decimal total3 = 0M;
            //    var result2 = GroupBybalsheetcode2(resultj);

            //    var result3 = new List<LedgersView2>();
            //    var result4 = new List<ReportViewModel4>();

            //    foreach (var j in result2)
            //    {
            //        foreach (var k in resultj)
            //        {
            //            if (j.balSheetCode == k.balSheetCode)
            //            {
            //                var z = new LedgersView2
            //                {
            //                    description = k.MDesc,
            //                    Amount = k.Amount,
            //                    acctcode = k.balSheetCode
            //                };

            //                result3.Add(z);
            //            }

            //        }
            //        var fh = new ReportViewModel4
            //        {
            //            bl_desc = j.bl_desc,
            //            code = j.balSheetCode,
            //            tolly = result3,
            //            total = (j.Amount == null) ? 0M : (decimal)j.Amount
            //        };

            //        result4.Add(fh);
            //        total3 += fh.total;

            //    }


            //    gh.deficit = total3;
            //    gh.colly = result4;

            //    return await generatePdf.GetPdf("Views/BalsheetReport/BalsheetReport.cshtml", gh);



        }

        public IActionResult suplusanddeficit()
        {
            string fundTypeCode = HttpContext.Session.GetString("fundtypecode");
            var p = fundService.GetFundTypeCodeByCode(fundTypeCode); ;
            int y = p.processingYear;

            var jk = new AuditViewModel
            {
                year = y.ToString()
            };

            return View(jk);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> suplusanddeficit(AuditViewModel model)
        {


            var result = new List<LedgersView2>();
            string fundTypeCode = HttpContext.Session.GetString("fundtypecode");
            var p = fundService.GetFundTypeCodeByCode(fundTypeCode);
            int m = p.processingMonth;
            int y = p.processingYear;

            string fperiod = model.year;
            string fundperiod = y.ToString();

            if (fperiod == fundperiod)
            {
                //get list from surplus or deficit nd get total
               // var res = service.GetAllSurplus();
               // decimal total2 = GroupBybalsheetcode(res.ToList());


                result = service.GetSurplus_Deficit().ToList();

            }

            return await generatePdf.GetPdf("Views/BalsheetReport/SurplusDedificitReport.cshtml", result);

        }
        public IActionResult balancesheetsandd() //balance sheet surplus and deficit
        {
            string fundTypeCode = HttpContext.Session.GetString("fundtypecode");
            var p = fundService.GetFundTypeCodeByCode(fundTypeCode);
            int y = p.processingYear;

            var jk = new AuditViewModel
            {
                year = y.ToString()
            };

            return View(jk);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> balancesheetsandd(AuditViewModel model)
        {


            var result = new List<LedgersView2>();
            string fundTypeCode = HttpContext.Session.GetString("fundtypecode");
            var p = fundService.GetFundTypeCodeByCode(fundTypeCode);
            int m = p.processingMonth;
            int y = p.processingYear;

            string fperiod = model.year;
            string fundperiod = y.ToString();

            if (fperiod == fundperiod)
            {
                
                result = service.GetBalanceSheetSurplus_Deficit().ToList();

            }

            return await generatePdf.GetPdf("Views/BalsheetReport/BalsheetReport_surplusndeficit.cshtml", result);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(AuditViewModel model)
        {
            //var result = new List<LedgersView2>();
            //var result2 = new List<LedgersView2>();
            //var result3 = new List<LedgersView2>();

            //var gh = new ReportViewModelQ();
            //string fundTypeCode = HttpContext.Session.GetString("fundtypecode");
            //var p = fundService.GetFundTypeCodeByCode(fundTypeCode);
            //int m = p.processingMonth;
            //int y = p.processingYear;

            //string fperiod = model.year;
            //string fundperiod = y.ToString();

            //if (fperiod == fundperiod)
            //{
            //    result = service.GetBalSheet().ToList();
            //    result2 = GroupBybalsheetcode2(result).ToList();
            //    result3 = GroupBybalsheetcode9(result).ToList();


            //    gh.p = result2;
            //    gh.q = result3;

            //}




            var result = new List<V_TRIALBALANCE>();
            string fundTypeCode = HttpContext.Session.GetString("fundtypecode");
            var p = fundService.GetFundTypeCodeByCode(fundTypeCode);
            int m = p.processingMonth;
            int y = p.processingYear;

            string fperiod = model.year;
            string fundperiod = y.ToString();

            if (fperiod == fundperiod)
            {
                if (model.excel == true)
                {
                    var exc3 = service.GetBalSheet_StoredProcedure(fundTypeCode).ToList();

                    var stream = new MemoryStream();

                    int row = 2;
                    using (var package = new ExcelPackage(stream))
                    {
                        var workSheet = package.Workbook.Worksheets.Add("Sheet2");
                        workSheet.Cells.LoadFromCollection(exc3, true);
                        package.Save();
                    }

                    string excelname = "BalanceSheetReport.xlsx";

                    stream.Position = 0;
                    string excelName = $"LoanSchedule-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
                    return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelname);
                }

                else
                {
                    result = service.GetBalSheet_StoredProcedure(fundTypeCode).ToList();
                    return await generatePdf.GetPdf("Views/BalsheetReport/BalsheetReport.cshtml", result);
                }
            }

            return View();

            //   else
            //   {
            //       string year = model.year;
            //       string wdoc = "Open" + fundTypeCode + year.Substring(2, 2) ;

            //       var res = service.GetAllSurplus2();
            //       var res2 = res.ToList();
            ////       res2 = Refractlist(res2, wdoc);
            //       decimal total2 = GroupBybalsheetcode(res2.ToList());


            //       var result = service.GetBalSheet2();
            //       var resultM = Refractlist(result.ToList(), wdoc);
            //       var resultj = resultM.ToList();
            //       var r2 = FilterRecord(resultj, total2).ToList();


            //       resultj.AddRange(r2);
            //       decimal total3 = 0M;
            //       var result2 = GroupBybalsheetcode2(resultj);

            //       var result3 = new List<LedgersView2>();
            //       var result4 = new List<ReportViewModel4>();

            //       foreach (var j in result2)
            //       {
            //           foreach (var k in resultj)
            //           {
            //               if (j.balSheetCode == k.balSheetCode)
            //               {
            //                   var z = new LedgersView2
            //                   {
            //                       description = k.MDesc,
            //                       Amount = k.Amount,
            //                       acctcode = k.balSheetCode
            //                   };

            //                   result3.Add(z);
            //               }

            //           }
            //           var fh = new ReportViewModel4
            //           {
            //               bl_desc = j.bl_desc,
            //               code = j.balSheetCode,
            //               tolly = result3,
            //               total = (j.Amount == null) ? 0M : (decimal)j.Amount
            //           };

            //           result4.Add(fh);
            //           total3 += fh.total;

            //       }


            //       gh.deficit = total3;
            //       gh.colly = result4;

            //       return await generatePdf.GetPdf("Views/BalsheetReport/BalsheetReport.cshtml", gh);

            //}

        }

        public decimal GroupBybalsheetcode(List<LedgersView2> solly)
        {
            decimal tot = 0M;
            var jk2 = new List<LedgersView2>();

            var m = solly.GroupBy(x => x.balSheetCode)
                .Select(
                  g => new
                  {
                      Value = g.Sum(s => s.Amount),
                      description = g.First().MDesc,
                      code = g.First().balSheetCode,
                      desc = g.First().bl_desc,
                      maincode = g.First().mainAccountCode
                  });


            foreach (var j in m)
            {
                var b = new LedgersView2
                {
                    description = j.description,
                    Amount = j.Value,
                    balSheetCode = j.code,
                    bl_desc = j.desc,
                    mainAccountCode = j.maincode
                };
                tot += (decimal)j.Value;
                jk2.Add(b);
            }

            return tot;
        }


        public IEnumerable<LedgersView2> GroupBybalsheetcode2(List<LedgersView2> solly)
        {

            var m = solly.GroupBy(x => x.balSheetCode)
                .Select(
                  g => new LedgersView2
                  {
                      Amount = Math.Round((decimal)g.Sum(s => s.Amount),2),
                      description = g.First().bl_desc,
                      balSheetCode = g.First().balSheetCode,
                      mainAccountCode=g.First().mainAccountCode
                  });


            return m;
        }

        public IEnumerable<LedgersView2> GroupBybalsheetcode9(List<LedgersView2> tolly)
        {
            var gettos = tolly.GroupBy(x => x.acctcode.Split("-")[0])
                .Select(
                  g => new LedgersView2
                  {
                      Amount = Math.Round((decimal)g.Sum(s => s.Amount), 2),
                      description = g.First().description,
                      MDesc=g.First().MDesc,
                      balSheetCode = g.First().balSheetCode,
                      mainAccountCode=g.First().mainAccountCode
                  });

            return gettos;
        }

        public IEnumerable<LedgersView2> FilterRecord(List<LedgersView2> solly,decimal total)
        {
            var r = new List<LedgersView2>();
            var s = new LedgersView2();
            int b = 1;
             
            foreach(var p in solly)
            {
                if (p.bl_desc.ToUpper().Trim() == "EQUITY" && b==1)
                {
                    s.description = "Surplus/Deficit";
                    s.MDesc = "Surplus / Deficit";
                    s.Amount = total;
                    s.balSheetCode = p.balSheetCode;
                    s.bl_desc = p.bl_desc;

                    r.Add(s);
                    b++;
                }

                

              
            }

            return r;
           
        }

        public List<LedgersView2>Refractlist(List<LedgersView2>polly,string doc)
        {
            var newList = new List<LedgersView2>();

            foreach(var j in polly)
            {
                if (j.docNo.Contains("Open101912"))
                {
                    string code = RefractCode(j.docNo).Trim();
                    if (code == doc.Trim())
                    {
                        newList.Add(j);
                    }
                }
                
            }

            return newList;
        }


        public string RefractCode(string Code)
        {
            char[] op = Code.ToCharArray();
            var arr = new char[op.Length];
            int k = 1;
            string output = string.Empty;

            for (int j = 0; j < op.Length; j++)
            {
                if (k <= 8)
                {
                    output += op[j].ToString();
                }

                k++;
            }

            return output;
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
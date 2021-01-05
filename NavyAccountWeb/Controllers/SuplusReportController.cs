
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NavyAccountCore.Core.AuditService;
using NavyAccountCore.Models;
using NavyAccountWeb.IServices;
using NavyAccountWeb.ViewModels;
using Wkhtmltopdf.NetCore;

namespace NavyAccountWeb.Controllers
{
    [SessionTimeout]
    public class SuplusReportController : Controller
    {
        private readonly ISurplusService service;
        private readonly IFundTypeCodeService fundService;
        private readonly IGeneratePdf generatePdf;
        public SuplusReportController(IFundTypeCodeService fundService, ISurplusService service, IGeneratePdf generatePdf)
        {
            this.fundService = fundService;
            this.service = service;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(AuditViewModel model)
        {
            string fundTypeCode = HttpContext.Session.GetString("fundtypecode");
            var p = fundService.GetFundTypeCodeByCode(fundTypeCode);
            int m = p.processingMonth;
            int y = p.processingYear;

            string fperiod = model.year.ToString();
            string fundperiod = y.ToString();
            var gh = new ReportViewModelQ();
            List<LedgersView2> result = new List<LedgersView2>();
            List<LedgersView2> result2 = new List<LedgersView2>();
            List<LedgersView2> result3 = new List<LedgersView2>();

            if (fperiod == fundperiod)
            {
                result = service.GetAllSurplus().ToList();
                result2 = GroupBybalsheetcode(result.ToList()).ToList();
                result3 = GroupBybalsheetcode9(result.ToList()).ToList();

                gh.p = result2;
                gh.q = result3;
            }
            //else
            //{
            //    string year = model.year;
            //    string wdoc2 = "Open" + fundTypeCode + year.Substring(2, 2) ;

            //    var result2 = service.GetAllSurplus2();
            //    var result3 = result2.Where(x => x.docNo.Substring(0,8) == wdoc2).ToList();

            //    decimal total;
            //    var res2 = GroupBybalsheetcode(result3.ToList(), out total);
            //    var res3 = new List<LedgersView2>();
            //    var res4 = new List<ReportViewModel4>();



            //    foreach (var j in res2)
            //    {
            //        foreach (var k in result3)
            //        {
            //            if (j.balSheetCode == k.balSheetCode)
            //            {
            //                var z = new LedgersView2
            //                {
            //                    description = k.MDesc,
            //                    Amount = k.Amount,
            //                    acctcode = k.balSheetCode
            //                };

            //                res3.Add(z);
            //            }

            //        }
            //        var fh = new ReportViewModel4
            //        {
            //            bl_desc = j.bl_desc,
            //            code = j.balSheetCode,
            //            tolly = res3,
            //            total = (j.Amount == null) ? 0M : (decimal)j.Amount
            //        };

            //        res4.Add(fh);


            //    }

            //    ViewBag.total = total;

            //    var gh = new ReportViewModel5
            //    {
            //        deficit = total,
            //        colly = res4
            //    };

            //    return await generatePdf.GetPdf("Views/SuplusReport/SurplusReport.cshtml", gh);
            //}
            return await generatePdf.GetPdf("Views/SuplusReport/SurplusReport.cshtml", gh);
        }


        public IEnumerable<LedgersView2> GroupBybalsheetcode9(List<LedgersView2> tolly)
        {
            var gettos=tolly.GroupBy(x=>x.acctcode.Split("-")[0])
                .Select(
                  g => new LedgersView2
                  {
                      Amount = Math.Round((decimal)g.Sum(s => s.Amount),2),
                      description = g.First().MDesc,
                      balSheetCode=g.First().balSheetCode
                  });

            return gettos;
        }

        public IEnumerable<LedgersView2> GroupBybalsheetcode(List<LedgersView2> solly)
        {
           

            var m = solly.GroupBy(x => x.balSheetCode)
                .Select(
                  g => new LedgersView2
                  {
                      Amount = Math.Round((decimal)g.Sum(s => s.Amount),2),
                      description = g.First().bl_desc,
                      balSheetCode=g.First().balSheetCode
                  });


            return m;

        }

        public List<LedgersView> GetMain(List<LedgersView>opo)
        {
            var y = new List<string>();
            var y2 = new List<string>();
            var result2 = new List<LedgersView>();

            foreach (var j in opo)
            {
                y.Add(service.RefractCode(j.code));
            }

            y2 = y.Distinct().ToList();

            foreach(var km in y2)
            {
                var k = new LedgersView
                {
                    code = km,
                    desc = service.description(km),
                    crbal = getSumCr(opo,km),
                    dbbal = getSumDr(opo,km)
                };

                result2.Add(k);
            }

            return result2;
        }

        


        public decimal getSumCr(List<LedgersView> solly, string code)
        {

            decimal c = 0M;
            foreach (var j in solly)
            {
                if (service.RefractCode(j.code.Trim()) == code.Trim())
                {
                    c += j.crbal;
                }
            }

            return c;

        }

        public decimal getSumDr(List<LedgersView> solly, string code)
        {
            decimal c = 0M;
            foreach (var j in solly)
            {
                if (service.RefractCode(j.code.Trim()) == code.Trim())
                {
                    c += j.dbbal;
                }
            }

            return c;

        }
        public List<LedgersView> GetAllSubs(List<LedgersView> solly,out decimal totalCr,out decimal totalDr)
        {
            var subsidiaryList = new List<LedgersView>();
            decimal TotalCr = 0M;
            decimal TotalDr = 0M;

            foreach (var j in solly)
            {
                var k = new LedgersView
                {

                    code = j.code,
                    desc = j.desc,
                    dbbal = j.dbbal,
                    crbal = j.crbal
                };

                TotalCr +=k.crbal;
                TotalDr += k.dbbal;

                subsidiaryList.Add(k);

            }

            totalCr = TotalCr;
            totalDr = TotalDr;


            return subsidiaryList;
          
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
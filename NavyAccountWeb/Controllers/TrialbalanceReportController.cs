using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NavyAccountCore.Models;
using NavyAccountWeb.IServices;
using NavyAccountWeb.Models;
using NavyAccountWeb.ViewModels;
using Wkhtmltopdf.NetCore;
using Microsoft.AspNetCore.Http;
using MoreLinq.Extensions;
using NavyAccountCore.Core.AuditService;

namespace NavyAccountWeb.Controllers
{
    [SessionTimeout]
    public class TrialbalanceReportController : Controller
    {
        private readonly ITrialbalanceReportService services;
        private readonly IFundTypeCodeService fundService;
        private readonly IGeneratePdf generatePdf;
        private readonly IAccountHistoryService accountHistoryService;
        //private IConverter _converter;


        public TrialbalanceReportController(ITrialbalanceReportService services, IGeneratePdf generatePdf, IFundTypeCodeService fundService, IAccountHistoryService accountHistoryService)
        {
            this.services = services;
            this.generatePdf = generatePdf;
            this.fundService = fundService;
            this.accountHistoryService = accountHistoryService;
        }

        
        public IActionResult Index()
        {
            string fundTypeCode = HttpContext.Session.GetString("fundtypecode");
            var p = fundService.GetFundTypeCodeByCode(fundTypeCode);
            string ProcessYear = p.processingYear.ToString();

            return View();
        }


        [Route("TrialbalanceReport/GetMainAct")]
        public IEnumerable<LedgersView> Get()
        {
            return services.GenerateMainAcct().ToList();
        }




        [Route("TrialbalanceReport/ProcessReport/{indicator}/{year}/{mainacct}")]
        public async Task<IActionResult> getReport(string indicator,string year,string mainacct)
        {
            string fundTypeCode = HttpContext.Session.GetString("fundtypecode");
            var p = fundService.GetFundTypeCodeByCode(fundTypeCode);
            string ProcessYear = p.processingYear.ToString();

            if (!string.IsNullOrWhiteSpace(indicator))
            {
                List<LedgersView> j = new List<LedgersView>();
                string valDb = RefractCode2(ProcessYear);
                string frmForm = RefractCode2(year);
                
                

                //string month = model.month;
                //string year = model.year;
                string headerVariable=string.Empty;

                if (valDb == frmForm)
                {
                    j = services.GenerateTrialBalanceReport(indicator, fundTypeCode).ToList();
                }
                else
                {
                   
                    string wdoc2 = "Open" + fundTypeCode + frmForm+ "00";
                    //check npf_history
                    j = services.GenerateNpfHistory(wdoc2).OrderBy(x => x.code).ToList();
                    if(indicator == "Main Ledger")
                    {
                        j = services.GenMainLedgers(j).ToList();
                    }

                }

              
                var q = FilterViewModel(j.ToList());

                if (indicator == "Main Ledger")
                {
                    headerVariable = "Trial Balance Report";

                    return await generatePdf.GetPdf("Views/TrialbalanceReport/ReportPage.cshtml", GroupSum(q.ToList(),year, headerVariable));
                }
                else if (indicator == "Subsidiary Ledger")
                {
                    headerVariable = "Trial Balance Report";
                    return await generatePdf.GetPdf("Views/TrialbalanceReport/ReportPage.cshtml", GetAllSubs(q.ToList(),year,headerVariable));
                }
                else
                {
                    if (mainacct == null)
                    {
                        return RedirectToAction("index", "TrialbalanceReport");
                    }
                    else
                    {
                        //string maincode = mainacct;
                        headerVariable = services.getMainActDesc(mainacct).ToLowerInvariant();
                        var t = GetAllfiltered(q, mainacct);
                        return await generatePdf.GetPdf("Views/TrialbalanceReport/ReportPage.cshtml", GetAllSubs(t, year, headerVariable));
                    }
                   
                }
            }

            return RedirectToAction("Index");

        }


        public List<balanceViewModel> getAllMonth()
        {
            return new List<balanceViewModel>
            {
                new balanceViewModel{Indicator="January"},
                new balanceViewModel{Indicator="Febuary"},
                new balanceViewModel{Indicator="March"},
                 new balanceViewModel{Indicator="April"},
                new balanceViewModel{Indicator="May"},
                new balanceViewModel{Indicator="June"},
                 new balanceViewModel{Indicator="July"},
                new balanceViewModel{Indicator="August"},
                new balanceViewModel{Indicator="September"},
                 new balanceViewModel{Indicator="October"},
                new balanceViewModel{Indicator="November"},
                new balanceViewModel{Indicator="December"}

            };
        }

        [HttpGet]
        public IActionResult getReportByYear() {
            List<Name_Value> nv = new List<Name_Value>();
            var res = new History_LedgerViewModel
            {
                fundcode = HttpContext.Session.GetString("fundtypecode")
            };

            foreach (int vv in accountHistoryService.getYearForReport()) {
                nv.Add(new Name_Value()
                {
                    text = vv,
                    value = vv
                });
            }
            res.getYear = nv;

            return View(res);
        }

        [HttpGet]
        public async Task<IActionResult> getReportByYear(string processingyear)
        {
            string fundTypeCode = HttpContext.Session.GetString("fundtypecode");
            int? processingYear = HttpContext.Session.GetInt32("processingYear");
            if (!String.IsNullOrEmpty(processingyear)) { 
            
            }
            return await generatePdf.GetPdf("Views/TrialbalanceReport/ReportPage.cshtml", "");
        }

            public string getMonthReversed(string month)
        {
            string result = "";

            if (month == "January")
                result = "01";
            if (month == "Febuary")
                result = "02";
            if (month == "March")
                result = "03";
            if (month == "April")
                result = "04";
            if (month == "May")
                result = "05";
            if (month == "June")
                result = "06";
            if (month == "July")
                result = "07";
            if (month == "August")
                result = "08";
            if (month == "September")
                result = "09";
            if (month == "October")
                result = "10";
            if (month == "November")
                result = "11";
            if (month == "December")
                result = "12";

            return result;
        }
        public string getMonth(int month)
        {
            string result = "";

            if (month == 1)
                result = "JANUARY";
            if (month == 2)
                result = "FEBUARY";
            if (month == 3)
                result = "MARCH";
            if (month == 4)
                result = "APRIL";
            if (month == 5)
                result = "MAY";
            if (month == 6)
                result = "JUNE";
            if (month == 7)
                result = "JULY";
            if (month == 8)
                result = "AUGUST";
            if (month == 9)
                result = "SEPTEMBER";
            if (month == 10)
                result = "OCTOBER";
            if (month == 11)
                result = "NOVEMBER";
            if (month == 12)
                result = "DECEMBER";

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

       



        public List<balanceViewModel> Filed()
        {
            return new List<balanceViewModel>
            {
                new balanceViewModel{Indicator="Main Ledger"},
                new balanceViewModel{Indicator="Subsidiary Ledger"},
                new balanceViewModel{Indicator="Specific Main ledger"}
            };
        }

        public List<TrialBalanceViewModel> GetAllfiltered(IEnumerable<TrialBalanceViewModel> solly, string co)
        {
            var mainSpecificList = new List<TrialBalanceViewModel>();

            foreach(var j in solly)
            {
                string code2 = RefractCode(j.code);
                if (code2 == co)
                {
                    mainSpecificList.Add(j);
                }
            }

            return mainSpecificList;
        }

        public string RefractCode(string Code)
        {
            char[] op = Code.ToCharArray();
            var arr = new char[op.Length];
            int k = 1;
            string output = string.Empty;

            for (int j = 0; j < op.Length; j++)
            {
                if (k <= 4)
                {
                    output += op[j].ToString();
                }

                k++;
            }

            return output;
        }

        public string RefractCode2(string Code)
        {
            char[] op = Code.ToCharArray();
            var arr = new char[op.Length];
           
            string output = string.Empty;

            for (int j = 0; j < op.Length; j++)
            {
                if (j >=2)
                {
                    output += op[j].ToString();
                }

              
            }

            return output;
        }

        public ReportViewModel GroupSum(List<TrialBalanceViewModel> solly,string year,string headerVariable)
        {
            var result1 = solly.DistinctBy(x => x.code).ToList();
            var result2 = new List<TrialBalanceViewModel>();
            decimal TotalCr = 0M;
            decimal TotalDr = 0M;
            foreach (var j in result1)
            {
                var k = new TrialBalanceViewModel
                {
                    code = j.code,
                    description = j.description,
                    credit = getSumCr(solly, j.code),
                    debit = getSumDr(solly, j.code)
                };
                TotalCr += k.credit;
                TotalDr += k.debit;

                result2.Add(k);
            }

            var result3 = new ReportViewModel
            {
                getList = result2,
                TotalCredit = TotalCr,
                TotalDebit = TotalDr,
                year=year,
                HeaderVariable=headerVariable
            };

            return result3;

        }

        public ReportViewModel GetAllSubs(List<TrialBalanceViewModel> solly,string year,string headerVariable)
        {
            var subsidiaryList = new List<TrialBalanceViewModel>();
            decimal TotalCr = 0M;
            decimal TotalDr = 0M;

            foreach (var j in solly)
            {
                var k=new TrialBalanceViewModel
                {
               
                    code = j.code,
                    description = j.description,
                    debit = j.debit,
                    credit = j.credit
                };

                TotalCr += k.credit;
                TotalDr += k.debit;

                subsidiaryList.Add(k);
            
            }

            return new ReportViewModel
            {
                getList = subsidiaryList.ToList(),
                TotalCredit = TotalCr,
                TotalDebit = TotalDr,
                year=year,
                HeaderVariable=headerVariable
                
            };
        }

        public decimal getSumCr(List<TrialBalanceViewModel> solly,string code)
        {
            decimal c = 0M;
            foreach(var j in solly)
            {
                if (j.code.Trim() == code.Trim())
                {
                    c += j.credit;
                }
            }

            return c;

        }

        public decimal getSumDr(List<TrialBalanceViewModel> solly, string code)
        {
            decimal c = 0M;
            foreach (var j in solly)
            {
                if (j.code.Trim() == code.Trim())
                {
                    c += j.debit;
                }
            }

            return c;

        }
        public IEnumerable<TrialBalanceViewModel> FilterViewModel(List<LedgersView> opo)
        {
            var result = new List<TrialBalanceViewModel>();

            foreach(var j in opo)
            {
                var k = new TrialBalanceViewModel
                {
                    code = j.code,
                    description = j.desc,
                    debit = checkDebit(j.opBal, j.dbbal, j.crbal),
                    credit = checkCredit(j.opBal, j.dbbal, j.crbal)
                };

                result.Add(k);
            }
            return result;
        }

        public decimal checkDebit(decimal a,decimal b,decimal c)
        {
            decimal result = a + b - c;
            decimal result2 = 0M;
            if(result>=0)
            {
                result2 = result;
            }

            return result2;
        }
        public decimal checkCredit(decimal a, decimal b, decimal c)
        {
            decimal result = a + b - c;
            decimal result2 = 0M;
            if (result < 0)
            {
                result2 = Math.Abs(result);
            }

            return result2;
        }

    }
}
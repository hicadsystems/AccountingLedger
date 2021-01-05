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
    public class LoanPositionController : Controller
    {
        private readonly ITrialbalanceReportService services;
        private readonly IGeneratePdf generatePdf;
        public LoanPositionController(ITrialbalanceReportService services,IGeneratePdf generatePdf)
        {
            this.services = services;
            this.generatePdf = generatePdf;
        }
        public IActionResult Index()
        {
            var res = new LoanPositionViewModel
            {
                getMainAct = services.GetLoanAct().ToList(),
                fundcode =  HttpContext.Session.GetString("fundtypecode")
            };

            return View(res);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> generate(LoanPositionViewModel model)
        {
            string indicator = "Specific Main ledger";
            string fundcode = model.fundcode;

            string code = model.mainacct;

            var result=services.GenerateTrialBalanceReport(indicator, fundcode);
            result = GetAllLoanfiltered(result, code).ToList();
            var result2 = FilteredLoan(result.ToList());
           
            return await generatePdf.GetPdf("Views/LoanPosition/LoanReport.cshtml", GetAllSubs(result2));
        }

        public ReportViewModel GetAllSubs(List<TrialBalanceViewModel> solly)
        {
            var subsidiaryList = new List<TrialBalanceViewModel>();
            decimal TotalCr = 0M;
            decimal TotalDr = 0M;

            foreach (var j in solly)
            {
                var k = new TrialBalanceViewModel
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
                TotalDebit = TotalDr
            };
        }

        public List<LedgersView> GetAllLoanfiltered(IEnumerable<LedgersView> solly, string co)
        {
            var mainSpecificList = new List<LedgersView>();

            foreach (var j in solly)
            {
                string code2 = RefractCode(j.code);
                if (code2 == co)
                {
                    mainSpecificList.Add(j);
                }
            }

            return mainSpecificList;
        }


        public List<TrialBalanceViewModel> FilteredLoan(List<LedgersView> lolly)
        {
            var opoy = new List<TrialBalanceViewModel>();
            foreach(var j in lolly)
            {
                var jk = new TrialBalanceViewModel
                {
                    code = j.code,
                    description = j.desc,
                    debit = Math.Abs(j.dbbal - j.crbal),
                    credit=0M
                };
                opoy.Add(jk);

            }

            return opoy;
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
    }
}
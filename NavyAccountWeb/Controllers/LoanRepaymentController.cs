using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NavyAccountCore.Core.AuditService;
using NavyAccountCore.Models;
using NavyAccountWeb.IServices;
using NavyAccountWeb.ViewModels;
using Wkhtmltopdf.NetCore;

namespace NavyAccountWeb.Controllers
{
    [SessionTimeout]
    public class LoanRepaymentController : Controller
    {
        private readonly ILoanScheduleservices services;
        private readonly IGeneratePdf generatePdf;
        public LoanRepaymentController(ILoanScheduleservices services,IGeneratePdf generatePdf)
        {
            this.services = services;
            this.generatePdf = generatePdf;
        }

        public async Task<IActionResult> CalaculateLoanRepayment(int id = 0, int loantypeid = 0)
        {

            var result = services.FilterAllLoanSchedule(id, loantypeid);
            var result2 = services.GetLoan(id, loantypeid);
            var result3 = services.getLoanCount(id, loantypeid);
            
            var gh = new LoanRepaymentViewModel();
            if (result3 == 0)
            {
                gh.solly = FilterRecord(result, result3);
                return await generatePdf.GetPdf("Views/LoanRegister/loanRepaymentReport.cshtml", gh);
            }
            gh.solly = FilterRecord(result, result3);
            gh.Name = result2.Name;
            gh.Email = result2.Email;
            gh.interest = result2.interest;
            gh.Tenor = result2.Tenor;
            gh.AmountGranted = result2.AmountGranted;
            gh.Rank = result2.Rank;


            return await generatePdf.GetPdf("Views/LoanRegister/loanRepaymentReport.cshtml", gh);
        }


        public IEnumerable<LoanView>FilterRecord (IEnumerable<LoanView>sol,int count)
        {
            var pol = new List<LoanView>();
            int k = 1;

            foreach(var j in sol)
            {
                if (count > 0)
                {
                    if (k <= count)
                    {
                        j.id = k;
                        pol.Add(j);
                        k++;
                    }
                } 
            }

            return pol;
        }


    }
}
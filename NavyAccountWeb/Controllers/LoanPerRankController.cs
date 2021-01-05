using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NavyAccountCore.Core.AuditService;
using NavyAccountWeb.IServices;
using Wkhtmltopdf.NetCore;

namespace NavyAccountWeb.Controllers
{
    [SessionTimeout]
    public class LoanPerRankController : Controller
    {
        private readonly ILoanPerRankService services;
        private readonly ILoanTypeService fundService;
        private readonly IGeneratePdf generatePdf;

        public LoanPerRankController(ILoanPerRankService services, ILoanTypeService fundService, IGeneratePdf generatePdf)
        {
            this.services = services;
            this.fundService = fundService;
            this.generatePdf = generatePdf;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> printLoanPerRank(string loancode)
        {
            var fg = fundService.GetLoanTypes();
            ViewBag.contributionList = new SelectList(fg, "Code", "Description", loancode);

            if (string.IsNullOrEmpty(loancode))
            {
                return View();
            }

            var result = services.GetAll(loancode);
            return await generatePdf.GetPdf("Views/LoanPerRank/LoanPerRankReport.cshtml", result);
        }
    }
}
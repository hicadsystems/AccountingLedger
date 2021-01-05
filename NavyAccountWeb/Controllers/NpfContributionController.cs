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
    public class NpfContributionController : Controller
    {
        private readonly IContributionServices services;
        private readonly IFundTypeService fundService;
        private readonly IGeneratePdf generatePdf;
        public NpfContributionController(IContributionServices services, IFundTypeService fundService, IGeneratePdf generatePdf)
        {
            this.services = services;
            this.fundService = fundService;
            this.generatePdf = generatePdf;
        }
        public ActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> printNpFContribution(string fundcode)
        {
            var fg = fundService.GetFundTypes();
            ViewBag.contributionList = new SelectList(fg, "Code", "Description", fundcode);

            if (string.IsNullOrEmpty(fundcode))
            {
                return View();
            }

            var result = services.GetAll(fundcode);
            return await generatePdf.GetPdf("Views/NpfContribution/Contribution.cshtml", result);
        }
    }
}
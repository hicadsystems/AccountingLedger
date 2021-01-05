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
    public class AuditTrailController : Controller
    {
        private readonly IAuditTrailServices services;
        private readonly IFundTypeCodeService fundService;
        private readonly IGeneratePdf generatePdf;
        public AuditTrailController(IAuditTrailServices services, IFundTypeCodeService fundService, IGeneratePdf generatePdf)
        {
            this.services = services;
            this.fundService = fundService;
            this.generatePdf = generatePdf;
        }
        public IActionResult Index()
        {
            var hj = new AuditViewModel
            {
                loadAllNpfChart = services.GetNpfChart(),
                loadAllYear= loadAllYearIntoForm(services.GetDateInHistory())
            };
            return View(hj);
        }

        [HttpPost]
        public async Task<IActionResult> Index(AuditViewModel model)
        {
            string fundTypeCode = HttpContext.Session.GetString("fundtypecode");
            var p = fundService.GetFundTypeCodeByCode(fundTypeCode);
            int m = p.processingMonth;
            int y = p.processingYear;

            string speriod = y.ToString() + getMonth2(m);
            string wdoc = "Open" + fundTypeCode + model.year.Substring(2, 2) + "00";

            var result1 = services.GetSingleRecord(model.acctcode, wdoc);
            var result2 = services.GetAllRecord(result1.period,model.acctcode).ToList();
            string description = services.GetNpfDesc(model.acctcode);

            var result3 = GetAllSubs(result2, result1,model.acctcode,y.ToString(),description);
           

            return await generatePdf.GetPdf("Views/AuditTrail/AuditReport.cshtml", result3);
  
        }


        public ReportViewModel2 GetAllSubs(List<LedgersView> solly, LedgersView tolly, string acctCode,string year,string description)
        {
            var j = new ReportViewModel2
            {
                AcctCode = acctCode,
                solly = solly,
                opy = tolly,
                year = year,
                AcctDesc=description
            };

            return j;
        }

        public IEnumerable<LedgersView> loadAllYearIntoForm(IEnumerable<string>folly)
        {
            var result = new List<LedgersView>();
            foreach(var j in folly)
            {
                result.Add(new LedgersView { code = j });
            }

            return result;
        }

        public string getMonth2(int month)
        {
            string result = "";

            if (month == 0)
                result = "00";
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
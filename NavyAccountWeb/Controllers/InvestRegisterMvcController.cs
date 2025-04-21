using Microsoft.AspNetCore.Mvc;
using NavyAccountCore.Core.AuditService;
using NavyAccountWeb.IServices;
using System;
using System.Threading.Tasks;
using Wkhtmltopdf.NetCore;

namespace NavyAccountWeb.Controllers
{
    [SessionTimeout]
    public class InvestRegisterMvcController : Controller
    {

        private readonly IInvestmentRegisterServices services;
        private readonly IGeneratePdf generatePdf;

        public InvestRegisterMvcController(IInvestmentRegisterServices services, IGeneratePdf generatePdf)
        {
            this.services = services;
            this.generatePdf = generatePdf;
        }
        public IActionResult listofinvestment()
        {
            var investlist = services.GetALLInvestListOT();
            return View(investlist);
        }
        public async Task<IActionResult> InvestmentAllOT()
        {
            var money =  services.GetALLInvestListOT();
            return await generatePdf.GetPdf("Views/InvestRegisterMvc/moneyReport.cshtml", money);
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult capitalmarket()
        {
            return View();
        }
        public IActionResult outstanding()
        {
            return View();
        }
        [HttpPost]
        public IActionResult outstanding(DateTime startdate, DateTime enddate,int Submit2)
        {
            var d = startdate.Day;
            var m = startdate.Month;
         var oustandinglist=  services.GetAllInvestRegisterOST(startdate,enddate);
            return View(oustandinglist);
        }
        public async Task<IActionResult> InvestmentMoneyReport(DateTime startdate, DateTime enddate)
        {
            var oustandinglist = services.GetAllInvestRegisterOST(startdate, enddate);
            return await generatePdf.GetPdf("Views/InvestRegisterMvc/InvestmentMoneyReport.cshtml", oustandinglist);
        }
        [Route("Investment/InvestmentMoneyAll")]
        public async Task<IActionResult> InvestmentMoney()
        {
            var money = services.GetAllInvestRegister();
            return await generatePdf.GetPdf("Views/InvestRegisterMvc/moneyReport.cshtml", money);
        }
        public async Task<IActionResult> InvestmentCapital()
        {
            var capital = services.GetAllInvestRegister2();
            return await generatePdf.GetPdf("Views/InvestRegisterMvc/capitalReport.cshtml", capital);

        }
    }
}
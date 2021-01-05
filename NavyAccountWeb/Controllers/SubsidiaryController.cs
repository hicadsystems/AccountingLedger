using Microsoft.AspNetCore.Mvc;
using NavyAccountCore.Core.AuditService;
using NavyAccountWeb.IServices;
using System.Threading.Tasks;
using Wkhtmltopdf.NetCore;

namespace NavyAccountWeb.Controllers
{
    [SessionTimeout]
    public class SubsidiaryController : Controller
    {
        private readonly IChartofAccountService chartofAccountService;
        private readonly IGeneratePdf generatePdf;
        public SubsidiaryController(IChartofAccountService chartofAccountService, IGeneratePdf generatePdf)
        {
            this.chartofAccountService = chartofAccountService;
            this.generatePdf = generatePdf;
        }

        [HttpGet]
        public ActionResult ViewSubsidiary()
        {
            //var pk = n.GetAll();

            return View();
        }
        public async Task<IActionResult> printchart()
        {
            var listchart = chartofAccountService.getAllChartofAccountsCH().Result;

            return await generatePdf.GetPdf("Views/Subsidiary/chartofacctReport.cshtml", listchart);
        }
    }
}
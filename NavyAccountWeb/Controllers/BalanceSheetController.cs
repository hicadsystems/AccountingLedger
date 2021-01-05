using Microsoft.AspNetCore.Mvc;
using NavyAccountCore.Core.AuditService;
using NavyAccountWeb.IServices;
using System.Threading.Tasks;
using Wkhtmltopdf.NetCore;

namespace NavyAccountWeb.Controllers
{
    [SessionTimeout]
    public class BalanceSheetController : Controller
    {
        private readonly IBalanceSheetService balanceSheetService;
        private readonly IGeneratePdf generatePdf;
        public BalanceSheetController(IBalanceSheetService balanceSheetService, IGeneratePdf generatePdf)
        {
            this.balanceSheetService = balanceSheetService;
            this.generatePdf = generatePdf;
        }

        [HttpGet]
        public ActionResult ViewFinancial()
        {
            return View();
        }
        public async Task<IActionResult> printbalancesheet()
        {
            var listbalance = balanceSheetService.GetBalanceSheets();

            return await generatePdf.GetPdf("Views/BalanceSheet/balancesheetReport.cshtml", listbalance);
        }

    }
}
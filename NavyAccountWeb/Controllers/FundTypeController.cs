using Microsoft.AspNetCore.Mvc;
using NavyAccountCore.Core.AuditService;
using NavyAccountWeb.IServices;
using System.Threading.Tasks;
using Wkhtmltopdf.NetCore;

namespace NavyAccountWeb.Controllers
{
    [SessionTimeout]
    public class FundTypeController : Controller
    {
        private readonly IFundTypeService fundtypeService;
        private readonly IGeneratePdf generatePdf;
        private readonly IStockService stockService;
        public FundTypeController(IGeneratePdf generatePdf, IFundTypeService fundtypeService, IStockService stockService)
        {
            this.fundtypeService = fundtypeService;
            this.generatePdf = generatePdf;
            this.stockService = stockService;
        }
        

        [HttpGet]
        public ActionResult ViewFundType()
        {
          return View();
        }
        public async Task<IActionResult> printfund2()
        {
            var listfund = fundtypeService.GetFundTypes();

            return await generatePdf.GetPdf("Views/FundType/FundReport.cshtml", listfund);
        }
    }
}
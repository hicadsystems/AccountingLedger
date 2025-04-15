using Microsoft.AspNetCore.Mvc;
using NavyAccountCore.Core.AuditService;
using NavyAccountWeb.IServices;
using System.Threading.Tasks;
using Wkhtmltopdf.NetCore;

namespace NavyAccountWeb.Controllers
{
    [SessionTimeout]
    public class StocksController : Controller
    {
        private readonly IStockService stockService;
        private readonly IGeneratePdf generatePdf;
        public StocksController(IStockService stockService, IGeneratePdf generatePdf)
        {
            this.stockService = stockService;
            this.generatePdf = generatePdf;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> printstock()
        {
            var listbalance = stockService.GetStocks();

            return await generatePdf.GetPdf("Views/Stock/stockReport.cshtml", listbalance);
        }

    }
}
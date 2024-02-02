using Microsoft.AspNetCore.Mvc;
using Wkhtmltopdf.NetCore;

namespace NavyAccountWeb.Controllers
{
    public class StockController : Controller
    {
        private readonly IGeneratePdf generatePdf;

        public StockController(IGeneratePdf generatePdf)
        {
            this.generatePdf = generatePdf;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using NavyAccountCore.Core.AuditService;
using NavyAccountWeb.IServices;
using System.Threading.Tasks;
using Wkhtmltopdf.NetCore;

namespace NavyAccountWeb.Controllers
{
    [SessionTimeout]
    public class LoanTypeController : Controller
    {
        private readonly ILoanTypeService loantypeService;
        private readonly IGeneratePdf generatePdf;
        public LoanTypeController(ILoanTypeService loantypeService, IGeneratePdf generatePdf)
        {
            this.loantypeService = loantypeService;
            this.generatePdf = generatePdf;
        }


        [HttpGet]
        public ActionResult GetAllLoanType()
        {
          
            return View();
        }
        public async Task<IActionResult> printloan()
        {
            var listloan = loantypeService.GetLoanTypes();

            return await generatePdf.GetPdf("Views/LoanType/LoanReport.cshtml", listloan);
        }
        public ActionResult ReviewLoanType()
        {
            return View();
        }
    }
}

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NavyAccountCore.Core.AuditService;
using NavyAccountWeb.IServices;
using Wkhtmltopdf.NetCore;

namespace NavyAccountWeb.Controllers
{
    [SessionTimeout]
    public class MainAccountController : Controller
    {
     
        private readonly IMainAccountService mainAccountService;
        private readonly IGeneratePdf generatePdf;
        public MainAccountController(IGeneratePdf generatePdf,IMainAccountService mainAccountService)
        {
            this.mainAccountService = mainAccountService;
            this.generatePdf = generatePdf;
        }


            [HttpGet]
        public ActionResult ViewMainAccount()
        {
            return View();
        }
        public async Task<IActionResult> printmainacct()
        {
            var listmain = mainAccountService.GetMainAccountDesc().Result;

            return await generatePdf.GetPdf("Views/MainAccount/mainacctReport.cshtml", listmain);
        }

    }
}
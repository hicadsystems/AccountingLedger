using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IronPdf;
using NavyAccountCore.Core.AuditService;

namespace NavyAccountWeb.Controllers
{
    [SessionTimeout]
    public class DemoController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PrintPDf()
        {
            var Renderer = new HtmlToPdf();

            Renderer.PrintOptions.MarginTop = 50; //millimeters    
            Renderer.PrintOptions.MarginBottom = 50;
            Renderer.PrintOptions.CssMediaType = PdfPrintOptions.PdfCssMediaType.Print;

            Renderer.PrintOptions.Header = new SimpleHeaderFooter()
            {
                CenterText = "{pdf-title}",
                DrawDividerLine = true,
                FontSize = 16
            };

            Renderer.PrintOptions.Footer = new SimpleHeaderFooter()
            {
                LeftText = "{date} {time}",
                RightText = "Page {page} of {total-pages}",
                DrawDividerLine = true,
                FontSize = 14
            };

            string adress = @"C:\Users\HP\Desktop\NavyAccount\AccountingLedger\NavyAccountWeb\Views\Shared\Index.cshtml";

            var PDF = Renderer.RenderHTMLFileAsPdf(adress);
            var OutputPath = "Invoice.pdf";
            PDF.SaveAs(OutputPath);

            // This neat trick opens our PDF file so we can see the result    
            System.Diagnostics.Process.Start(OutputPath);

            return View();
        }
    }
}
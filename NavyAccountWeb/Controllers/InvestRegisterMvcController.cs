using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NavyAccountCore.Core.AuditService;
using NavyAccountCore.Models;
using NavyAccountWeb.IServices;
using NavyAccountWeb.ViewModels;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            var investlist = services.GetALLInvestListCapitalMk();
            return View(investlist);
        }
        [HttpPost]
        public IActionResult listofinvestment(DateTime startdate, DateTime enddate, int Submit2)
        {
            HttpContext.Session.SetString("startdate", startdate.ToString());
            HttpContext.Session.SetString("enddate", enddate.ToString());

            var investlist = services.GetALLInvestListCapitalMk(startdate, enddate);
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
            var investlist = services.GetALLInvestListOT();
            return View(investlist);
        }
        [HttpPost]
        public IActionResult outstanding(DateTime startdate, DateTime enddate,int Submit2)
        {
            HttpContext.Session.SetString("startdate", startdate.ToString());
            HttpContext.Session.SetString("enddate", enddate.ToString());

            var oustandinglist=  services.GetAllInvestRegisterOST(startdate,enddate);
            return View(oustandinglist);
        }
        public async Task<IActionResult> InvestmentMoneyReport()
        {
            string startdates = HttpContext.Session.GetString("startdate");
            string enddates = HttpContext.Session.GetString("enddate");
            var money = new List<InvestmentView>();
            if (startdates != null)
            {
                money = services.GetAllInvestRegisterOST(Convert.ToDateTime(startdates), Convert.ToDateTime(enddates));
            }
            else
            {
                money = services.GetALLInvestListOT();
            }
            return await generatePdf.GetPdf("Views/InvestRegisterMvc/InvestmentMoneyReport.cshtml", money);
        }
        public async Task<IActionResult> InvestmentMoney()
        {
            var money = services.GetAllInvestRegister();
            return await generatePdf.GetPdf("Views/InvestRegisterMvc/moneyReport.cshtml", money);
        }
        public IActionResult Export2()
        {
            string startdates = HttpContext.Session.GetString("startdate");
            string enddates = HttpContext.Session.GetString("enddate");
            var money = new List<InvestmentView>();
            List<InvestmentReportModel> rpt = new List<InvestmentReportModel>();
            if (startdates != null)
            {
                money = services.GetAllInvestRegisterOST(Convert.ToDateTime(startdates), Convert.ToDateTime(enddates));
             
                foreach (var p in money)
                {
                    var pps = new InvestmentReportModel
                    {
                        Amount = p.Amount,
                        issuancebank = p.issuancebank,
                        DueDate = p.DueDate,
                        Tenure = p.Tenure,
                        interest = p.interest,
                        Maturingdate = p.Maturingdate,
                        Voucher = p.Voucher,
                        MaturedAmount = p.maturedamt,
                        receivingbank = p.receivingbank,
                        InvestmentType = p.InvestmentType,
                        company = p.company
                    };
                    rpt.Add(pps);
                }
            }
            else
            {
                money = services.GetALLInvestListOT();
                foreach (var p in money)
                {
                    var pps = new InvestmentReportModel
                    {
                        Amount = p.Amount,
                        issuancebank = p.issuancebank,
                        DueDate = p.DueDate,
                        Tenure = p.Tenure,
                        interest = p.interest,
                        Maturingdate = p.Maturingdate,
                        Voucher = p.Voucher,
                        MaturedAmount = p.maturedamt,
                        receivingbank = p.receivingbank,
                        InvestmentType = p.InvestmentType,
                        company = p.company
                    };
                    rpt.Add(pps);
                }
            }

            var stream = new MemoryStream();

            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet2");
                workSheet.Cells.LoadFromCollection(rpt, true);
                package.Save();
            }
            stream.Position = 0;
            string excelName = $"MoneyReport-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
        public async Task<IActionResult> InvestmentCapital()
        {
            string startdates=HttpContext.Session.GetString("startdate");
            string enddates = HttpContext.Session.GetString("enddate");
             var capital = new List<InvestmentView>();
       
            if (startdates != null)
            {
                capital = services.GetALLInvestListCapitalMk(Convert.ToDateTime(startdates), Convert.ToDateTime(enddates));
              
            }
            else
            {
                capital = services.GetALLInvestListCapitalMk();
               
            }
            return await generatePdf.GetPdf("Views/InvestRegisterMvc/capitalReport.cshtml", capital);

        }
        public IActionResult Export()
        {
            string startdates = HttpContext.Session.GetString("startdate");
            string enddates = HttpContext.Session.GetString("enddate");
            var capital = new List<InvestmentView>();
            List<InvestmentReportModel2> rpt = new List<InvestmentReportModel2>();
            if (startdates != null)
            {
                capital = services.GetALLInvestListCapitalMk(Convert.ToDateTime(startdates), Convert.ToDateTime(enddates));
                foreach (var p in capital)
                {
                    var pps = new InvestmentReportModel2
                    {
                        Amount = p.Amount,
                        issuancebank = p.issuancebank,
                        StockName = p.StockName,
                        TransactionType = p.TransactionType,
                        Date = p.Date,
                        InvestmentType = p.InvestmentType,
                        unit = p.unit,
                        company = p.company
                    };
                    rpt.Add(pps);
                }
            }
            else
            {
                capital = services.GetALLInvestListCapitalMk();
                foreach (var p in capital)
                {
                    var pps = new InvestmentReportModel2
                    {
                        Amount = p.Amount,
                        issuancebank = p.issuancebank,
                        StockName = p.StockName,
                        TransactionType = p.TransactionType,
                        Date = p.Date,
                        InvestmentType = p.InvestmentType,
                        unit = p.unit,
                        company = p.company
                    };
                    rpt.Add(pps);
                }
            }

            var stream = new MemoryStream();

            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet2");
                workSheet.Cells.LoadFromCollection(rpt, true);
                package.Save();
            }
            stream.Position = 0;
            string excelName = $"CapitalReport-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
    }
}
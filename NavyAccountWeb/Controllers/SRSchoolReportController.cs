using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MoreLinq;
using NavyAccountCore.Core.Data;
using NavyAccountCore.Models;
using NavyAccountWeb.IServices;
using NavyAccountWeb.Models;
using NavyAccountWeb.Services;
using NavyAccountWeb.ViewModels;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Wkhtmltopdf.NetCore;
using static NavyAccountWeb.Models.SchoolFilterModels;

namespace NavyAccountWeb.Controllers
{
    public class SRSchoolReportController : Controller
    {
        private readonly string _connectionstring;
        private readonly IGeneratePdf generatePdf;
        private readonly IUnitOfWork unitofWork;
        private readonly INavyAccountDbContext context;
        private readonly IStudentRecordService recordService;
        private readonly IClaimRecordService clrecordService;
        private readonly IPaymentRecordService payrecordService;
        public SRSchoolReportController(IPaymentRecordService payrecordService,IClaimRecordService clrecordService,IStudentRecordService recordService,INavyAccountDbContext context, IGeneratePdf generatePdf, IConfiguration configuration, IUnitOfWork unitofWork)
        {
            this.generatePdf = generatePdf;
            this.unitofWork = unitofWork;
            this.context = context;
            this.recordService = recordService;
            this.clrecordService = clrecordService;
            this.payrecordService = payrecordService;
            _connectionstring = configuration.GetConnectionString("DefaultConnection");
        }
        // GET: SRClaimRecordController
        public ActionResult NominalRoll()
        {
            return View();
        }
        [Route("SRSchoolReport/PrintStudentByPdf/{SchoolId}/{ClassId}/{Status}/{ParentalStatus}/{sortby}")]
        public async Task<IActionResult> GetStudentReport(int SchoolId, int ClassId, string Status, string ParentalStatus, string sortby)
        {
            StudentFilterModel value = new StudentFilterModel();
            value.SchoolId = SchoolId;
            value.ClassId = ClassId;
            value.Status = Status;
            value.ParentalStatus = ParentalStatus;
            value.sortby = sortby;

            try
            {
                var sn= await recordService.GetStudentReport(value);
                return await generatePdf.GetPdf("Views/SRSchoolReport/NominalRollInpdf.cshtml", sn);

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [Route("SRSchoolReport/PrintStudentByExcel/{SchoolId}/{ClassId}/{Status}/{ParentalStatus}/{sortby}")]
        public async Task<IActionResult> GetStudentReportExcel(int SchoolId, int ClassId, string Status, string ParentalStatus, string sortby)
        {
            StudentFilterModel value = new StudentFilterModel();
            value.SchoolId = SchoolId;
            value.ClassId = ClassId;
            value.Status = Status;
            value.ParentalStatus = ParentalStatus;
            value.sortby = sortby;

            try
            {
                var sn= await recordService.GetStudentReport(value);
                var stream = new MemoryStream();

                int row = 2;
                using (var package = new ExcelPackage(stream))
                {
                    var workSheet = package.Workbook.Worksheets.Add("Sheet2");
                    workSheet.Cells.LoadFromCollection(sn, true);
                    package.Save();
                }

                stream.Position = 0;
                string excelName = $"NominalRollReport-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public ActionResult ClaimPaySummary()
        {
            return View();
        }
        [Route("SRSchoolReport/CLaimAndPaymentSummaryByPdf/{studentid}")]
        public async Task<IActionResult> CLaimSummaryByPdf(int studentid)
        {
            try
            {

            var claim = await clrecordService.GetStudentSummary(studentid);
                var pay = await payrecordService.GetStudentPaySummary(studentid);
                  var oq = new PaymentAndCalimReport
                 {
                     claimRecord = claim,
                     paymentRecord = pay

                 };
                 return await generatePdf.GetPdf("Views/SRSchoolReport/ClaimAndPaymentSummarypdf.cshtml", oq);

            }
            catch (Exception ex)
            {
                string message = ex.Message;
                throw;
            }
        }
        
       

    }
}

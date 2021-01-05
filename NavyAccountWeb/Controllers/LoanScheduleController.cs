using Microsoft.AspNetCore.Mvc;
using NavyAccountCore.Core.AuditService;
using NavyAccountWeb.IServices;
using NavyAccountWeb.ViewModels;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace NavyAccountWeb.Controllers.Api.ReferenceTable
{
    [SessionTimeout]
    public class LoanScheduleController : Controller
    {
        private readonly ILoanScheduleservices services;
        public LoanScheduleController(ILoanScheduleservices services)
        {
            this.services = services;
        }

        
        public ActionResult CalculateScheduleLoan(int id=0,int loantypeid=0)
        {
            var result= services.FilterAllLoanSchedule(id,loantypeid);

            var stream = new MemoryStream();
          

            int row = 2;
            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet2");
                workSheet.Cells.LoadFromCollection(result, true);
                package.Save();
            }
           

            string excelname = "LoanShedule.xlsx";



           
            stream.Position = 0;
            string excelName = $"LoanSchedule-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";


            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelname);
        }

    }
}
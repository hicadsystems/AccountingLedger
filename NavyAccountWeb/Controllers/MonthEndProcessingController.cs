using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NavyAccountCore.Core.AuditService;
using NavyAccountCore.Core.IRepositories;
using NavyAccountWeb.IServices;

namespace NavyAccountWeb.Controllers
{
    [SessionTimeout]
    public class MonthEndProcessingController : Controller
    {
        private readonly string _connectionstring;
        private readonly IFundTypeCodeService _fundTypeCode;

        public MonthEndProcessingController(IConfiguration configuration, IFundTypeCodeService fundTypeCode)
        {
            _connectionstring = configuration.GetConnectionString("DefaultConnection");
            _fundTypeCode = fundTypeCode;
        }
        public ActionResult monthend()
        {
            ViewBag.month = _fundTypeCode.GetFundTypes().FirstOrDefault().processingMonth;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index()
        {
                using (SqlConnection sqls = new SqlConnection(_connectionstring))
                {
                    using (SqlCommand cmd = new SqlCommand("npf_Monthend_Processing", sqls))
                    {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@globaluser", User.Identity.Name));
                    cmd.Parameters.Add(new SqlParameter("@fundcode", HttpContext.Session.GetString("fundtypecode")));

                       await sqls.OpenAsync();
                      await cmd.ExecuteNonQueryAsync();

                        TempData["message"] = "Uploaded Successfully";
                    }
                }
            return RedirectToAction("monthend");

           
        }

        public ActionResult Yearend()
        {
            ViewBag.month = _fundTypeCode.GetFundTypes().FirstOrDefault().processingYear;

            return View();

        }
        [HttpPost]
        public async Task<IActionResult> YearendPost()
        {
            using (SqlConnection sqls = new SqlConnection(_connectionstring))
            {
                using (SqlCommand cmd = new SqlCommand("npf_Yearend_Processing", sqls))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@globaluser", User.Identity.Name));
                    cmd.Parameters.Add(new SqlParameter("@fundcode", HttpContext.Session.GetString("fundtypecode")));

                   await sqls.OpenAsync();
                   await cmd.ExecuteNonQueryAsync();

                    TempData["message"] = "Uploaded Successfully";
                }
            }
            return View("Yearend");


        }
    }
}
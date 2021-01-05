using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NavyAccountCore.Core.Data;
using NavyAccountCore.Entities;
using NavyAccountCore.Models;
using NavyAccountWeb.IServices;

namespace NavyAccountWeb.Controllers.Api.ReferenceTable
{
    [Route("api/[LoanDisc2]")]
    [ApiController]
    public class LoanDiscController : ControllerBase
    {
        private readonly ILoandiscService loandiscService;
        private readonly string _connectionstring;
        private readonly INavyAccountDbContext context;
        private readonly IUnitOfWork unitofWork;
        public LoanDiscController(IUnitOfWork unitofWork, INavyAccountDbContext context, IConfiguration configuration, ILoandiscService loandiscService)
        {
            _connectionstring = configuration.GetConnectionString("DefaultConnection");
            this.unitofWork = unitofWork;
            this.context = context;
            this.loandiscService = loandiscService;
 
        }
        [Route("createLoandisc")]
        [HttpPost]
        public IActionResult createLoandisc([FromBody] pf_loandisc newLoan)
        {
            try
            {
                if (String.IsNullOrEmpty(newLoan.loanact))
                {
                    return Ok(new { responseCode = 500, responseDescription = "Kindly Supply Balance Sheet Code" });
                }
              
                newLoan.datecreated = DateTime.Now;
                loandiscService.AddLoandisc(newLoan);

                return Ok(new { responseCode = 200, responseDescription = "Created Successfully" });
            }
            catch (Exception ex)
            {
                return Ok(new { responseCode = 500, responseDescription = "Failed" });
            }
      

        }
    }
}
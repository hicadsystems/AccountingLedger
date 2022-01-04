using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NavyAccountCore.Core.Entities;
using NavyAccountWeb.IServices;

namespace NavyAccountWeb.Controllers.Api.Claim
{
    [Route("api/Claimtype")]
    [ApiController]
    public class ClaimtypeController : ControllerBase
    {

        private readonly IClaimTypeServices services;
        public ClaimtypeController(IClaimTypeServices services)
        {
            this.services = services;
        }

        //api/Claimtype/GetClaim/{fundtypeCode}/{serviceNo}
        [Route("GetClaim/{serviceNo}/{fundtypeCode}")]
        [HttpGet]
        public IActionResult Index(string serviceNo, string fundtypeCode)
        {
            decimal amount = 0M;
            decimal outstandAmt = 0M;
            decimal amountDue = 0M;
            decimal amt = 0M;
            if(services.fundtypeDesc(fundtypeCode).ToUpper()== "NAVIP")
            {
                amount = services.GetNavipAmount(serviceNo, fundtypeCode,out amt);
                outstandAmt = services.OutstandingLoanServices(serviceNo);
                amountDue = amount- outstandAmt;
            }

            if (services.fundtypeDesc(fundtypeCode).ToUpper() == "DEPENDANT FUND")
            {
                amount = services.GetDependentAmount(serviceNo, fundtypeCode);
                amountDue =  amount;
            }

            Npf_ClaimRegister val = new Npf_ClaimRegister();
            val.PersonID = Convert.ToInt32(serviceNo);
            val.TotalContribution = amount;
            val.amountPaid = amt;
            val.amountReceived = outstandAmt;
            val.AmountDue = amountDue;
            val.FundTypeID = fundtypeCode;
            val.bank = 0;
            val.acctno = "";
            val.Beneficiary = "";


            return Ok(new { responseCode = 200, responseDescription = "Created Successfully",val });
            //return Ok(val);
        }

        [Route("GetPersonelClaim/{serviceNo}/{fundtypeCode}")]
        [HttpGet]
        public IActionResult ViewPersonelclaim(string serviceNo, string fundtypeCode)
        {
            var claim = services.GetpersonClaim(serviceNo, fundtypeCode);
            return Ok(new { responseCode = 200, responseDescription = "Created Successfully", claim });
            //return Ok(val);
        }
    }
}
   

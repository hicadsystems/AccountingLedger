using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NavyAccountCore.Core.Entities;
using NavyAccountWeb.IServices;

namespace NavyAccountWeb.Controllers.Api.ReferenceTable
{
    [Route("api/LoanPerRank")]
    [ApiController]
    public class LoanPerRankController : ControllerBase
    {
        private readonly ILoanPerRankService service;
        public LoanPerRankController(ILoanPerRankService service)
        {
            this.service = service;
        }


        //api/LoanPerRank/getAllLoanType
        [Route("getAllLoanType")]
        [HttpGet]
        public List<Pf_loanType> getAllLoanType()
        {
            return service.GetLoan().ToList();
        }

        //api/LoanPerRank/getAllLoanPerRank
        [Route("getAllLoanPerRank")]
        [HttpGet]
        public IEnumerable<loanPerRank> Get()
        {
            return service.GetLoanTypes();
        }


        // api/LoanPerRank/createLoanPerRank
        [Route("createLoanPerRank")]
        [HttpPost]
        public IActionResult createNPfContribution([FromBody] loanPerRank value)
        {
            try
            {
                if (service.GetLoanPerRankByCode(value.loantype.Trim()).Result != null)
                {
                    return Ok(new { responseCode = 400, responseDescription = "loan type code already Exist" });
                }
                service.AddContribution(value);

                return Ok(new { responseCode = 200, responseDescription = "Created Successfully" });
            }
            catch (Exception ex)
            {
                return Ok(new { responseCode = 500, responseDescription = "Failed" });
            }
        }

        // PUT:  api/LoanPerRank/updateLoanPerRank
        [Route("updateLoanPerRank")]
        [HttpPut]
        public IActionResult updateNPFContribution([FromBody]loanPerRank value)
        {
            try
            {

                var getbal = service.GetLoanPerRankById(value.Id).Result;
                getbal.loantype = value.loantype;
                getbal.amount01 = value.amount01;
                getbal.amount02 = value.amount02;
                getbal.amount03 = value.amount03;
                getbal.amount04 = value.amount04;
                getbal.amount05 = value.amount05;
                getbal.amount06 = value.amount06;
                getbal.amount07 = value.amount07;
                getbal.amount08 = value.amount08;
                getbal.amount09 = value.amount09;
                getbal.amount10 = value.amount10;
                getbal.amount11 = value.amount11;
                getbal.amount12 = value.amount12;
                getbal.amount13 = value.amount13;
                getbal.amount14 = value.amount14;
                getbal.amount15 = value.amount15;
                getbal.amount16 = value.amount16;
                getbal.amount17 = value.amount17;
                getbal.amount18 = value.amount18;
                getbal.amount19 = value.amount19;
                getbal.amount20 = value.amount20;
                getbal.amount21 = value.amount21;
                getbal.amount22 = value.amount22;

                service.UpdateContribution(getbal);
                return Ok(new { responseCode = 200, responseDescription = "Updated Successfully" });
            }
            catch (Exception ex)
            {
                return Ok(new { responseCode = 500, responseDescription = "Failed" });
            }
        }
    }
}
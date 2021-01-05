using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NavyAccountCore.Core.Entities;
using NavyAccountWeb.IServices;

namespace NavyAccountWeb.Controllers.Api.ReferenceTable
{
    [Route("api/NpfContribution")]
    [ApiController]
    public class NpfContributionController : ControllerBase
    {
        private readonly IContributionServices service;
        public NpfContributionController(IContributionServices service)
        {
            this.service = service;
        }


        //api/NpfContribution/getAllFundType
        [Route("getAllFundType")]
        [HttpGet]
        public List<Pf_fund> getAllFundType()
        {
            return service.GetFundTypes().ToList();
        }


        //api/NpfContribution/getAllLoanType
        [Route("getAllLoanType")]
        [HttpGet]
        public List<Pf_loanType> getAllLoanType()
        {
            return service.GetLoan().ToList();
        }

        //api/NpfContribution/getAllNPFContribution
        [Route("getAllNPFContribution")]
        [HttpGet]
        public IEnumerable<npf_contributions> Get()
        {
            return service.GetAllNPFContributions().ToList();
        }


        // api/NpfContribution/createNPfContribution
        [Route("createNPfContribution")]
        [HttpPost]
        public IActionResult createNPfContribution([FromBody] npf_contributions value)
        {
            try
            {
                if (service.GetContributionByCode(value.fundtype.Trim()).Result!=null)
                {
                    return Ok(new { responseCode = 400, responseDescription = "function type code already Exist" });
                }
                service.AddContribution(value);

                return Ok(new { responseCode = 200, responseDescription = "Created Successfully" });
            }
            catch (Exception ex)
            {
                return Ok(new { responseCode = 500, responseDescription = "Failed" });
            }
        }

        // PUT: api/NpfContribution/updateNPFContribution
        [Route("updateNPFContribution")]
        [HttpPut]
        public IActionResult updateNPFContribution([FromBody]  npf_contributions value)
        {
            try
            {

                var getbal = service.GetContributionById(value.Id).Result;
                getbal.fundtype = value.fundtype;
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
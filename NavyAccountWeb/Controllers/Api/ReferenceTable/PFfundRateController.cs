using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NavyAccountCore.Core.Entities;
using NavyAccountCore.Models;
using NavyAccountWeb.IServices;

namespace NavyAccountWeb.Controllers.Api.ReferenceTable
{

    [Route("api/PFfundRate")]
    [ApiController]
    public class PFfundRateController : ControllerBase
    {
        private readonly IPFundRateService pFundRateService;
        public PFfundRateController(IPFundRateService pFundRateService)
        {
            this.pFundRateService = pFundRateService;
        }

        [Route("getAllFundType")]
        [HttpGet]
        public List<fundtypeView> getAllFundType()
        {
            return pFundRateService.GetFundList().Result;
        }

        //api/PFfundRate/createPfFunRate
        [Route("getAllPFundRate")]
        [HttpGet]
        public IEnumerable<Pf_fundRate> Get()
        {
            return pFundRateService.GetFundRateTypes();
        }


        // api/PFfundRate/updatepFundRate
        [Route("createPfFunRate")]
        [HttpPost]
        public IActionResult createPfFunRate([FromBody] Pf_fundRate value)
        {
            try
            {
                if (pFundRateService.GetFundRateTypeByCode(value.FundCode.Trim()).Result != null)
                {
                    return Ok(new { responseCode = 400, responseDescription = "function type code already Exist" });
                }
                pFundRateService.AddFundType(value);

                return Ok(new { responseCode = 200, responseDescription = "Created Successfully" });
            }
            catch (Exception ex)
            {
                return Ok(new { responseCode = 500, responseDescription = "Failed" });
            }
        }

        // PUT: api/ChartofAccount/5
        [Route("updatepFundRate")]
        [HttpPut]
        public IActionResult updatepFundRate([FromBody] Pf_fundRate value)
        {
            try
            {
                if (String.IsNullOrEmpty(value.FundCode))
                {
                    return Ok(new { responseCode = 500, responseDescription = "Kindly Supply  Code" });
                }

                var getbal = pFundRateService.GetFundRateTypeById(value.Id).Result;
                getbal.FundCode = value.FundCode;
                getbal.Interest = value.Interest;
                getbal.datecreated = value.datecreated;
                getbal.createdby = value.createdby;
                getbal.Period = value.Period;
                getbal.Rate = value.Rate;
               
                pFundRateService.UpdateFundType(getbal);
                return Ok(new { responseCode = 200, responseDescription = "Updated Successfully" });
            }
            catch (Exception ex)
            {
                return Ok(new { responseCode = 500, responseDescription = "Failed" });
            }
        }
    }
}
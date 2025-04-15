using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NavyAccountCore.Core.Entities;
using NavyAccountWeb.IServices;

namespace NavyAccountWeb.Controllers.Api.ReferenceTable
{
    [Route("api/FundTypeCode")]
    [ApiController]
    public class FundTypeCodeController : ControllerBase
    {
        private readonly IFundTypeCodeService fundTypeService;
        public FundTypeCodeController(IFundTypeCodeService fundTypeService)
        {
            this.fundTypeService = fundTypeService;
        }
        // GET: api/FundTypeCode
        [Route("getAllFundTypeCodes")]
        [HttpGet]
        public IEnumerable<npf_fundType> GetFundTypes()
        {
            var getF= fundTypeService.GetFundTypes();

            return getF;
        }
        [Route("getCurrentYear")]
        [HttpGet]
        public int GetCurrentYear()
        {
            var getF = fundTypeService.GetCurrentYear();
            return getF;
        }

        [Route("getFundTypeByCode/{fundTypeCode}")]
        [HttpGet]
        public IActionResult getFundTypeByCode(string fundTypeCode)
        {
            return 
                Ok(new { responseCode = "200", responseDescription="Successfull", data = fundTypeService.GetFundTypeCodeByCode(fundTypeCode) });
        }


    }
}

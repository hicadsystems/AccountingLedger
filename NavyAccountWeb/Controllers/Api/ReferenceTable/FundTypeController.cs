using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NavyAccountCore.Core.Entities;
using NavyAccountWeb.IServices;
using Wkhtmltopdf.NetCore;

namespace NavyAccountWeb.Controllers.Api.ReferenceTable
{
    [Route("api/FundType")]
    [ApiController]
    public class FundTypeController : ControllerBase
    {
        private readonly IFundTypeService fundtypeService;
        private readonly IGeneratePdf generatePdf;
        public FundTypeController(IGeneratePdf generatePdf,IFundTypeService fundtypeService)
        {
            this.fundtypeService = fundtypeService;
            this.generatePdf = generatePdf;
        }
        // GET: api/FundType
        [Route("getAllFundTypes")]
        [HttpGet]
        public IEnumerable<Pf_fund> Get()
        {
            var r=fundtypeService.GetFundTypes();

            return r;
        }


        //api/FundType/RemoveFundType
        [Route("RemoveFundType/{id:int}")]
        [HttpGet]
        public IActionResult Remove(int id)
        {
            var resu = fundtypeService.GetFundTypeById(id);
            if (resu == null) return NotFound();

            fundtypeService.RemoveFundType(resu.Result);

            return Ok(new { responseCode = 200, responseDescription = "Deleted Successful" });

        }

        // GET: api/FundType
        [Route("getFundTypeByCode/{id}")]
        [HttpGet]
        public IActionResult GetFundTypeByCode(string id)
        {
            var fund = fundtypeService.GetFundTypeByCode(id).Result;

            if(fund == null)
            {
                return Ok(new { responseCode = 404, responseDescription = "fund type Code does not Exist" });
            }
            return Ok(new { responseCode = 200, responseDescription = "fund type Code Exist", Data = fund });
        }

        // GET: api/FundType/5
        [Route("Getl")]
        [HttpGet("{id}" )]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/FundType
        [Route("createFundType")]
        [HttpPost]
        public IActionResult createFundType([FromBody] Pf_fund value)
        {
            try
            {
                if (String.IsNullOrEmpty(value.Code))
                {
                    return Ok(new { responseCode = 500, responseDescription = "Kindly Supply Fund Type Code" });
                }
                if (fundtypeService.GetFundTypeByCode(value.Code.Trim()).Result != null)
                {
                    return Ok(new { responseCode = 400, responseDescription = "Fund Type Code already Exist" });
                }
                value.datecreated = DateTime.Now;
                value.createdby="Hicad";
                value.startDate = DateTime.Now;
                fundtypeService.AddFundType(value);

                return Ok(new { responseCode = 200, responseDescription = "Created Successfully" });
            }
            catch (Exception ex) {
                return Ok(new { responseCode = 500, responseDescription = "Failed" });
            }
        }

        // PUT: api/FundType/5
        [Route("updateFundType")]
        [HttpPut]
        public IActionResult Put([FromBody] Pf_fund value)
        {
            try{
                if (String.IsNullOrEmpty(value.Code))
                {
                    return Ok(new { responseCode = 500, responseDescription = "Kindly Supply Fund Type Code" });
                }
                var getfund = fundtypeService.GetFundTypeByCode(value.Code.Trim()).Result;
                        getfund.Description = value.Description;
                        getfund.startDate = value.startDate;
                        getfund.processingMonth = value.processingMonth;
                        getfund.processingYear= value.processingYear;
                        getfund.interestacct = value.interestacct;
                        getfund.interestacct = value.interestacct;
                        getfund.incomeacct = value.incomeacct;
                        getfund.fundacct = value.fundacct;
                        getfund.trusteeacct = value.trusteeacct;
                        getfund.datecreated= DateTime.Now;
                        getfund.createdby= "Hicad";
                    fundtypeService.UpdateFundType(getfund);
                    return Ok(new { responseCode = 200, responseDescription = "Updated Successfully" });
            }
            catch(Exception ex){
                return Ok(new { responseCode = 500, responseDescription = "Failed" });
            }
        }
        [Route("GetReport")]
        [HttpGet]
        public async Task<IActionResult> printfund()
        {
            var listfund = fundtypeService.GetFundTypes();

            return await generatePdf.GetPdf("Views/FundType/FundReport.cshtml", listfund);
        }

    }
}
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NavyAccountCore.Core.Entities;
using NavyAccountWeb.IServices;

namespace NavyAccountWeb.Controllers.Api.ReferenceTable
{
    [Route("api/BalanceSheet")]
    [ApiController]
    public class BalanceSheetController : ControllerBase
    {
        private readonly IBalanceSheetService balanceSheetService;
        public BalanceSheetController(IBalanceSheetService balanceSheetService)
        {
            this.balanceSheetService = balanceSheetService;
        }
        // GET: api/BalanceSheet
        [Route("getAllBalanceSheets")]
        [HttpGet]
        public IEnumerable<npf_balsheet> Get()
        {
            return balanceSheetService.GetBalanceSheets();
        }

        // GET: api/BalanceSheet
        [Route("getBalanceSheetByCode/{id}")]
        [HttpGet]
        public IActionResult GetBalanceSheetByCode(string id)
        {
            var balsheet = balanceSheetService.GetBalanceSheetByCode(id).Result;

            if(balsheet == null)
            {
                return Ok(new { responseCode = 404, responseDescription = "Balance Sheet Code does not Exist" });
            }
            return Ok(new { responseCode = 200, responseDescription = "Balance Sheet Code Exist", Data = balsheet });
        }

        // GET: api/BalanceSheet/5
        [Route("Getl")]
        [HttpGet("{id}" )]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/BalanceSheet
        [Route("createBalanceSheet")]
        [HttpPost]
        public IActionResult createBalanceSheet([FromBody] npf_balsheet value)
        {
            try
            {
                if (String.IsNullOrEmpty(value.bl_code))
                {
                    return Ok(new { responseCode = 500, responseDescription = "Kindly Supply Balance Sheet Code" });
                }
                if (balanceSheetService.GetBalanceSheetByCode(value.bl_code.Trim()).Result != null)
                {
                    return Ok(new { responseCode = 400, responseDescription = "Balance Sheet Code already Exist" });
                }
                value.datecreated = DateTime.Now;
                balanceSheetService.AddBalanceSheet(value);

                return Ok(new { responseCode = 200, responseDescription = "Created Successfully" });
            }
            catch (Exception ex) {
                return Ok(new { responseCode = 500, responseDescription = "Failed" });
            }
        }

        //api/BalanceSheet/RemoveBalsheet/7
        [Route("RemoveBalsheet/{id:int}")]
        [HttpGet]
        public IActionResult Remove(int id)
        {
            var balsheet = balanceSheetService.GetBalanceSheetById(id).Result;
            if (balsheet == null) return NotFound();

            balanceSheetService.RemoveBalsheet(balsheet);
            return Ok(new { responseCode = 200, responseDescription = "Deleted Successful" });
        }


        // PUT: api/BalanceSheet/5
        [Route("updateBalanceSheet")]
        [HttpPut]
        public IActionResult Put([FromBody] npf_balsheet value)
        {
            try{
                if (String.IsNullOrEmpty(value.bl_code))
                {
                    return Ok(new { responseCode = 500, responseDescription = "Kindly Supply Balance Sheet Code" });
                }
                var getbal = balanceSheetService.GetBalanceSheetByCode(value.bl_code.Trim()).Result;
                        getbal.bl_desc = value.bl_desc;
                    balanceSheetService.UpdateBalanceSheet(getbal);
                    return Ok(new { responseCode = 200, responseDescription = "Updated Successfully" });
            }
            catch(Exception ex){
                return Ok(new { responseCode = 500, responseDescription = "Failed" });
            }
        }
        

    }
}

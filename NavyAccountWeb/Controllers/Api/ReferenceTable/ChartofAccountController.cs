using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NavyAccountCore.Core.Entities;
using NavyAccountCore.Models;
using NavyAccountWeb.IServices;

namespace NavyAccountWeb.Controllers.Api.ReferenceTable
{
    [Route("api/ChartofAccount")]
    [ApiController]
    public class ChartofAccountController : ControllerBase
    {
        private readonly IChartofAccountService chartofAccountService;
        public ChartofAccountController(IChartofAccountService chartofAccountService)
        {
            this.chartofAccountService = chartofAccountService;
        }

        // api/ChartofAccount/RemoveChart
        [Route("RemoveChart/{id:int}")]
        [HttpGet]
        public IActionResult Remove(int id)
        {
            var resu = chartofAccountService.GetChartofAccountById(id);
            if (resu == null) return NotFound();

            chartofAccountService.RemoveChart(resu.Result);

            return Ok(new { responseCode = 200, responseDescription = "Deleted Successful" });
        }


        [Route("getAllChartofAccount")]
        [HttpGet]
        public async System.Threading.Tasks.Task<IActionResult> getChart(int? pageno)
        {
            int iDisplayLength = 10;

            if (pageno == 0) pageno = 0;
            else pageno = pageno - 1;
            var chatlist= chartofAccountService.getChartofAccountAndDesc2(((int)pageno * iDisplayLength), iDisplayLength).Result;
            var countall = await chartofAccountService.getChartofAccountCount();
            return Ok(new { responseCode = 200, accountList = chatlist, total = countall });
        }

        [Route("getAllChartofAccounts")]
        [HttpGet]
        public List<ChartofAccountView> getChartofAcount()
        {
            return chartofAccountService.getChartofAccountAndDesc().Result;
        }

        // GET: api/ChartofAccount/5
        [Route("getChartofAccountById")]
        [HttpGet("{id}")]
        public string getChartofAccountById(int id)
        {
            return "value";
        }
        [Route("getChartofAccount1/{id}")]
        [HttpGet]
        public List<ChartofAccountView> getChartofAcountByCode1(string id)
        {
            return chartofAccountService.GetChartofAccountByCode1(id).Result;
        }
        [Route("getChartofAccount2/{id}")]
        [HttpGet]
        public List<ChartofAccountView> getChartofAcountByCode2(string id)
        {
            return chartofAccountService.GetChartofAccountByCode2(id).Result;
        }
        [Route("getChartofAccount3/{id}")]
        [HttpGet]
        public List<ChartofAccountView> getChartofAcountByCode3(string id)
        {
            return chartofAccountService.GetChartofAccountByCode3(id).Result;
        }
        [Route("getChartofAccount4/{id}")]
        [HttpGet]
        public List<ChartofAccountView> getChartofAcountByCode4(string id)
        {

            return chartofAccountService.GetChartofAccountByCode4(id).Result;

        }
        [Route("getChartofAccount5/{id}")]
        [HttpGet]
        public List<ChartofAccountView> getChartofAcountByCode5(string id)
        {

            return chartofAccountService.GetChartofAccountByCode5(id).Result;

        }
        [Route("getLastChartofAccount/{id}")]
        [HttpGet]
        public IActionResult getLastChartofAcountByCode(string id)
        {

            var chartofact = chartofAccountService.GetLastChartofAccountByCode(id).Result;
            if (chartofact == null)
            {
                return Ok(new { responseCode = 200, responseDescription = "", Data = 0 });
            }
            return Ok(new { responseCode = 200, responseDescription = "Chart of Account Code Exist", Data = chartofact });
        }

        // POST: api/ChartofAccount
        [Route("createChartofAccount")]
        [HttpPost]
        public IActionResult addNewChartofAccount([FromBody] npf_chart value)
        {
            try
            {
                if (chartofAccountService.GetChartofAccountByCode(value.acctcode.Trim()).Result != null)
                {
                    return Ok(new { responseCode = 400, responseDescription = "Balance Sheet Code already Exist" });
                }
                chartofAccountService.AddChartofAccount(value);

                return Ok(new { responseCode = 200, responseDescription = "Created Successfully" });
            }
            catch (Exception ex) {
                return Ok(new { responseCode = 500, responseDescription = "Failed" });
            }
        }

        // PUT: api/ChartofAccount/5
        [Route("updateChartofAccount")]
        [HttpPut]
        public IActionResult updateChartofAccount( [FromBody] npf_chart value)
        {
            try{
                if (String.IsNullOrEmpty(value.acctcode))
                {
                    return Ok(new { responseCode = 500, responseDescription = "Kindly Supply Chart of Account Code" });
                }
                var getbal = chartofAccountService.GetChartofAccountByCode(value.acctcode.Trim()).Result;
                        getbal.description = value.description;
                     chartofAccountService.UpdateChartofAccount(getbal);
                    return Ok(new { responseCode = 200, responseDescription = "Updated Successfully" });
            }
            catch(Exception ex){
                return Ok(new { responseCode = 500, responseDescription = "Failed" });
            }
        }

    }
}

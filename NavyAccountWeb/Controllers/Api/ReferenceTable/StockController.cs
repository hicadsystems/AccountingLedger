using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NavyAccountCore.Core.Entities;
using NavyAccountWeb.IServices;

namespace NavyAccountWeb.Controllers.Api.ReferenceTable
{
    [Route("api/Stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockService StockService;
        public StockController(IStockService StockService)
        {
            this.StockService = StockService;
        }
        // GET: api/Stock
        [Route("getAllStocks")]
        [HttpGet]
        public IEnumerable<npf_Stocks> Get()
        {
            return StockService.GetStocks();
        }

        // GET: api/Stock
        [Route("getStockByCode/{id}")]
        [HttpGet]
        public IActionResult GetStockByCode(string id)
        {
            var stock = StockService.GetStockByCode(id).Result;

            if(stock == null)
            {
                return Ok(new { responseCode = 404, responseDescription = "Balance Sheet Code does not Exist" });
            }
            return Ok(new { responseCode = 200, responseDescription = "Balance Sheet Code Exist", Data = stock });
        }

        // GET: api/Stock/5
        [Route("Getl")]
        [HttpGet("{id}" )]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Stock
        [Route("createStock")]
        [HttpPost]
        public IActionResult createStock([FromBody] npf_Stocks value)
        {
            try
            {
                if (value == null)
                {
                    return Ok(new { responseCode = 404, responseDescription = "Model can not be empty" });
                }
                if (StockService.GetStockByCode(value.stockname.Trim()).Result != null)
                {
                    return Ok(new { responseCode = 400, responseDescription = "stock name already Exist" });
                }
                value.datecreated = DateTime.Now;
                StockService.AddStock(value);

                return Ok(new { responseCode = 200, responseDescription = "Created Successfully" });
            }
            catch (Exception ex) {
                return Ok(new { responseCode = 500, responseDescription = "Failed" });
            }
        }

        //api/Stock/RemoveBalsheet/7
        [Route("RemoveStock/{id:int}")]
        [HttpGet]
        public IActionResult Remove(int id)
        {
            var stock = StockService.GetStockById(id).Result;
            if (stock == null) return NotFound();

            StockService.RemoveStock(stock);
            return Ok(new { responseCode = 200, responseDescription = "Deleted Successful" });
        }


        // PUT: api/Stock/5
        [Route("updateStock")]
        [HttpPut]
        public IActionResult Put([FromBody] npf_Stocks value)
        {
            try{
                if (value==null)
                {
                    return Ok(new { responseCode = 404, responseDescription = "Model can not be empty" });
                }
                var getbal = StockService.GetStockByCode(value.stockname.Trim()).Result;
                        getbal.description = value.description;
                    StockService.UpdateStock(getbal);
                    return Ok(new { responseCode = 200, responseDescription = "Updated Successfully" });
            }
            catch(Exception ex){
                return Ok(new { responseCode = 500, responseDescription = "Failed" });
            }
        }
        

    }
}

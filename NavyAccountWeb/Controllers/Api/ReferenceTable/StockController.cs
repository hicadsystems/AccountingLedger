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
        private readonly IStockService stockService;
        public StockController(IStockService stockService)
        {
            this.stockService = stockService;
        }
        // GET: api/StockSS
        [Route("getAllStocks")]
        [HttpGet]
        public IEnumerable<npf_stock> Get()
        {
            return stockService.GetStocks();
        }

        // GET: api/Stock
        [Route("getStockByCode/{id}")]
        [HttpGet]
        public IActionResult GetStockByCode(string id)
        {
            var balsheet = stockService.GetStockByCode(id).Result;

            if(balsheet == null)
            {
                return Ok(new { responseCode = 404, responseDescription = "Balance Sheet Code does not Exist" });
            }
            return Ok(new { responseCode = 200, responseDescription = "Balance Sheet Code Exist", Data = balsheet });
        }

        // GET: api/Stock/5
        [Route("Get")]
        [HttpGet("{id}" )]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Stock
        [Route("createStock")]
        [HttpPost]
        public IActionResult createStock([FromBody] npf_stock value)
        {
            try
            {
                if (String.IsNullOrEmpty(value.stockname))
                {
                    return Ok(new { responseCode = 500, responseDescription = "Kindly Supply Balance Sheet Code" });
                }
                if (stockService.GetStockByCode(value.stockname.Trim()).Result != null)
                {
                    return Ok(new { responseCode = 400, responseDescription = "Balance Sheet Code already Exist" });
                }
                value.datecreated = DateTime.Now;
                stockService.AddStock(value);

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
            var stock = stockService.GetStockById(id).Result;
            if (stock == null) return NotFound();

            stockService.RemoveStock(stock);
            return Ok(new { responseCode = 200, responseDescription = "Deleted Successful" });
        }


        // PUT: api/Stock/5
        [Route("updateStock")]
        [HttpPut]
        public IActionResult Put([FromBody] npf_stock value)
        {
            try{
                if (String.IsNullOrEmpty(value.stockname))
                {
                    return Ok(new { responseCode = 500, responseDescription = "Kindly Enter Stock" });
                }
                var getbal = stockService.GetStockByCode(value.stockname.Trim()).Result;
                        getbal.description = value.description;
                    stockService.UpdateStock(getbal);
                    return Ok(new { responseCode = 200, responseDescription = "Updated Successfully" });
            }
            catch(Exception ex){
                return Ok(new { responseCode = 500, responseDescription = "Failed" });
            }
        }
        

    }
}

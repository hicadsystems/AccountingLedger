using Microsoft.AspNetCore.Mvc;
using NavyAccountCore.Entities;
using NavyAccountWeb.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NavyAccountWeb.Controllers.Api.StudentInsurance
{
    [Route("api/Payment")]
    [ApiController]
    public class PaymentRecordApiController : ControllerBase
    {
        private readonly IPaymentRecordService recordService;
        public PaymentRecordApiController(IPaymentRecordService recordService)
        {
            this.recordService = recordService;
        }
        // GET: api/<PaymenRecordApiController>
        [Route("GetAll")]
        [HttpGet]
        public async Task<IEnumerable<sr_PaymentRecord>> GetAll()
        {
            return await recordService.GetAllPayment();
        }

        // GET api/<SchoolRecordApiController>/5
        [Route("GetRecordbyCode/{code}")]
        [HttpGet("{id}")]
        public IActionResult GetByCode(string code)
        {
            var result = recordService.GetPaymentByCode(code);
            if (result == null)
                return Ok(new { responseCode = "404", responseDescription = "Dose not Exist" });
            else
                return Ok(new { responseCode = "200", responseDescription = "Successfull", data = result });
        }

        // POST api/<SchoolRecordApiController>
        [Route("Add")]
        [HttpPost]
        public IActionResult Addschool([FromBody] sr_PaymentRecord value)
        {
            try
            {
                if (recordService.GetPaymentByCode(value.Reg_Number).Result != null)
                {
                    return Ok(new { responseCode = 400, responseDescription = "Already Exist" });
                }
                value.CreatedBy = User.Identity.Name;
                value.CreatedDate = DateTime.Now;
                recordService.AddPayment(value);

                return Ok(new { respnseCode = 200, ResponseDescription = "Successfully Added" });
            }
            catch (Exception ex)
            {
                return Ok(new { responseCode = 500, responseDescription = ex.Message });
            }
        }

        // PUT api/<SchoolRecordApiController>/5
        [Route("Update/{id}")]
        [HttpPut]
        public IActionResult Update(int id, [FromBody] sr_PaymentRecord value)
        {
            try
            {
                var sch = recordService.GetPaymentByid(id).Result;
                if (sch == null)
                {
                    return Ok(new { responseCode = 400, responseDescription = "Not Found" });
                }
                sch.Reg_Number = value.Reg_Number;
                sch.Period = value.Period;
                sch.Transdate = value.Transdate;
                sch.Amount = value.Amount;
                sch.CreatedDate = DateTime.Now;
                sch.CreatedBy = User.Identity.Name;


                recordService.UpdatePayment(sch);
                return Ok(new { respnseCode = 200, ResponseDescription = "Successfully Updated" });


            }
            catch (Exception ex)
            {

                return Ok(new { respnseCode = 500, ResponseDescription = ex.Message });
            }
        }

        // DELETE api/<SchoolRecordApiController>/5
        [Route("Delete/{id}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var sch = recordService.GetPaymentByid(id).Result;
            if (sch == null)
            {
                return Ok(new { responseCode = 400, responseDescription = "Not Found" });
            }
            recordService.DeletePayment(sch);
            return Ok(new { respnseCode = 200, ResponseDescription = "Successfully Deleted" });
        }
    }
}


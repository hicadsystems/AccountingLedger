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
    [Route("api/SchoolFee")]
    [ApiController]
    public class SchoolFeeApiController : ControllerBase
    {
        private readonly ISchoolFeeService recordService;
        public SchoolFeeApiController(ISchoolFeeService recordService)
        {
            this.recordService = recordService;    
        }
        // GET: api/<SchoolRecordApiController> api/SchoolRecord/GetAll
        [Route("GetAll")]
        [HttpGet]
        public async Task<List<SchoolFeeVM>> GetAll()
        {
            return await recordService.GetAllSchoolFee();
        }
        

        // GET api/<SchoolRecordApiController>/5 
        [Route("GetRecordbyCode/{code}")]    //api/SchoolRecord/GetRecordbyCode/{code}
        [HttpGet]
        public async Task<IActionResult> GetByCode(string code)
        {
            var result = await recordService.GetAllSchoolFeeByCode(code);
            if (result == null)
                return Ok(new { responseCode = "404", responseDescription = "School Dose not Exist" });
            else
           return Ok(new { responseCode = "200", responseDescription = "Successfull", data = result });
        }

        [Route("GetschoolFeeByName/{school}")]    //api/SchoolRecord/GetschoolByName/{school}
        [HttpGet]
        public async Task<IActionResult> GetschoolFeeByName(string school)
        {
            var result = await recordService.GetSchoolFeeByName(school);
            if (result == null)
                return Ok(new { responseCode = "404", responseDescription = "School Dose not Exist" });
            else
                return Ok(new { responseCode = "200", responseDescription = "Successfull", data = result });
        }

        // POST api/<SchoolRecordApiController>
        [Route("AddSchoolFee")]
        [HttpPost]
        public IActionResult AddSchoolFee([FromBody] sr_SchoolFeeTB value)
        {
            try
            {
                if (recordService.GetAllSchoolFeeByCode(value.Period).Result!=null){
                    return Ok(new { responseCode = 400, responseDescription = "School Name Already Exist" });
                }
                value.CreatedDate = DateTime.Now;
                value.CreatedBy = User.Identity.Name;
                recordService.AddSchoolFee(value);

                return Ok(new { respnseCode = 200, ResponseDescription = "School Successfully Added" });
            }
            catch (Exception ex)
            {
                return Ok(new {responseCode=500,responseDescription=ex.Message });
            }
        }

        // PUT api/<SchoolRecordApiController>/5
        [Route("Update/{id}")]
        [HttpPut]
        public IActionResult Update(int id, [FromBody] sr_SchoolFeeTB value)
        {
            try
            {
                var sch = recordService.GetSchoolFeeByid(id).Result;
                if (sch==null)
                {
                    return Ok(new { responseCode = 400, responseDescription = "School Not Found" });
                }
                sch.Period = value.Period;
                sch.SchoolId = value.SchoolId;
                sch.ClassId = value.ClassId;
                sch.ClassCategory = value.ClassCategory;
                sch.Amount = value.Amount;
                recordService.UpdateSchoolFee(sch);
                return Ok(new { respnseCode = 200, ResponseDescription = "School Successfully Updated" });


            }
            catch (Exception ex)
            {

                return Ok(new { respnseCode = 500, ResponseDescription = ex.Message });
            }
        }

        // DELETE api/<SchoolRecordApiController>/5
        [Route("DeleteRecord/{id:int}")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var sch = recordService.GetSchoolFeeByid(id).Result;
            if (sch == null)
            {
                return Ok(new { responseCode = 400, responseDescription = "School Not Found" });
            }
            recordService.DeleteSchoolFee(sch);
            return Ok(new { respnseCode = 200, ResponseDescription = "School Successfully Deleted" });
        }
    }
}

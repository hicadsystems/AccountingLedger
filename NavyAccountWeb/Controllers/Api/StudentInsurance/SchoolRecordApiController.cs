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
    [Route("api/SchoolRecord")]
    [ApiController]
    public class SchoolRecordApiController : ControllerBase
    {
        private readonly ISchoolRecordService recordService;
        public SchoolRecordApiController(ISchoolRecordService recordService)
        {
            this.recordService = recordService;    
        }
        // GET: api/<SchoolRecordApiController> api/SchoolRecord/GetAll
        [Route("GetAll")]
        [HttpGet]
        public async Task<IEnumerable<sr_SchoolRecord>> GetAll()
        {
            return await recordService.GetAllSchool();
        }
        

        // GET api/<SchoolRecordApiController>/5 
        [Route("GetRecordbyCode/{code}")]    //api/SchoolRecord/GetRecordbyCode/{code}
        [HttpGet]
        public async Task<IActionResult> GetByCode(string code)
        {
            var result = await recordService.GetAllSchoolByCode(code);
            if (result == null)
                return Ok(new { responseCode = "404", responseDescription = "School Dose not Exist" });
            else
           return Ok(new { responseCode = "200", responseDescription = "Successfull", data = result });
        }

        [Route("GetschoolByName/{school}")]    //api/SchoolRecord/GetschoolByName/{school}
        [HttpGet]
        public async Task<IActionResult> GetschoolByName(string school)
        {
            var result = await recordService.GetSchoolByName(school);
            if (result == null)
                return Ok(new { responseCode = "404", responseDescription = "School Dose not Exist" });
            else
                return Ok(new { responseCode = "200", responseDescription = "Successfull", data = result });
        }

        // POST api/<SchoolRecordApiController>
        [Route("AddSchool")]
        [HttpPost]
        public IActionResult Addschool([FromBody] sr_SchoolRecord value)
        {
            try
            {
                if (recordService.GetAllSchoolByCode(value.Schoolname).Result!=null){
                    return Ok(new { responseCode = 400, responseDescription = "School Name Already Exist" });
                }
                value.CreatedDate = DateTime.Now;
                value.CreatedBy = User.Identity.Name;
                recordService.AddSchool(value);

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
        public IActionResult Update(int id, [FromBody] sr_SchoolRecord value)
        {
            try
            {
                var sch = recordService.GetSchoolByid(id).Result;
                if (sch==null)
                {
                    return Ok(new { responseCode = 400, responseDescription = "School Not Found" });
                }
                sch.SchoolAddress = value.SchoolAddress;
                sch.SchoolCity = value.SchoolCity;
                sch.SchoolCode = value.SchoolCode;
                sch.Schoolname = value.Schoolname;
                sch.SchoolState = value.SchoolState;
                sch.SchoolType = value.SchoolType;
                recordService.UpdateSchool(sch);
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
            var sch = recordService.GetSchoolByid(id).Result;
            if (sch == null)
            {
                return Ok(new { responseCode = 400, responseDescription = "School Not Found" });
            }
            recordService.DeleteSchool(sch);
            return Ok(new { respnseCode = 200, ResponseDescription = "School Successfully Deleted" });
        }
    }
}

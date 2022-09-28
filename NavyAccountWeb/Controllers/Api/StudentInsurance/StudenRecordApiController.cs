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
    [Route("api/StudentRecord")]
    [ApiController]
    public class StudenRecordApiController : ControllerBase
    {
        private readonly IStudentRecordService recordService;
        public StudenRecordApiController(IStudentRecordService recordService)
        {
            this.recordService = recordService;
        }
        // GET: api/<StudenRecordApiController>
        [Route("GetAll")]
        [HttpGet]
        public async Task<IEnumerable<sr_StudentRecord>> GetAll()
        {
            return await recordService.GetAllStudent();
        }

        // GET api/<SchoolRecordApiController>/5
        [Route("GetRecordbyCode/{code}")]
        [HttpGet("{id}")]
        public IActionResult GetByCode(string code)
        {
            var result = recordService.GetStudentByCode(code);
            if (result == null)
                return Ok(new { responseCode = "404", responseDescription = "Dose not Exist" });
            else
                return Ok(new { responseCode = "200", responseDescription = "Successfull", data = result });
        }

        // POST api/<SchoolRecordApiController>
        [Route("Add")]
        [HttpPost]
        public IActionResult Addschool([FromBody] sr_StudentRecord value)
        {
            try
            {
                if (recordService.GetStudentByCode(value.Reg_Number).Result != null)
                {
                    return Ok(new { responseCode = 400, responseDescription = "Already Exist" });
                }
                //value.CommencementDate = DateTime.Now;
                //value.CreatedBy = User.Identity.Name;
                recordService.AddStudent(value);

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
        public IActionResult Update(int id, [FromBody] sr_StudentRecord value)
        {
            try
            {
                var sch = recordService.GetStudentByid(id).Result;
                if (sch == null)
                {
                    return Ok(new { responseCode = 400, responseDescription = "Not Found" });
                }
                sch.Reg_Number = value.Reg_Number;
                sch.Surname = value.Surname;
                sch.MiddleName = value.MiddleName;
                sch.FirstName = value.FirstName;
                sch.Sex = value.Sex;
                sch.SchoolCode = value.SchoolCode;
                sch.PhoneNumber = value.PhoneNumber;
                sch.ParentalStatus = value.ParentalStatus;
                sch.Status = value.Status;
                sch.Age = value.Age;

                recordService.UpdateStudent(sch);
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
            var sch = recordService.GetStudentByid(id).Result;
            if (sch == null)
            {
                return Ok(new { responseCode = 400, responseDescription = "Not Found" });
            }
            recordService.DeleteStudent(sch);
            return Ok(new { respnseCode = 200, ResponseDescription = "Successfully Deleted" });
        }
    }
}

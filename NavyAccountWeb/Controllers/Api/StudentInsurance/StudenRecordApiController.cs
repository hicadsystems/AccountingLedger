using Microsoft.AspNetCore.Mvc;
using NavyAccountCore.Entities;
using NavyAccountCore.Models;
using NavyAccountWeb.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static NavyAccountWeb.Models.SchoolFilterModels;

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

        [Route("getAllSTudents")]
        [HttpGet]
        public async Task<IActionResult> Get(int? pageno)
        {
            int iDisplayLength = 10;
            pageno = pageno == null ? 0 : (pageno--);
            var _studentlist = await recordService.GetStudentList(((int)pageno * iDisplayLength), iDisplayLength);
            var countall = await recordService.getStudentListCount();
            return Ok(new { responseCode = 200, studentlist = _studentlist, total = countall });
        }

        [Route("getAllInactivestudents")]
        [HttpGet]
        public async Task<IActionResult> Getinactive(int? pageno)
        {
            int iDisplayLength = 10;
            pageno = pageno == null ? 0 : (pageno--);
            var _studentlist = await recordService.GetInactiveStudentList(((int)pageno * iDisplayLength), iDisplayLength);
            //var countall = await recordService.getStudentListCount();
            return Ok(new { responseCode = 200, studentlist = _studentlist});
        }

        
        [Route("getPersonByID2/{id}")]
        [HttpGet]
        public IActionResult getBySTudentById(int id)
        {
            var pers = recordService.GetStudentByid(id).Result;
            sr_StudentRecord pn = new sr_StudentRecord();
            pn.id = pers.id;
            pn.Reg_Number = pers.Reg_Number;
            pn.Surname = pers.Surname;
            pn.FirstName = pers.FirstName;
            pn.MiddleName = pers.MiddleName;
            pn.Email = pers.Email;
            pn.Age = pers.Age;
            pn.Sex = pers.Sex;
            pn.ParentalStatus = pers.ParentalStatus;
            pn.SchoolCode = pers.SchoolCode;
            pn.PhoneNumber = pers.PhoneNumber;
            pn.Class = pers.Class;
            pn.ClassCategory = pers.ClassCategory;

            return Ok(new { responseCode = 200, pn });
        }
        [Route("getAllStudentByNameLimited/{studentname}")]
        [HttpGet]
        public List<Searchby_Name> getBySTudentNameLimited(string studentname)
        {
            List<Searchby_Name> pp = new List<Searchby_Name>();
            var result = recordService.GetStudentListByName(studentname).Result;
            foreach (var v in result)
            {
                pp.Add(new Searchby_Name()
                {
                    Id = v.id,
                    name = v.FirstName + " " + v.Surname + " " + v.MiddleName + "_" + v.Reg_Number
                });
            }
            return pp;
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

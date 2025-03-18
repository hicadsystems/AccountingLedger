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
        [Route("GetStudentById2/{id}")]
        [HttpGet]
        public sr_StudentRecord GetStudentById(int id)
        {
            return recordService.GetAllStudentByID(id).Result;

        }
        [Route("getAllSTudents/{schoolid}")]
        [HttpGet]
        public async Task<IActionResult> Get(int schoolid,int? pageno)
        {
            int iDisplayLength = 10;
            pageno = pageno == null ? 0 : (pageno--);
            var _studentlist = await recordService.GetStudentList(schoolid,((int)pageno * iDisplayLength), iDisplayLength);
            //var _studentlist = await recordService.GetStudentList(((int)pageno * iDisplayLength), iDisplayLength);
            var countall = await recordService.getStudentListCount();
            return Ok(new { responseCode = 200, studentlist = _studentlist, total = countall });
        }
        [Route("getAllSTudent2s")]
        [HttpGet]
        public async Task<IActionResult> Get2(int? pageno)
        {
            int iDisplayLength = 10;
            pageno = pageno == null ? 0 : (pageno--);
            var _studentlist = await recordService.GetStudentList2(((int)pageno * iDisplayLength), iDisplayLength);
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
            var countall = await recordService.getInactiveStudentListCount();
            return Ok(new { responseCode = 200, studentlist = _studentlist, total = countall });
        }

        
        [Route("getStudentByID/{id}")]
        [HttpGet]
        public IActionResult getByStudentById(int id)
        {
            var pers = recordService.GetStudentListByID(id);
            StudentRecordVM pn = new StudentRecordVM();
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
            pn.ClassName = pers.ClassName;
            pn.ParentName = pers.ParentName;
            pn.GuardianName = pers.GuardianName;
            pn.ClassCategory = pers.ClassCategory;
            pn.ExitReason = pers.ExitReason;
            pn.ExitDate=pers.ExitDate;

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

        [Route("getOldStudentByID/{id}")]
        [HttpGet]
        public IActionResult getOldByStudentById(int id)
        {
            var pers = recordService.GetOldStudentListByID(id);
            StudentRecordVM pn = new StudentRecordVM();
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
            pn.ClassName = pers.ClassName;
            pn.ParentName = pers.ParentName;
            pn.GuardianName = pers.GuardianName;
            pn.ClassCategory = pers.ClassCategory;
            pn.ExitReason = pers.ExitReason;
            pn.ExitDate = pers.ExitDate;

            return Ok(new { responseCode = 200, pn });
        }
        [Route("getAllOldStudentByNameLimited/{studentname}")]
        [HttpGet]
        public List<Searchby_Name> getOldByStudentNameLimited(string studentname)
        {
            List<Searchby_Name> pp = new List<Searchby_Name>();
            var result = recordService.GetOldStudentListByName(studentname).Result;
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
        [Route("CreateStudent")]
        [HttpPost]
        public IActionResult CreateStudent([FromBody] sr_StudentRecord value)
        {
            try
            {
                if (recordService.GetStudentByCode(value.Reg_Number).Result != null)
                {
                    return Ok(new { responseCode = 400, responseDescription = "Already Exist" });
                }
                //value.CommencementDate = DateTime.Now;
                //value.CreatedBy= User.Identity.Name;
                recordService.AddStudent(value);

                return Ok(new { responseCode = 200, responseDescription = "Successfully Added" });
            }
            catch (Exception ex)
            {
                return Ok(new { responseCode = 500, responseDescription = ex.Message });
            }
        }
        [Route("getStudentNorminalRoll/{SchoolId}/{ClassId}/{Status}/{ParentalStatus}/{sortby}")]
        [HttpGet]
        public async Task<IEnumerable<StudentRecordVM>> GetStudentReport(int SchoolId,int ClassId,string Status,string ParentalStatus, string sortby)
        {
            StudentFilterModel value = new StudentFilterModel();
            value.SchoolId = SchoolId;
            value.ClassId = ClassId;
            value.Status = Status;
            value.ParentalStatus = ParentalStatus;
            value.sortby = sortby;

            try
            {
                return await recordService.GetStudentReport(value);
                
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [Route("getStudentNorminalRoll2/{SchoolId}/{ClassId}/{Status}/{ParentalStatus}/{sortby}")]
        [HttpGet]
        public async Task<IEnumerable<StudentRecordVM>> GetStudentReport2(StudentFilterModel value)
        {
            //StudentFilterModel value = new StudentFilterModel();
            //value.SchoolId = SchoolId;
            //value.ClassId = ClassId;
            //value.Status = Status;
            //value.ParentalStatus = ParentalStatus;
            //value.sortby = sortby;

            try
            {
                return await recordService.GetStudentReport(value);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // PUT api/<SchoolRecordApiController>/5
        [Route("Update")]
        [HttpPut]
        public IActionResult Update([FromBody] sr_StudentRecord pers)
        {
            try
            {
                var sch = recordService.GetStudentByCode(pers.Reg_Number).Result;
                if (sch == null)
                {
                    return Ok(new { responseCode = 400, responseDescription = "Not Found" });
                }
                //sch.id = pers.id;
                sch.Reg_Number = pers.Reg_Number;
                sch.Surname = pers.Surname;
                sch.FirstName = pers.FirstName;
                sch.MiddleName = pers.MiddleName;
                sch.Email = pers.Email;
                sch.Age = pers.Age;
                sch.Sex = pers.Sex;
                sch.ParentalStatus = pers.ParentalStatus;
                sch.PhoneNumber = pers.PhoneNumber;
                sch.ClassCategory = pers.ClassCategory;
                sch.Parentid = pers.Parentid;
                sch.Guardianid = pers.Guardianid;
                sch.ClassId = pers.ClassId;
                sch.SchoolId = pers.SchoolId;
                if (pers.ExitDate != null)
                {
                    sch.Status = pers.Status;
                    sch.ExitDate = pers.ExitDate;
                    sch.ExitReason = pers.ExitReason;
                }

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

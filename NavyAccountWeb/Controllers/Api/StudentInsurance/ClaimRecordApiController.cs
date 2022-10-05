using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NavyAccountCore.Entities;
using NavyAccountWeb.IServices;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NavyAccountWeb.Controllers.Api.StudentInsurance
{
    [Route("api/StudentClaim")]
    [ApiController]
    public class ClaimRecordApiController : ControllerBase
    {
        private readonly IClaimRecordService recordService;
        private readonly IStudentRecordService stdrecordService;
        private readonly string _connectionstring;
        public ClaimRecordApiController(IClaimRecordService recordService, IStudentRecordService stdrecordService, IConfiguration configuration)
        {
            this.recordService = recordService;
            this.stdrecordService = stdrecordService;
            _connectionstring = configuration.GetConnectionString("DefaultConnection");
        }
        // GET: api/<PaymenRecordApiController>
        [Route("GetAll")]
        [HttpGet]
        public async Task<IEnumerable<sr_ClaimRecord>> GetAll()
        {
            return await recordService.GetAllClaim();
        }
        [Route("GetStudentClaim/{studentNo}")]
        [HttpGet]
        public IActionResult Index(string studentNo)
        {
            decimal amount = 0M;
            decimal amountDue = 0M;
            decimal amt = 0M;
            var stud = stdrecordService.GetStudentByid(Convert.ToInt32(studentNo)).Result;
            if (stud!=null)
            {
                amount = recordService.GetAmountPerSchoolType(studentNo, out amt);
                amountDue = amount;
            }

            sr_ClaimRecord val = new sr_ClaimRecord();
            val.id = stud.id;
            val.Transdate = DateTime.Now;
            val.Amount = amountDue;
            val.Reg_Number = stud.Reg_Number;
            val.CreatedBy = stud.Surname + " " + stud.FirstName + " " + stud.MiddleName;
            val.CreatedDate = DateTime.Now;

            return Ok(new { responseCode = 200, responseDescription = "Created Successfully", val });
        }


        // GET api/<SchoolRecordApiController>/5
        [Route("GetRecordbyCode/{code}")]
        [HttpGet("{id}")]
        public IActionResult GetByCode(string code)
        {
            var result = recordService.GetClaimRecordByCode(code);
            if (result == null)
                return Ok(new { responseCode = "404", responseDescription = "Dose not Exist" });
            else
                return Ok(new { responseCode = "200", responseDescription = "Successfull", data = result });
        }

        // POST api/<SchoolRecordApiController>
        [Route("Add")]
        [HttpPost]
        public IActionResult Addschool([FromBody] sr_ClaimRecord value)
        {
            try
            {
                if (recordService.GetClaimRecordByCode(value.Reg_Number).Result != null)
                {
                    return Ok(new { responseCode = 400, responseDescription = "Already Exist" });
                }
                value.CreatedBy = User.Identity.Name;
                value.CreatedDate = DateTime.Now;
                recordService.AddClaimRecord(value);

                return Ok(new { respnseCode = 200, ResponseDescription = "Successfully Added" });
            }
            catch (Exception ex)
            {
                return Ok(new { responseCode = 500, responseDescription = ex.Message });
            }
        }

        // PUT api/<SchoolRecordApiController>/5
        [Route("UpdateCLaim")]
        [HttpPut]
        public IActionResult Update(int id, [FromBody] sr_ClaimRecord value)
        {
            try
            {
                var sch = recordService.GetClaimByid(id).Result;
                if (sch == null)
                {
                    return Ok(new { responseCode = 400, responseDescription = "Not Found" });
                }
                using (SqlConnection sqls = new SqlConnection(_connectionstring))
                {
                    using (SqlCommand cmd = new SqlCommand("sr_initiateclaim", sqls))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@globaluser", User.Identity.Name));
                        cmd.Parameters.Add(new SqlParameter("@regno", value.Reg_Number));
                        cmd.Parameters.Add(new SqlParameter("@amount", value.Amount));

                        sqls.OpenAsync();
                        cmd.ExecuteNonQuery();


                    }
                }
                return Ok(new { respnseCode = 200, ResponseDescription = "Successfully Initiated" });


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
            var sch = recordService.GetClaimByid(id).Result;
            if (sch == null)
            {
                return Ok(new { responseCode = 400, responseDescription = "Not Found" });
            }
            recordService.DeleteClaimRecord(sch);
            return Ok(new { respnseCode = 200, ResponseDescription = "Successfully Deleted" });
        }
    }
}


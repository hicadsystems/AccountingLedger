using Microsoft.AspNetCore.Mvc;
using NavyAccountCore.Entities;
using NavyAccountWeb.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static NavyAccountWeb.Models.SchoolFilterModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NavyAccountWeb.Controllers.Api.GuardianInsurance
{
    [Route("api/GuardianRecord")]
    [ApiController]
    public class GuardianRecordApiController : ControllerBase
    {
        // GET: api/<GuardianRecordApiController>
        private readonly IGuardianRecordService recordService;
        public GuardianRecordApiController(IGuardianRecordService recordService)
        {
            this.recordService = recordService;
        }
        [Route("getAllGuardians")]
        [HttpGet]
        public async Task<IActionResult> Get(int? pageno)
        {
            int iDisplayLength = 10;
            pageno = pageno == null ? 0 : (pageno--);
            var _Guardianlist = await recordService.GetGuardianList(((int)pageno * iDisplayLength), iDisplayLength);
            var countall = await recordService.getGuardianListCount();
            return Ok(new { responseCode = 200, Guardianlist = _Guardianlist, total = countall });
        }
        [Route("getAllGuardianByNameLimited/{Guardianname}")]
        [HttpGet]
        public List<Searchby_Name> getByNameLimited(string Guardianname)
        {
            List<Searchby_Name> pp = new List<Searchby_Name>();
            var result = recordService.GetGuardianListByName(Guardianname).Result;
            foreach (var v in result)
            {
                pp.Add(new Searchby_Name()
                {
                    Id = v.id,
                    name = v.Surname + " " + v.OtherNames 
                });
            }
            return pp;
        }

        // GET: api/<ParenRecordApiController>
        [Route("GetAll")]
        [HttpGet]
        public async Task<IEnumerable<sr_GuardianRecord>> GetAll()
        {
            return await recordService.GetAllGuardian();
        }
        [Route("GetGuardianName")]
        [HttpGet]
        public List<Searchby_Name> GetGuardianName()
        {
            List<Searchby_Name> pp = new List<Searchby_Name>();
            var result = recordService.GetAllGuardian().Result;
            foreach (var v in result)
            {
                pp.Add(new Searchby_Name()
                {
                    Id = v.id,
                    name = v.Surname + " " + v.OtherNames
                });
            }
            return pp;
        }
        [Route("GetGuardianById/{id:int}")]
        [HttpGet]
        public IActionResult GetGuardianById(int id)
        {
            var pers = recordService.GetGuardianByid(id).Result;
            sr_GuardianRecord Guardian = new sr_GuardianRecord();
            Guardian.id = pers.id;
            Guardian.Surname = pers.Surname;
            Guardian.OtherNames = pers.OtherNames;
            Guardian.Email = pers.Email;
            Guardian.Address = pers.Address;
            Guardian.PhoneNumber = pers.PhoneNumber;
            Guardian.Workclass = pers.Workclass;

            return Ok(new { responseCode = 200, Guardian });
        }

        // GET api/<SchoolRecordApiController>/5
        [Route("GetRecordbyCode/{code}")]
        [HttpGet("{id}")]
        public IActionResult GetByCode(string code)
        {
            var result = recordService.GetGuardianByCode(code);
            if (result == null)
                return Ok(new { responseCode = "404", responseDescription = "Dose not Exist" });
            else
                return Ok(new { responseCode = "200", responseDescription = "Successfull", data = result });
        }

        // POST api/<SchoolRecordApiController>
        [Route("Add")]
        [HttpPost]
        public IActionResult AddGuardian([FromBody] sr_GuardianRecord value)
        {
            try
            {
                if (recordService.GetGuardianByCode(value.Reg_Number).Result != null)
                {
                    return Ok(new { responseCode = 400, responseDescription = "Already Exist" });
                }
                value.CreatedBy = User.Identity.Name;
                value.CreatedDate = DateTime.Now;
                recordService.AddGuardian(value);

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
        public IActionResult Update(int id, [FromBody] sr_GuardianRecord value)
        {
            try
            {
                var sch = recordService.GetGuardianByid(id).Result;
                if (sch == null)
                {
                    return Ok(new { responseCode = 400, responseDescription = "Not Found" });
                }
                sch.Reg_Number = value.Reg_Number;
                sch.Surname = value.Surname;
                sch.OtherNames = value.OtherNames;
                sch.PhoneNumber = value.PhoneNumber;
                sch.Status = value.Status;
                sch.StatusReason = value.StatusReason;
                sch.StatusDate = value.StatusDate;
                sch.Address = value.Address;
               

                recordService.UpdateGuardian(sch);
                return Ok(new { respnseCode = 200, ResponseDescription = "Successfully Updated" });


            }
            catch (Exception ex)
            {

                return Ok(new { respnseCode = 500, ResponseDescription = ex.Message });
            }
        }

        // DELETE api/<SchoolRecordApiController>/5
        [Route("DeleteRecord/{id:int}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var sch = recordService.GetGuardianByid(id).Result;
            if (sch == null)
            {
                return Ok(new { responseCode = 400, responseDescription = "Not Found" });
            }
            recordService.DeleteGuardian(sch);
            return Ok(new { respnseCode = 200, ResponseDescription = "Successfully Deleted" });
        }
    }
}


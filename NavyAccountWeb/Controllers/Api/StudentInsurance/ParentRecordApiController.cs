﻿using Microsoft.AspNetCore.Mvc;
using NavyAccountCore.Entities;
using NavyAccountWeb.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static NavyAccountWeb.Models.SchoolFilterModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NavyAccountWeb.Controllers.Api.ParentInsurance
{
    [Route("api/ParentRecord")]
    [ApiController]
    public class ParentRecordApiController : ControllerBase
    {
        // GET: api/<ParentRecordApiController>
        private readonly IParentRecordService recordService;
        public ParentRecordApiController(IParentRecordService recordService)
        {
            this.recordService = recordService;
        }
        [Route("getAllParents")]
        [HttpGet]
        public async Task<IActionResult> Get(int? pageno)
        {
            int iDisplayLength = 10;
            pageno = pageno == null ? 0 : (pageno--);
            var _parentlist = await recordService.GetParentList(((int)pageno * iDisplayLength), iDisplayLength);
            var countall = await recordService.getParentListCount();
            return Ok(new { responseCode = 200, parentlist = _parentlist, total = countall });
        }
        [Route("getAllParentByNameLimited/{parentname}")]
        [HttpGet]
        public List<Searchby_Name> getByNameLimited(string parentname)
        {
            List<Searchby_Name> pp = new List<Searchby_Name>();
            var result = recordService.GetParentListByName(parentname).Result;
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
        public async Task<IEnumerable<sr_ParentRecord>> GetAll()
        {
            return await recordService.GetAllParent();
        }
        [Route("GetParentName")]
        [HttpGet]
        public List<Searchby_Name> GetParentName()
        {
            List<Searchby_Name> pp = new List<Searchby_Name>();
            var result = recordService.GetAllParent().Result;
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
        [Route("GetParentById/{id:int}")]
        [HttpGet]
        public IActionResult GetParentById(int id)
        {
            var pers = recordService.GetParentByid(id).Result;
            sr_ParentRecord parent = new sr_ParentRecord();
            parent.id = pers.id;
            parent.Surname = pers.Surname;
            parent.OtherNames = pers.OtherNames;
            parent.Email = pers.Email;
            parent.Address = pers.Address;
            parent.PhoneNumber = pers.PhoneNumber;
            parent.Workclass = pers.Workclass;

            return Ok(new { responseCode = 200, parent });
        }

        // GET api/<SchoolRecordApiController>/5
        [Route("GetRecordbyCode/{code}")]
        [HttpGet("{id}")]
        public IActionResult GetByCode(string code)
        {
            var result = recordService.GetParentByCode(code);
            if (result == null)
                return Ok(new { responseCode = "404", responseDescription = "Dose not Exist" });
            else
                return Ok(new { responseCode = "200", responseDescription = "Successfull", data = result });
        }

        // POST api/<SchoolRecordApiController>
        [Route("Add")]
        [HttpPost]
        public IActionResult AddParent([FromBody] sr_ParentRecord value)
        {
            try
            {
                if (recordService.GetParentByCode(value.Reg_Number).Result != null)
                {
                    return Ok(new { responseCode = 400, responseDescription = "Already Exist" });
                }
                value.CreatedBy = User.Identity.Name;
                value.CreatedDate = DateTime.Now;
                recordService.AddParent(value);

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
        public IActionResult Update(int id, [FromBody] sr_ParentRecord value)
        {
            try
            {
                var sch = recordService.GetParentByid(id).Result;
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
               

                recordService.UpdateParent(sch);
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
            var sch = recordService.GetParentByid(id).Result;
            if (sch == null)
            {
                return Ok(new { responseCode = 400, responseDescription = "Not Found" });
            }
            recordService.DeleteParent(sch);
            return Ok(new { respnseCode = 200, ResponseDescription = "Successfully Deleted" });
        }
    }
}


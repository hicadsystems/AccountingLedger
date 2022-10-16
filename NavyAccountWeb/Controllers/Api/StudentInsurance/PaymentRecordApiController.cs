using Microsoft.AspNetCore.Mvc;
using NavyAccountCore.Entities;
using NavyAccountWeb.IServices;
using NavyAccountWeb.Models;
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

        [Route("FilterPaymentProposal/{proposalValue}")] //api/Payment/FilterPaymentProposal/
        [HttpGet]
        public async Task<IEnumerable<PaymentProposalRecord2>> GetAll(string proposalValue)
        {
            var pp=await recordService.filteredPaymentProposal(proposalValue);
            return pp;
        }


        [Route("GetPaymentProposalByReqNum/{reqNum}")] //api/Payment/GetPaymentProposalByReqNum/
        [HttpGet]
        public async Task<List<PaymentProposalRecord>> GetPaymentProposalByReqNum(string reqNum)
        {
            return await recordService.GetPaymentProposalByReq(reqNum);
        }

        // GET: api/<PaymenRecordApiController>
        [Route("GetAllPaymentProposal")]
        [HttpGet]
        public async Task<IEnumerable<PaymentProposalRecord>> GetAll()
        {
            return await recordService.GetStudentpaymentProposal();
        }

        [Route("GetAllProposalBySchoolName/{schoolName}")]
        [HttpGet]
        public async Task<IEnumerable<PaymentProposalRecord>> Get(string schoolName)
        {
            return await recordService.GetStudentpaymentProposalbySchool(schoolName);
        }

        [Route("GetDescrepancyRecord")]  //api/Payment/GetDescrepancyRecord
        [HttpGet]
        public async Task<IActionResult> Get(int? pageno)
        {
            int iDisplayLength = 10;
            pageno = pageno == null ? 0 : (pageno--);
            var paymentProposalList = await recordService.GetDiscrepancyRecord(((int)pageno * iDisplayLength), iDisplayLength);
            //var countall = await recordService.getStudentListCount();
            return Ok(new { responseCode = 200, paymentProposalList = paymentProposalList.Item1, totalCount = paymentProposalList.Item2 });
           
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

        [Route("DeletePaymentRecord")] 
        [HttpDelete]
        public async  Task<IActionResult> DeletePaymentRecord(string reqnum)
        {
            var op = new DeleteStudentPaymentproposal { Req_Number= reqnum };
            await recordService.DeletePaymentProposal(op);

            return Ok(new { responseCode = 200, ResponseDescription = "Successfully Deleted" });
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


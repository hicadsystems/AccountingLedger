using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NavyAccountCore.Core.Entities;
using NavyAccountWeb.IServices;

namespace NavyAccountWeb.Controllers.Api.ReferenceTable
{
    [Route("api/Beneficiary")]
    [ApiController]
    public class BeneficiaryController : ControllerBase
    {
        private readonly IBeneficiaryService beneficiaryService;
        public BeneficiaryController(IBeneficiaryService beneficiaryService)
        {
            this.beneficiaryService = beneficiaryService;
        }
        // GET: api/Beneficiaries
        [Route("getAllBeneficiaries/{id}")]
        [HttpGet]
        public IEnumerable<Beneficiary> getAllBeneficiaries(int id)
        {
            return beneficiaryService.GetBeneficiaryByPersonID(id).Result;
        }


        // POST: api/Person
        [Route("createBeneficiary")]
        [HttpPost]
        public IActionResult createBeneficiary([FromBody] Beneficiary value)
        {
            try
            {
               
                value.CreatedDate = DateTime.Now;
                beneficiaryService.AddBeneficiary(value);

                return Ok(new { responseCode = 200, responseDescription = "Created Successfully" });
            }
            catch (Exception ex) {
                return Ok(new { responseCode = 500, responseDescription = "Failed" });
            }
        }

       
       
    }
}

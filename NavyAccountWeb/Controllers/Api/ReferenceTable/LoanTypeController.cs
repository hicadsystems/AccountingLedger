using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NavyAccountCore.Core.Entities;
using NavyAccountCore.Entities;
using NavyAccountWeb.IServices;

namespace NavyAccountWeb.Controllers.Api.ReferenceTable
{

    [Route("api/LoanType")]
    [ApiController]
    public class LoanTypeController : ControllerBase
    {
        private readonly ILoanTypeService loantypeService;
        private readonly ILoantypeReviewService loantypereviewService;

        public LoanTypeController(ILoanTypeService loantypeService, ILoantypeReviewService loantypereviewService)
        {
            this.loantypeService = loantypeService;
            this.loantypereviewService = loantypereviewService;
        }

        //api/LoanType/RemoveLoanType
        [Route("RemoveLoanType/{id:int}")]
        [HttpGet]
        public IActionResult Remove(int id)
        {
            var resu = loantypeService.GetLoanTypeById(id);
            if (resu == null) return NotFound();

            loantypeService.RemoveLoanType(resu.Result);

            return Ok(new { responseCode = 200, responseDescription = "Deleted Successful" });
        }
        // GET: api/FundType
        [Route("getAllLoanTypes")]
        [HttpGet]
        public IEnumerable<Pf_loanType> Get()
        {
            return loantypeService.GetLoanTypes();
        }
        [Route("getAllLoanDesc")]
        [HttpGet]
        public IActionResult getloanDesc()
        {
            var lonatype = loantypeService.GetLoanTypeDesc().Result;
            
            return Ok(lonatype);

        }

        // GET: api/FundType
        [Route("getLoanTypeByCode/{id}")]
        [HttpGet]
        public IActionResult GetFundTypeByCode(string id)
        {
            var fund = loantypeService.GetLoanTypeByCode(id);

            if (fund == null)
            {
                return Ok(new { responseCode = 404, responseDescription = "Loan type Code does not Exist" });
            }
            return Ok(new { responseCode = 200, responseDescription = "loan type Code Exist", Data = fund });
        }

        // GET: api/FundType/5
        [Route("Getl")]
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/LoanType
        [Route("createLoanType")]
        [HttpPost]
        public IActionResult createLoanType([FromBody] Pf_loanType value)
        {
            try
            {
                if (String.IsNullOrEmpty(value.Code))
                {
                    return Ok(new { responseCode = 500, responseDescription = "Kindly Supply loan Type Code" });
                }
                if (loantypeService.GetLoanTypeByCode(value.Code.Trim()) != null)
                {
                    return Ok(new { responseCode = 400, responseDescription = "Loan Type Code already Exist" });
                }
                
                value.datecreated = DateTime.Now;
                value.createdby = User.Identity.Name;

                loantypeService.AddLoanType(value);

                return Ok(new { responseCode = 200, responseDescription = "Created Successfully" });
            }
            catch (Exception ex)
            {
                return Ok(new { responseCode = 500, responseDescription = "Failed" });
            }
        }

        // PUT: api/FundType/5
        [Route("updateLoanType")]
        [HttpPut]
        public IActionResult Put([FromBody] Pf_loanType value)
        {
            try
            {
                if (String.IsNullOrEmpty(value.Code))
                {
                    return Ok(new { responseCode = 500, responseDescription = "Kindly Supply Fund Type Code" });
                }
                var getloan = loantypeService.GetLoanTypeByCode(value.Code.Trim());
                getloan.Description = value.Description;
                getloan.Tenure = value.Tenure;
                getloan.FundTypeID = value.FundTypeID;
                getloan.Interest = value.Interest;
                getloan.liabilityacct = value.liabilityacct;
                getloan.interestacct = value.interestacct;
                getloan.incomeacct = value.incomeacct;
                getloan.loanacct = value.loanacct;
                getloan.trusteeacct = value.trusteeacct;
                getloan.datecreated = DateTime.Now;
                getloan.createdby = User.Identity.Name;
                loantypeService.UpdateLoanType(getloan);
                return Ok(new { responseCode = 200, responseDescription = "Updated Successfully" });
            }
            catch (Exception ex)
            {
                return Ok(new { responseCode = 500, responseDescription = "Failed" });
            }
        }
        
        [Route("RemoveLoanTyper/{id:int}")]
        [HttpGet]
        public IActionResult Remover(int id)
        {
            var resu = loantypereviewService.GetLoanTypeById(id);
            if (resu == null) return NotFound();

            loantypereviewService.RemoveLoanType(resu.Result);

            return Ok(new { responseCode = 200, responseDescription = "Deleted Successful" });
        }
        // GET: api/FundType
        [Route("getAllLoanTypesr")]
        [HttpGet]
        public IEnumerable<npf_LoanTypeReview> Getr()
        {
            return loantypereviewService.GetLoanTypes();
        }
        [Route("getAllLoanDescr")]
        [HttpGet]
        public IActionResult getloanDescr()
        {
            var lonatype = loantypereviewService.GetLoanTypeDesc().Result;

            return Ok(lonatype);

        }
        [Route("createLoanTyper")]
        [HttpPost]
        public IActionResult createLoanTyper([FromBody] npf_LoanTypeReview value)
        {
            try
            {
                //if (String.IsNullOrEmpty(value.Code))
                //{
                //    return Ok(new { responseCode = 500, responseDescription = "Kindly Supply loan Type Code" });
                //}
                //if (loantypeService.GetLoanTypeByCode(value.Code.Trim()) != null)
                //{
                //    return Ok(new { responseCode = 400, responseDescription = "Loan Type Code already Exist" });
                //}

                //value.datecreated = DateTime.Now;
                //value.createdby = User.Identity.Name;

                loantypereviewService.AddLoanType(value);

                return Ok(new { responseCode = 200, responseDescription = "Created Successfully" });
            }
            catch (Exception ex)
            {
                return Ok(new { responseCode = 500, responseDescription = "Failed" });
            }
        }

        // PUT: api/FundType/5
        [Route("updateLoanTyper")]
        [HttpPut]
        public IActionResult Put([FromBody] npf_LoanTypeReview value)
        {
            try
            {
                if (value.Id==null)
                {
                    return Ok(new { responseCode = 500, responseDescription = "Kindly Supply Fund Type Code" });
                }
                var getloan = loantypereviewService.GetLoanTypeByCode(value.Id);
                getloan.Id = value.Id;
                getloan.Interestrate = value.Interestrate;
                getloan.ReviewDate = value.ReviewDate;
                getloan.LoanType = value.LoanType;

                loantypereviewService.UpdateLoanType(getloan);
                return Ok(new { responseCode = 200, responseDescription = "Updated Successfully" });
            }
            catch (Exception ex)
            {
                return Ok(new { responseCode = 500, responseDescription = "Failed" });
            }
        }
    }
 }
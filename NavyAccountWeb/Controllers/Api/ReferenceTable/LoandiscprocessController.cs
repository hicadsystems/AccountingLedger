using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Entities;
using NavyAccountCore.Entities;
using NavyAccountWeb.IServices;
using NavyAccountWeb.ViewModels;

namespace NavyAccountWeb.Controllers.Api.ReferenceTable
{
    [Route("api/Loandiscs")]
    [ApiController]
    public class LoandiscprocessController : ControllerBase
    {
        private readonly ILoandiscService loandiscService;
        private readonly ILoanTypeService loantypeService;
        private readonly IFundTypeCodeService fundtypeservice;
        private readonly IUnitOfWork unitOfWork;
        public LoandiscprocessController(IFundTypeCodeService fundtypeservice,ILoandiscService loandiscService, ILoanTypeService loantypeService, IUnitOfWork unitOfWork)
        {
            this.loandiscService = loandiscService;
            this.loantypeService = loantypeService;
            this.unitOfWork = unitOfWork;
            this.fundtypeservice = fundtypeservice;
        }
        [Route("createLoandisc")]
        [HttpPost]
        public IActionResult createLoandisc([FromBody] LoanDiscVM newLoan)
        {
            try
            {

                if(newLoan.PersonID==null)
                {
                    return Ok(new { responseCode = 500, responseDescription = "Kindly Supply An Account" });
                }
                var fundcode= HttpContext.Session.GetString("fundtypecode");
                var getallfund = fundtypeservice.GetFundTypeCodeByCode(fundcode);
                var getallloantype = unitOfWork.loanType.GetLoanTypeByCode(x=>x.Id==newLoan.LoanTypeID);
                var getPersonId = unitOfWork.person.GetPersonBySVC_No(x => x.PersonID == newLoan.PersonID);
                
                newLoan.loanact =getallloantype.loanacct.Substring(0,5)  + getPersonId.SVC_NO;
                newLoan.loantype =Convert.ToString(getallloantype.Id);
                newLoan.incomeact = getallloantype.incomeacct;
                newLoan.trusteeact = getallloantype.trusteeacct;
                newLoan.period =Convert.ToString(getallfund.processingYear+ getallfund.processingMonth);
                newLoan.datecreated = DateTime.Now;
                newLoan.fundcode = HttpContext.Session.GetString("fundtypecode");
                loandiscService.AddLoandisc(newLoan);

                return Ok(new { responseCode = 200, responseDescription = "Created Successfully" });
            }
            catch (Exception ex)
            {
                return Ok(new { responseCode = 500, responseDescription = "Failed" });
            }


        }
    }
}
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NavyAccountCore.Core.Data;
using NavyAccountWeb.IServices;

namespace NavyAccountWeb.Controllers.Api.Transaction
{
    [Route("api/DashBoardWidget")]
    [ApiController]
    public class DashBoardWidgetController : ControllerBase
    {
        
       private readonly ILedgerService ledgerService;
        private readonly IFundTypeCodeService fundTypeService;
        private readonly ILoanRegisterService loanRegisterService;
        private readonly IUnitOfWork unitOfWork;

        public DashBoardWidgetController(IUnitOfWork unitOfWork,ILedgerService ledgerService, IFundTypeCodeService fundTypeService, ILoanRegisterService loanRegisterService)
       {
          this.ledgerService = ledgerService;
            this.fundTypeService = fundTypeService;
            this.loanRegisterService = loanRegisterService;
            this.unitOfWork = unitOfWork;
       }

        [Route("getSurplus_Loss/{fundtypecode}")]
        [HttpGet]
        public IActionResult getSurplusORLoss( string fundtypecode)
        {
            var result = fundTypeService.GetFundTypeCodeByCode(fundtypecode);
            decimal te = 0;
            if (result != null) {
                te = ledgerService.getSurplus_Loss(result.profitacct, fundtypecode);

            }
            
            
            return Ok(new { responseCode = "200", responseDescription = "Successfull", data = te });
        }
        [Route("getSurplus_LossC/{fundtypecode}")]
        [HttpGet]
        public IActionResult getSurplusORLossC(string fundtypecode)
        {
            var result = fundTypeService.GetFundTypeCodeByCode(fundtypecode);

            decimal te = 0;
            if (result != null)
            {
              var ledger = ledgerService.getLedgerInfoCSD(fundtypecode).ToList();
                te = Convert.ToDecimal(ledger.Sum(x => x.opbalance + x.adbbalance - x.crbalance)*-1);

            }


            return Ok(new { responseCode = "200", responseDescription = "Successfull", data = te });
        }


        [Route("getLoanAppCountByStatus/{fundtypeid}")]
        [HttpGet]
        public IActionResult getPendingLoanAppCount(int fundtypeid)
        {
           // var resultt = loanRegisterService.getListofLoanRegisterByStatus(fundtypeid,"8").Result;
            var resul = loanRegisterService.getPendingLoanCount(fundtypeid).Result;
            var d = resul.Count();
          
            return Ok(new { responseCode = "200", responseDescription = "Successfull", data = d });
        }

       
        [Route("getLoanSum/{fundtypeid}")]
        [HttpGet]
        public IActionResult getTotalLoanAmount(int fundtypeid)
        {

            var resultt = loanRegisterService.getListofLoanRegisterByStatus(fundtypeid, "8").Result;
            var loanlist = resultt.ToList();
            decimal loanamount = 0;
            if (loanlist != null)
            {
                loanamount = Convert.ToDecimal(loanlist.Sum(x => x.Amount));
            }
            return Ok(new { responseCode = "200", responseDescription = "Successfull", data = loanamount });
        }
    }
}
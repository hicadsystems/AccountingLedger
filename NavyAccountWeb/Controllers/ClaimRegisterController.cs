using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NavyAccountCore.Core.AuditService;
using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Entities;
using NavyAccountCore.Models;
using NavyAccountWeb.IServices;
using Wkhtmltopdf.NetCore;

namespace NavyAccountWeb.Controllers
{
    [SessionTimeout]
    public class ClaimRegisterController : Controller
    {
        private readonly ILedgerService ledgerService;
        private readonly IClaimRegisterService claimRegisterService;
        private readonly IGeneratePdf generatePdf;
        private readonly IUnitOfWork unitofWork;
        public ClaimRegisterController(ILedgerService ledgerService, IClaimRegisterService claimRegisterService, IGeneratePdf generatePdf, IUnitOfWork unitofWork) {
            this.ledgerService = ledgerService;
            this.claimRegisterService = claimRegisterService;
            this.generatePdf = generatePdf;
            this.unitofWork = unitofWork;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Index2()
        {
            return View();
        }

        public ActionResult Index3()
        {
            return View();
        }


        [Route("ClaimRegister/CreateClaim")]
        public async Task<IActionResult> CreateClaim([FromBody] Npf_ClaimRegister value)
        {
            try
            {
                var getperson = unitofWork.person.GetPersonBySVC_No(x => x.PersonID == value.PersonID);
                var kp = claimRegisterService.GetClaimRegisterByCode2(getperson.SVC_NO.Trim(), value.FundTypeID).Result;
                var getperrank = unitofWork.rank.GetRankbyName(x => x.Id == getperson.rank);
                //  var fn = services.GetClaimRegisterByCode(value.FundTypeID.Trim());
                if (kp != null && kp.status == "Pending")
                {
                    

                        kp.TotalContribution = value.TotalContribution;
                        kp.AmountDue = value.AmountDue;
                        kp.interest = value.amountPaid;
                        kp.statusdate = DateTime.Now;
                        kp.loan = value.amountReceived;
                        kp.Beneficiary = value.Beneficiary;
                        kp.bank = value.bank;
                        kp.acctno = value.acctno;
                        kp.Remark = "Dependant For Late " + getperrank.Description + " " + getperson.LastName + " " + getperson.FirstName + " " + getperson.SVC_NO;
                        kp.status = "Pending";
                        kp.appdate = DateTime.Now;


                        await claimRegisterService.UpdateNpfClaimRegister(kp);
                    
                    //var getclaim2 = claimRegisterService.GetclaimBysvcNo(kp.svcno).ToList();

                    //return await generatePdf.GetPdf("Views/ClaimRegister/ReportPage2.cshtml", getclaim2);

                   return Ok(new { responseCode = 200, responseDescription = "update Successfully" });
                }
                else if (kp == null)
                {
                    var fg = new Npf_ClaimRegister
                    {
                        PersonID = getperson.PersonID,
                        svcno = getperson.SVC_NO,
                        appdate = DateTime.Now,
                        TotalContribution = value.TotalContribution,
                        FundTypeID = value.FundTypeID,
                        statusdate = DateTime.Now,
                        AmountDue = value.AmountDue,
                        interest = value.amountPaid,
                        loan = value.amountReceived,
                        Beneficiary = value.Beneficiary,
                        bank = value.bank,
                        acctno = value.acctno,
                        Remark = "Dependant For Late " + getperrank.Description + " " + getperson.LastName + " " + getperson.FirstName + " " + getperson.SVC_NO,
                        status = "Pending"


                    };
                    await claimRegisterService.AddNpfClaimRegister(fg);

                    return Ok(new { responseCode = 200, responseDescription = "Created Successfully" });
                }
                else
                {
                    return Ok(new
                    {
                        responseCode = 200,
                        responseDescription = "Claim has been approved" +
                        "do you want to do reversal?"
                    });
                }
                           }
            catch (Exception ex)
            {
                return Ok(new { responseCode = 500, responseDescription = "Failed" });
            }  
        }

        [Route("ClaimRegister/finishClaimRequest/{personid}/{fundcode}")]
        public async Task<IActionResult> finishClaimRequestAsync(int personid, string fundcode)
        {
            var getperson = unitofWork.person.GetPersonBySVC_No(x => x.PersonID == personid);
            if (fundcode == "0")
            {
                var getclaim2 = claimRegisterService.GetclaimBysvcNo(getperson.SVC_NO).ToList();

                return await generatePdf.GetPdf("Views/ClaimType/ReportPage2.cshtml", getclaim2);
            }
            else
            {
                var getclaim = claimRegisterService.Getclaim(getperson.SVC_NO, fundcode).ToList();

                return await generatePdf.GetPdf("Views/ClaimType/ReportPage.cshtml", getclaim);
            }
        }

        [Route("ClaimRegister/finishClaimRequest/{dateofclaim}/{svcno}/{fundcode}")]
        public async Task<IActionResult> finishClaimRequestAsync(string dateofclaim, string svcno, string fundcode) 
        {
            DischargeCalcView dischargeCalcView = new DischargeCalcView();


            dischargeCalcView = ledgerService.dischargeCalculation(svcno, fundcode);
            decimal sumloan = 0; decimal sumContr = 0; decimal sumofthetwo = 0;
            foreach (var dloan in dischargeCalcView.personLoans) {
                sumloan += dloan.amount;
            }

            foreach (var dcon in dischargeCalcView.personContributions)
            {
                if (dcon.description == "Total")
                { 
                    sumContr = dcon.amount;
                    break;
                }
                
            }
            dischargeCalcView.loanTotal = sumloan;
            dischargeCalcView.contrTotal = sumContr;
            sumofthetwo = sumloan + sumContr;
            dischargeCalcView.theTwoTotal = sumofthetwo;
            string yeartouse = dateofclaim.Substring(0, 4);
            string monthtouse = dateofclaim.Substring(4, 2);
            string daytouse = dateofclaim.Substring(6, 2);
            string ddate = yeartouse + "/" + monthtouse + "/" + daytouse;
            if (await claimRegisterService.GetClaimRegisterByCode(svcno) != null)
            {
                TempData["message"] = "The Person has exited";
            }
            else
            {
                await claimRegisterService.AddNpfClaimRegister(new Npf_ClaimRegister()
                {
                    svcno = svcno,
                    loan = dischargeCalcView.personLoans == null ? 0 : sumloan,
                    appdate = DateTime.Parse(ddate),
                    AmountDue = sumofthetwo,
                    status = "Pending",
                    statusdate = DateTime.Now,
                    interest = 10,
                    TotalContribution = dischargeCalcView.personContributions == null ? 0 : sumContr,
                });

                return await generatePdf.GetPdf("Views/ClaimRegister/ReportPage.cshtml", dischargeCalcView);
            }
            return RedirectToAction("Index");
        }

       
    }
}
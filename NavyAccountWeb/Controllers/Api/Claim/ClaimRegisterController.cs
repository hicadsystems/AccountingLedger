using Microsoft.AspNetCore.Mvc;
using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Entities;
using NavyAccountCore.Models;
using NavyAccountWeb.IServices;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Wkhtmltopdf.NetCore;
using Microsoft.AspNetCore.Http;
using System.Data;

namespace NavyAccountWeb.Controllers.Api.Claim
{
    [Route("api/ClaimRegister")]
    [ApiController]
    public class ClaimRegisterController:ControllerBase
    {
        private readonly IClaimRegisterService services;
        private readonly IUnitOfWork unitofWork;
        private readonly ILedgerService ledgerService;
        private readonly IGeneratePdf generatePdf;
        private readonly string _connectionstring;
        public ClaimRegisterController(IUnitOfWork unitofWork,IClaimRegisterService services, ILedgerService ledgerService, IConfiguration configuration,IGeneratePdf generatePdf)
        {
            this.services = services;
            this.ledgerService = ledgerService;
            this.unitofWork=unitofWork;
            this.generatePdf = generatePdf;
            _connectionstring = configuration.GetConnectionString("DefaultConnection");
        }

        //api/ClaimRegister/CreateClaim
        [Route("CreateClaim")]
        [HttpPut]
        public async Task<IActionResult> CreateClaim([FromBody] Npf_ClaimRegister value)
        {
            try
            {
                var getperson = unitofWork.person.GetPersonBySVC_No(x => x.PersonID == value.PersonID);
                var kp = services.GetClaimRegisterByCode2(getperson.SVC_NO.Trim(), value.FundTypeID).Result;
                //  var fn = services.GetClaimRegisterByCode(value.FundTypeID.Trim());
                if (kp != null && kp.status == "Pending")
                {

                    kp.TotalContribution = value.TotalContribution;
                    kp.AmountDue = value.AmountDue;
                    kp.interest = value.amountPaid;
                    kp.statusdate = DateTime.Now;
                    kp.loan = value.amountReceived;
                    kp.Beneficiary = value.Beneficiary;
                    kp.status = "Pending";
                    kp.appdate = DateTime.Now;
                    

                    await services.UpdateNpfClaimRegister(kp);

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
                        status = "Pending"


                    };
                    await services.AddNpfClaimRegister(fg);

                    var getclaim2 = services.GetclaimBysvcNo(fg.svcno);

                    return await generatePdf.GetPdf("Views/ClaimType/ReportPage2.cshtml", getclaim2);
                    // return Ok(new { responseCode = 200, responseDescription = "Created Successfully" });
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

        //api/ClaimRegister/GetClaimNotApproved
        [Route("GetClaimNotApproved")]
        [HttpGet]
        public IEnumerable<ClaimModel> GetClaimNotApproved()
        {
            return services.GetClaimRegisterNotApproved();
        }

        [Route("GetClaimApproved")]
        [HttpGet]
        public IEnumerable<ClaimModel> GetClaimApproved()
        {
            return services.GetClaimRegisterApproved();
        }
        //api/ClaimRegister/GetClaimNotApprovedBySvc
        [Route("GetClaimApprovedBySvc/{PersonID}")]
        [HttpGet]
        public IEnumerable<ClaimModel> GetClaimApprovedBySvc(int PersonID)
        {
            return services.GetClaimRegisterApprovedList(PersonID);
        }
        [Route("GetClaimNotApprovedBySvc/{PersonID}")]
        [HttpGet]
        public IEnumerable<ClaimModel> GetClaimNotApprovedBySvc(int PersonID)
        {
            return services.GetClaimRegisterNotApprovedList(PersonID);
        }

        //api/ClaimRegister/UpdateApproval
        [Route("UpdateApproval")]
        [HttpPost]
        public async Task<IActionResult> UpdateApproval([FromBody] ClaimModel value)
        {

            using (SqlConnection sqls = new SqlConnection(_connectionstring))
            {
                using (SqlCommand cmd = new SqlCommand("npf_update_claim", sqls))
                {
                    cmd.CommandTimeout = 1200;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@globaluser", User.Identity.Name));
                    cmd.Parameters.Add(new SqlParameter("@claimtype", value.FundTypeID));
                    cmd.Parameters.Add(new SqlParameter("@fundtype", HttpContext.Session.GetInt32("fundtypeid")));
                    cmd.Parameters.Add(new SqlParameter("@bankcode", value.incomeacct));
                    cmd.Parameters.Add(new SqlParameter("@persid", value.PersonID));
                    cmd.Parameters.Add("@error", SqlDbType.Char, 500);
                    cmd.Parameters["@error"].Direction = ParameterDirection.Output;


                    await sqls.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                   string message = (string)cmd.Parameters["@error"].Value;

                    return Ok(new { responseCode = 200, responseDescription = message });
                }

            }
        }

        [Route("UpdateReversal")]
        [HttpPost]
        public async Task<IActionResult> UpdateReversal([FromBody] ClaimModel value)
        {

            using (SqlConnection sqls = new SqlConnection(_connectionstring))
            {
                using (SqlCommand cmd = new SqlCommand("npf_update_claimReversal", sqls))
                {
                    cmd.CommandTimeout = 1200;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@globaluser", User.Identity.Name));
                    cmd.Parameters.Add(new SqlParameter("@claimtype", value.FundTypeID));
                    cmd.Parameters.Add(new SqlParameter("@fundtype", HttpContext.Session.GetInt32("fundtypeid")));
                    cmd.Parameters.Add(new SqlParameter("@persid", value.PersonID));


                    await sqls.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();

                    return Ok(new { responseCode = 200, responseDescription = "Successfully Reverse" });
                }

            }
        }

        //[Route("RecalculateClaim")]
        //[HttpPost]
        //public ActionResult RecalculateClaim([FromBody] Npf_ClaimRegister value)
        //{
        //    try
        //    {
        //        var getperson = unitofWork.person.GetPersonBySVC_No(x => x.PersonID == value.PersonID);
        //        var kp = services.GetClaimRegisterByCode(getperson.SVC_NO.Trim()).Result;
        //        var fn = services.GetClaimRegisterByCode(value.FundTypeID.Trim());

        //        if (kp != null && kp.FundTypeID==value.FundTypeID)
        //        {
        //            var fg = new Npf_ClaimRegister
        //            {
        //                PersonID = getperson.PersonID,
        //                svcno = getperson.SVC_NO,
        //                TotalContribution=value.TotalContribution,
        //                appdate = DateTime.Now,
        //                FundTypeID = value.FundTypeID,
        //                AmountDue = value.AmountDue,
        //                amountPaid = value.amountPaid,
        //                amountReceived = value.amountReceived,
        //                Beneficiary = value.Beneficiary,
        //                status = "Pending"
        //            };

        //            services.UpdateNpfClaimRegister(fg);

        //            return Ok(new { responseCode = 200, responseDescription = "update Successfully" });
        //        }
        //        else
        //        {
        //            return Ok(new { responseCode = 200, responseDescription = "you have not done claim processing" });
        //        }  
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(new { responseCode = 500, responseDescription = "Failed" });
        //    }
        //}



        [Route("LoadAllClaim")]
        [HttpGet]
        public IEnumerable<Npf_ClaimRegister> Get()
        {
            return services.GetClaimRegister();
        }


        // api/ClaimRegister/updateClaimRegister
        [Route("updateClaimRegister")]
        [HttpPut]
        public ActionResult Put([FromBody] Npf_ClaimRegister value)
        {
            try
            {
                if (String.IsNullOrEmpty(value.Id.ToString()))
                {
                    return Ok(new { responseCode = 500, responseDescription = "Kindly Supply claim register" });
                }
                var getbal = services.GetClaimRegisterById(value.Id).Result;

                getbal.appdate = value.appdate;
                services.UpdateNpfClaimRegister(getbal);

                return Ok(new { responseCode = 200, responseDescription = "Updated Successfully" });
            }
            catch (Exception ex)
            {
                return Ok(new { responseCode = 500, responseDescription = "Failed" });
            }
        }

        [Route("getDischargeCalculation/{svcno}/{fundcode}")]
        [HttpGet]
        public IActionResult getDischargeCalculation(string svcno, string fundcode)
        {
            try
            {
                return Ok(new { responseCode = "200", responseDescription = "Record Retrieved Successfully", data = ledgerService.dischargeCalculation(svcno, fundcode) });
            }
            catch (Exception ex) {
                return Ok(new { responseCode = "500", responseDescription = "Record Retrieval Failed" });
            }
        }
    }
}

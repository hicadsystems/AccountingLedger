using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Entities;
using NavyAccountCore.Models;
using NavyAccountWeb.IServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NavyAccountWeb.Controllers.Api.Transaction
{
    [Route("api/LoanRegister")]
    [ApiController]
    public class LoanRegisterController : ControllerBase
    {
        private readonly ILoanRegisterService loanRegisterService;
        private readonly IUnitOfWork unitOfWork;
        private readonly ILedgerService ledgerService;
        private readonly string _connectionstring;
        public LoanRegisterController(ILedgerService ledgerService,ILoanRegisterService loanRegisterService, IUnitOfWork unitOfWork, IConfiguration configuration) {
            this.loanRegisterService = loanRegisterService;
            this.ledgerService = ledgerService;
            this.unitOfWork = unitOfWork;
            _connectionstring = configuration.GetConnectionString("DefaultConnection");
        }
        // GET: api/<controller>
        [Route("getAllLoanRegisterees/{id}")]
        [HttpGet]
        public IEnumerable<LoanRegisterViewModel> Get(int id)
        {
            return loanRegisterService.getListofLoanRegister(id).Result;

        }
        [Route("getStoreProcedure")]
        [HttpGet]
        public IEnumerable<LoanRegisterViewModel> GetStoreProcedure(int id)
        {
           
            return loanRegisterService.getListofLoanRegister(id).Result;
        }

        [Route("getAllLoanRegisterByStatus/{id}/{statusid}")]
        [HttpGet]
        public IEnumerable<LoanRegisterViewModel> getAllLoanRegisterByStatus(int id, string statusid)
        {
            return loanRegisterService.getListofLoanRegisterByStatus(id, statusid).Result;
        }
        [Route("getloanbyBatch2")]
        [HttpGet]
        public async Task<List<Pf_LoanRegister>> getbatchdrp()
        {
            return await loanRegisterService.getListofLoanRegisterBatchDrp();
        }
        [Route("getloanbyBatch")]
        [HttpGet]
        public IEnumerable<LoanRegisterViewModel> PutBatchApprovedd()
        {
            return loanRegisterService.getListofLoanRegister();

        }
        [Route("getbatchtotal/{batchNo}")]
        [HttpGet]
        public IActionResult sumofbatch(string batchNo)
        {
            var getloan = loanRegisterService.getListofLoanRegisterByBatch(batchNo).Result;
            //var getloanbatch = getloan.Where(x => x.batchNo == batchNo).ToList();
            decimal sumbatch = 0;
            if (getloan != null)
            {
                sumbatch = Convert.ToDecimal(getloan.Sum(x => x.Amount));
            }
            return Ok(new { responseCode = "200", responseDescription = "Successfull", data = sumbatch });


        }
        [Route("getbanktotal/{acctcode}")]
        [HttpGet]
        public IActionResult sumofbank(string acctcode)
        {
          
            decimal sumbank = 0;
            sumbank = ledgerService.getsumbybank(acctcode);
            return Ok(new { responseCode = "200", responseDescription = "Successfull", data = sumbank });


        }
        [Route("ApproveLoanInBatch/{batchNo}/{acctcode}")]
        [HttpPut]
        public async Task<IActionResult> PutApproveLoanbyBatch(string batchNo,string acctcode)
        {
            var getloanregister = unitOfWork.loanRegisterRepository.GetloanregisterByCode(x => x.batchNo == batchNo);
            var getPersonId = unitOfWork.person.GetPersonBySVC_No(x => x.PersonID == getloanregister.PersonID);
            var getLoanType = unitOfWork.loanType.GetLoanTypeByCode(x => x.Id == getloanregister.LoanTypeID);
            using (SqlConnection sqls = new SqlConnection(_connectionstring))
            {
                using (SqlCommand cmd = new SqlCommand("npf_Batch_approval_loan", sqls))
                {
                    cmd.CommandTimeout = 1200;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@globaluser", User.Identity.Name));
                    cmd.Parameters.Add(new SqlParameter("@fundcode", HttpContext.Session.GetString("fundtypecode")));
                    cmd.Parameters.Add(new SqlParameter("@chqno", batchNo));
                    cmd.Parameters.Add(new SqlParameter("@batchno", batchNo));
                    cmd.Parameters.Add(new SqlParameter("@bankcode", acctcode));


                    await sqls.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();

                    return Ok(new { responseCode = 200, responseDescription = "Updated Successfully" });
                }
               
            }
            //return Ok(new { responseCode = 200, responseDescription = "Updated Successfully" });
        }
        [Route("getAllLoanRegisterByRangeStatus/{id}/{statusid}")]
        [HttpGet]
        public IEnumerable<LoanRegisterViewModel> getListofLoanRegisterBTNByStatus(int id, string statusid)
        {
            return loanRegisterService.getListofLoanRegisterBTNByStatus(id, statusid).Result;
        }
        [Route("getAllLoanRegisterByRangeStatusP/{id}/{statusid}")]
        [HttpGet]
        public IEnumerable<LoanRegisterViewModel> getListofLoanRegisterBTNByStatusP(int id, string statusid)
        {
            return loanRegisterService.getListofLoanRegisterBTNByStatusP(id, statusid).Result;
        }

        [Route("getLoanListPerPerson/{personid}/{fundTypeid}")]
        [HttpGet]
        public IEnumerable<LoanRegisterViewModel> getLoanListPerPerson(int personid, int fundTypeid)
        {
            return loanRegisterService.getLoanListPerPerson(personid, fundTypeid).Result;
        }
        [Route("getAllBatch")]
        [HttpGet]
        public IEnumerable<Pf_LoanRegister> getLoanListbybatch()
        {
            return loanRegisterService.GetLoanRegisterByApplication().Result;
        }

        [Route("createLoanRegister")]
        [HttpPost]
        public IActionResult createLoanRegister([FromBody] Pf_LoanRegister value)
        {
            try
            {

                
                if (value.PersonID == null)
                {
                    return Ok(new { responseCode = 500, responseDescription = "Kindly Supply Person Service Number" });
                }
                var getPersonId = unitOfWork.person.GetPersonBySVC_No(x => x.PersonID == value.PersonID);
                int databasetenure = 0;

                //02/29/2020
                var getLoanType = unitOfWork.loanType.GetLoanTypeByCode(x => x.Id == value.LoanTypeID);
                var loacct = getLoanType.loanacct.Substring(0,5) + getPersonId.SVC_NO;
                var getledger = unitOfWork.npf_Ledgers.Getledger(x => x.acctcode == loacct);
                //var loacct = getLoanType.loanacct.Substring(0,4)+"-" + getPersonId.SVC_NO;
            
                if(getledger!=null)
                {
                    getledger.opbalance = 0; getledger.adbbalance = 0; getledger.crbalance = 0;
                      
                var checkloan = getledger.opbalance + getledger.adbbalance - getledger.crbalance;
                
                if (checkloan > 0)
                {
                    return Ok(new { responseCode = 500, responseDescription = "You have" + ""+checkloan+ " "+" Outstanding Balance on Previous Loan" });
                }
                }
                string remarks = "";
                //value.Status = ;
                value.LoanAppNo = getPersonId.SVC_NO + getLoanType.Code + DateTime.Now.Year+DateTime.Now.Month;
                value.batchNo = value.batchNo + "_" + getLoanType.Description;
                databasetenure = Int32.Parse(getLoanType.Tenure);

                if (databasetenure != Int32.Parse(value.Tenure))
                {
                   remarks += "|Incorrect Loan Tenure Supplied" + "_" + Int32.Parse(value.Tenure);
                }
                

                //check eligibility
                decimal amountYOUcantake = unitOfWork.loanRegisterRepository.loanAmountPerRankBy2(getPersonId.rank, getLoanType.Code).Result;

                if (value.Amount > amountYOUcantake)
                {
                    remarks += "|You can not take more than" + "_" + amountYOUcantake;
                }
                value.remarks += remarks;

                if (remarks != "") {

                    value.Status = "2";
                }

                unitOfWork.loanRegisterRepository.Create(value);
                unitOfWork.Done().Wait();

            return Ok(new { responseCode = 200, responseDescription = "Created Successfully" });
            }
            catch (Exception ex)
            {
                return Ok(new { responseCode = 500, responseDescription = "Failed" });
            }
        }

        [Route("updateLoanRegister")]
        [HttpPut]
        public IActionResult Put([FromBody] Pf_LoanRegister value)
        {
            try
            {
                if (value.Id <= 0)
                {
                    return Ok(new { responseCode = 500, responseDescription = "Invalid Loan Record" });
                }
                var getLoanReg = loanRegisterService.GetLoanRegisterById(value.Id).Result;
                //if (value.Status != "8")-----kenneth
                //{
                getLoanReg.ApproveDate = value.ApproveDate;
                getLoanReg.BankID = value.BankID;
                getLoanReg.StatusAndStatusDate = value.StatusAndStatusDate;
                getLoanReg.remarks = value.remarks;
                getLoanReg.Status = value.Status;
                loanRegisterService.UpdateLoanRegister(getLoanReg);
               
                
                return Ok(new { responseCode = 200, responseDescription = "Updated Successfully" });
            }
            catch (Exception ex)
            {
                return Ok(new { responseCode = 500, responseDescription = "Failed" });
            }
        }

        [Route("updateLoanAppRegister")]
        [HttpPut]
        public async System.Threading.Tasks.Task<IActionResult> PutLoanAppAsync([FromBody] Pf_LoanRegister value)
        {
            try
            {
                if (value.Id <= 0)
                {
                    return Ok(new { responseCode = 500, responseDescription = "Invalid Loan Record" });
                }
                var getLoanReg = loanRegisterService.GetLoanRegisterById(value.Id).Result;

                var getPersonId = unitOfWork.person.GetPersonBySVC_No(x => x.PersonID == value.PersonID);
                int databasetenure = 0;
                var getLoanType = unitOfWork.loanType.GetLoanTypeByCode(x => x.Id == value.LoanTypeID);

                string remarks = "";
                value.Status = "1";

                databasetenure = Int32.Parse(getLoanType.Tenure);

                if (databasetenure != Int32.Parse(value.Tenure))
                {
                    remarks += "|Incorrect Loan Tenure Supplied" + "_" + Int32.Parse(value.Tenure);
                }

                decimal amountYOUcantake = 0;
                //check eligibility
                amountYOUcantake = await loanRegisterService.loanAmountPerRankBy2(getPersonId.rank, getLoanType.Code, value.FundTypeID.Value);

                if (value.Amount > amountYOUcantake)
                {
                    remarks += "|You can not take more than" + "_" + amountYOUcantake;
                }
                value.remarks += remarks;

                if (remarks != "")
                {

                    value.Status = "2";
                }
                getLoanReg.Status = value.Status;
                getLoanReg.Tenure = value.Tenure;
                getLoanReg.Amount = value.Amount;
                getLoanReg.LoanTypeID = value.LoanTypeID;
                getLoanReg.remarks = value.remarks;
                getLoanReg.StatusAndStatusDate = value.StatusAndStatusDate;
                getLoanReg.StatusAndStatusDate = value.remarks;
                getLoanReg.batchNo = value.batchNo;
                getLoanReg.loancount = value.loancount;
                await loanRegisterService.UpdateLoanRegister(getLoanReg);
                return Ok(new { responseCode = 200, responseDescription = "Updated Successfully" });
            }
            catch (Exception ex)
            {
                return Ok(new { responseCode = 500, responseDescription = "Failed" });
            }
        }

        [Route("ApproveLoanRegister")]
        [HttpPut]
        public async Task<IActionResult> PutApproveLoanSPAsync([FromBody] Pf_LoanRegister val)
        {
            var getPersonId = unitOfWork.person.GetPersonBySVC_No(x => x.PersonID == val.PersonID);
            var getLoanType = unitOfWork.loanType.GetLoanTypeByCode(x => x.Id == val.LoanTypeID);
            using (SqlConnection sqls = new SqlConnection(_connectionstring))
            {
                using (SqlCommand cmd = new SqlCommand("npf_approve_loan", sqls))
                {
                    cmd.CommandTimeout = 1200;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@globaluser", User.Identity.Name));
                    cmd.Parameters.Add(new SqlParameter("@fundcode", HttpContext.Session.GetString("fundtypecode")));
                    cmd.Parameters.Add(new SqlParameter("@applno", val.LoanAppNo));
                    cmd.Parameters.Add(new SqlParameter("@chqno", val.ChequeNo));
                    cmd.Parameters.Add(new SqlParameter("@bankcode", val.BankID));


                    await sqls.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();

                    return Ok(new { responseCode = 200, responseDescription = "Updated Successfully" });
                }
            }

        }
        [Route("BatchApproveLoanRegister")]
        [HttpPut]
        public async Task<IActionResult> PutBatchApproveLoanSPAsync([FromBody] Pf_LoanRegister val)
        {
            var getPersonId = unitOfWork.person.GetPersonBySVC_No(x => x.PersonID == val.PersonID);
            var getLoanType = unitOfWork.loanType.GetLoanTypeByCode(x => x.Id == val.LoanTypeID);
            using (SqlConnection sqls = new SqlConnection(_connectionstring))
            {
                using (SqlCommand cmd = new SqlCommand("npf_Batch_approval_loan", sqls))
                {
                    cmd.CommandTimeout = 1200;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@globaluser", User.Identity.Name));
                    cmd.Parameters.Add(new SqlParameter("@fundcode", HttpContext.Session.GetString("fundtypecode")));
                    cmd.Parameters.Add(new SqlParameter("@batchno", val.LoanAppNo));
                    cmd.Parameters.Add(new SqlParameter("@chqno", val.ChequeNo));
                    cmd.Parameters.Add(new SqlParameter("@bankcode", val.BankID));


                    await sqls.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();

                    return Ok(new { responseCode = 200, responseDescription = "Updated Successfully" });
                }
            }

        }
        [Route("updateLoanRegisterbybatch")]
        [HttpPut]
        public async Task<IActionResult> updateLoanRegisterbybatch([FromBody] Pf_LoanRegister val)
        {
            var getPersonId = unitOfWork.person.GetPersonBySVC_No(x => x.PersonID == val.PersonID);
            var getLoanType = unitOfWork.loanType.GetLoanTypeByCode(x => x.Id == val.LoanTypeID);
            using (SqlConnection sqls = new SqlConnection(_connectionstring))
            {
                using (SqlCommand cmd = new SqlCommand("npf_Batch_update_loan", sqls))
                {
                    cmd.CommandTimeout = 1200;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@globaluser", User.Identity.Name));
                    cmd.Parameters.Add(new SqlParameter("@fundcode", HttpContext.Session.GetString("fundtypecode")));
                    cmd.Parameters.Add(new SqlParameter("@batchno", val.batchNo));
                    cmd.Parameters.Add(new SqlParameter("@status", val.Status));
                    cmd.Parameters.Add("@message", SqlDbType.Char, 500);
                    cmd.Parameters["@message"].Direction=ParameterDirection.Output;

                    await sqls.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();


                    return Ok(new { responseCode = 200, responseDescription = "Updated Successfully" });
                }
            }

        }
        [Route("getLoanAppCountByStatus2/{fundtypeid}")]
        [HttpGet]
        public IActionResult getPendingLoanAppCount2(int fundtypeid)
        {

            var resultt = loanRegisterService.getListofLoanRegisterByStatus(fundtypeid, "8").Result;


            return Ok(new { responseCode = "200", responseDescription = "Successfull", data = resultt.Count() });
        }

        [Route("UpdateLoanReversal")]
        [HttpPut]
        public async Task<IActionResult> UpdateLoanReversal([FromBody] Pf_LoanRegister val)
        {
            var getPersonId = unitOfWork.person.GetPersonBySVC_No(x => x.PersonID == val.PersonID);
            var getLoanType = unitOfWork.loanType.GetLoanTypeByCode(x => x.Id == val.LoanTypeID);

            string fund = HttpContext.Session.GetString("fundtypecode");
            string loan = getLoanType.loanacct.Substring(0, 4);
            using (SqlConnection sqls = new SqlConnection(_connectionstring))
            {
                using (SqlCommand cmd = new SqlCommand("npf_update_loanReversal", sqls))
                {
                    cmd.CommandTimeout = 1200;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@globaluser", User.Identity.Name));
                    cmd.Parameters.Add(new SqlParameter("@fundtype", fund));
                    cmd.Parameters.Add(new SqlParameter("@batchno", val.batchNo));
                    cmd.Parameters.Add(new SqlParameter("@loantype", getLoanType.loanacct.Substring(0,4)));
                    cmd.Parameters.Add(new SqlParameter("@persid",val.PersonID));
                    cmd.Parameters.Add(new SqlParameter("@periodH", val.LoanAppNo));
                    cmd.Parameters.Add(new SqlParameter("@docdate", val.ApproveDate));
                    cmd.Parameters.Add("@error", SqlDbType.Char, 500);
                    cmd.Parameters["@error"].Direction = ParameterDirection.Output;

                    await sqls.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    string message = (string)cmd.Parameters["@error"].Value;
                    return Ok(new { responseCode = 200, responseDescription = message });
                }
            }

        }

        [Route("UpdateLoanStatus")]
        [HttpPut]
        public async Task<IActionResult> UpdateLoanStatus([FromBody] LoanStatusViewModel val)
        {
            var getPersonId = unitOfWork.person.GetPersonBySVC_No(x => x.PersonID == val.personid);

            using (SqlConnection sqls = new SqlConnection(_connectionstring))
            {
                using (SqlCommand cmd = new SqlCommand("npf_update_loanStatus", sqls))
                {
                    cmd.CommandTimeout = 1200;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@globaluser", User.Identity.Name));
                    cmd.Parameters.Add(new SqlParameter("@batchno", val.batchno));
                    cmd.Parameters.Add(new SqlParameter("@persid", val.personid));
                    cmd.Parameters.Add(new SqlParameter("@status", val.description));

                    await sqls.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    
                    return Ok(new { responseCode = 200, responseDescription = "Successfully Updated" });
                }
            }

        }
        [Route("getloanStatus")]
        [HttpGet]
        public async Task<List<npf_loanstatus>> getloanstatus()
        {
            return await loanRegisterService.getListofLoanStatus();

        }

        [Route("getloanperiod/{personID}/{loantypeID}/{batchNo}")]
        [HttpGet]
        public async Task<List<npf_history>> getloanperiod(int personID,int loantypeID,string batchNo)
        {
            var getPersonId = unitOfWork.person.GetPersonBySVC_No(x => x.PersonID == personID);
            var loantype = unitOfWork.loanType.GetLoanTypeByCode(x => x.Id == loantypeID);
            string loanacct = loantype.loanacct.Substring(0, 5)+ getPersonId.SVC_NO;

            return await loanRegisterService.getListofHistoryPeriod(loanacct, batchNo);

        }
        [Route("getloanperiod2/{personID}/{loantypeID}/{period}")]
        [HttpGet]
        public async Task<IActionResult> getloanperiod2(int personID, int loantypeID,string period)
        {
            var getPersonId = unitOfWork.person.GetPersonBySVC_No(x => x.PersonID == personID);
            var loantype = unitOfWork.loanType.GetLoanTypeByCode(x => x.Id == loantypeID);
            string loanacct = loantype.loanacct.Substring(0, 5) + getPersonId.SVC_NO;

            var d= await loanRegisterService.getListofHistoryPeriod2(loanacct,period);
            decimal cramt = d.FirstOrDefault().cramt;

            return Ok(new { responseCode = 200, amount = cramt });


        }

    }
}
 
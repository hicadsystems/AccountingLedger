using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Entities;
using NavyAccountCore.Models;
using NavyAccountWeb.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.Services
{
    public class ProcesUpload
    {
        private readonly List<LoanCapture> loanCaptures;
        //private readonly IPersonService personService;
        //private readonly ILoanTypeService loanTypeService;
        //private readonly ILoanRegisterService loanRegisterService;
        private readonly IUnitOfWork unitOfWork;
        private int fundid;
        private string fundcode;
        private string user;
        private string batch;
        public ProcesUpload(List<LoanCapture> loanCaptures, IUnitOfWork unitOfWork, int fundid, string fundcode, string user,string batch)
        {

            this.loanCaptures = loanCaptures;
            this.fundid = fundid;
            this.fundcode = fundcode;
            this.user = user;
            this.unitOfWork = unitOfWork;
            this.batch = batch;
        }

        public async Task processUploadInThread()
        {

            foreach (var s in loanCaptures)
            {
                var getPersonId = unitOfWork.person.GetPersonBySVC_No(x => x.SVC_NO == s.SVC_NO);
                if (getPersonId == null)
                {
                    var getrank = unitOfWork.rank.GetRankbyName(x => x.Description == s.RANK);
                    unitOfWork.person.Create(new Person()
                    {
                        rank = getrank.Id,
                        SVC_NO = s.SVC_NO,
                        LastName = s.SURNAME,
                        FirstName = s.OTHERNAME,
                        CreatedBy = user,
                        CreatedDate = System.DateTime.Now,
                    });
                   await unitOfWork.Done();
                }
            }
            List<Pf_LoanRegister> list_loanregister = new List<Pf_LoanRegister>();
            //fetch Persons ID
            foreach (var s in loanCaptures)
            {
                //string me = s.SVC_NO.ToString();
                var getPersonId = unitOfWork.person.GetPersonBySVC_No(x => x.SVC_NO == s.SVC_NO);
                int personidnotavailable = -1; int databasetenure = 0;
                var getLoanType = unitOfWork.loanType.GetLoanTypeByCode(x => x.Description == s.LOAN_TYPE);

                var getstatus = unitOfWork.loanStatus.GetLoanStatusByCode(x => x.Description == s.STATUS);

                //var getbank = unitOfWork.accountChart.GetChartofAccountByCode(x => x.description == s.BANK);
                //var getloanregister = unitOfWork.loanRegisterRepository.GetloanregisterByCode(x => x.PersonID == getPersonId.PersonID);
               // var getloanregister2 = unitOfWork.loanRegisterRepository.GetloanregisterByCode(x => x.LoanTypeID == getLoanType.Id);
                //if (getloanregister != null)
                //{
                    //if (getloanregister.BankID == null)
                    //{
                       // getloanregister.BankID = getbank.Result.acctcode;
                        //getloanregister.ApproveDate = DateTime.Now;
                        //getloanregister.batchNo = s.BATCH_NO;
                        //getloanregister.Status = getstatus.Result.Id.ToString();
                        //getloanregister.LoanTypeID = getLoanType.Id;
                       
                        //unitOfWork.loanRegisterRepository.Update(getloanregister);
                        //unitOfWork.Done().Wait();
                   // }
             //   }
               // else
               // {
                    string remarks = "";
                    remarks += getPersonId == null ? "Invalid Service Number" + "_" + s.SVC_NO : "";
                    remarks += getLoanType == null ? "|Invalid Loan Type" + "_" + s.LOAN_TYPE : "";
                    string statustosubmit = "1";
                    if (getLoanType != null)
                    {

                        databasetenure = Int32.Parse(getLoanType.Tenure);

                        if (databasetenure != s.TENURE)
                        {
                            remarks += "|Incorrect Loan Tenure Supplied" + "_" + s.TENURE;
                        }
                    }

                    //check eligibility
                    decimal amountYOUcantake = unitOfWork.loanRegisterRepository.loanAmountPerRankBy(getPersonId.rank, getLoanType.Code).Result;

                    if (s.AMOUNT > amountYOUcantake)
                    {
                        remarks += "|You can not take more than" + "_" + amountYOUcantake;
                    }
                    statustosubmit = remarks == "" ? "1" : "2";



                    unitOfWork.loanRegisterRepository.Create(new Pf_LoanRegister()
                    {
                        Amount = s.AMOUNT,
                        PersonID = getPersonId == null ? personidnotavailable : getPersonId.PersonID,
                        FundTypeID = fundid,
                        LoanTypeID = getLoanType == null ? -1 : getLoanType.Id,
                        loanAccount = getLoanType.loanacct,
                        Tenure = databasetenure != s.TENURE ? "-1" : "" + s.TENURE,
                        remarks = null,
                        batchNo=s.BATCH_NO+'_'+getLoanType.Description,
                        Status = statustosubmit,
                        createdby = user,
                        StatusAndStatusDate = ":" + remarks + ": Loan Application on: " + System.DateTime.Now,
                        VoucherNo = null,
                        datecreated = System.DateTime.Now,
                        BankID = null,
                        //ApproveDate = null,
                        ChequeNo = null,
                        //EffectiveDate = null,
                        ExpDate = null,
                        loancount = (string.IsNullOrWhiteSpace(s.MONTHS_PAID)) ? 0 : Convert.ToInt32(s.MONTHS_PAID),
                        LoanAppNo = s.BATCH_NO + "-" + s.SVC_NO 
                    });
                    await unitOfWork.Done();
               // }

            }
        }
        public async Task processUploadInThreadB()
        {

           
            //fetch Persons ID
            foreach (var s in loanCaptures)
            {
 
                var getPersonId = unitOfWork.person.GetPersonBySVC_No(x => x.SVC_NO == s.SVC_NO);
                var getLoanType = unitOfWork.loanType.GetLoanTypeByCode(x => x.Description == s.LOAN_TYPE);

                var getstatus = unitOfWork.loanStatus.GetLoanStatusByCode(x => x.Description == s.STATUS);

                var getbank = unitOfWork.accountChart.GetChartofAccountByCode(x => x.description == s.BANK);
                var getloanregister = unitOfWork.loanRegisterRepository.GetloanregisterByCode(x => x.PersonID == getPersonId.PersonID);
                if (getloanregister != null && getloanregister.batchNo==batch)
                {

                    getloanregister.ApproveDate = DateTime.Now;
                    //getloanregister.batchNo = s.BATCH_NO;
                    getloanregister.Status = getstatus.Result.Id.ToString();
                    //getloanregister.LoanTypeID = getLoanType.Id;
            

                    unitOfWork.loanRegisterRepository.Update(getloanregister);
                  
                    await unitOfWork.Done();
                    // }
                }
               

            }
        }
        public async Task processUploadInThread2()
        {

            foreach (var s in loanCaptures)
            {
                var getPersonId = unitOfWork.person.GetPersonBySVC_No(x => x.SVC_NO == s.SVC_NO);
                if (getPersonId == null)
                {
                    var getrank = unitOfWork.rank.GetRankbyName(x => x.Description == s.RANK);
                    unitOfWork.person.Create(new Person()
                    {
                        rank = getrank.Id,
                        SVC_NO = s.SVC_NO,
                        LastName = s.SURNAME,
                        FirstName = s.OTHERNAME,
                        CreatedBy = user,
                        CreatedDate = System.DateTime.Now,
                    });
                 await  unitOfWork.Done();
                }
            }
            foreach (var s in loanCaptures)
            {
                var getLoanType = unitOfWork.loanType.GetLoanTypeByCode(x => x.Description == s.LOAN_TYPE);
                var getchart = unitOfWork.accountChart.GetChartofAccountByCode(x => x.acctcode == getLoanType.loanacct.Substring(0, 5) + s.SVC_NO);
                if (getchart.Result == null)
                {
                    var getPersonId = unitOfWork.person.GetPersonBySVC_No(x => x.SVC_NO == s.SVC_NO);
                    unitOfWork.accountChart.Create(new npf_chart()
                    {
                        acctcode = getLoanType.loanacct.Substring(0, 5) + s.SVC_NO,
                        description =getPersonId.LastName +" "+getPersonId.FirstName,
                        mainAccountCode = getLoanType.loanacct.Substring(0, 4),
                        subtype="0",
                        ispersonel = true,
                        createdby = user,
                        datecreated = System.DateTime.Now,
                    });
                await unitOfWork.Done();
                }
            }
            List<Pf_LoanRegister> list_loanregister = new List<Pf_LoanRegister>();
            //fetch Persons ID
            foreach (var s in loanCaptures)
            {
                var getPersonId = unitOfWork.person.GetPersonBySVC_No(x => x.SVC_NO == s.SVC_NO);
                int personidnotavailable = -1; int databasetenure = 0;
                var getLoanType = unitOfWork.loanType.GetLoanTypeByCode(x => x.Description == s.LOAN_TYPE);

                string remarks = "";
                remarks += getPersonId == null ? "Invalid Service Number" + "_" + s.SVC_NO : "";
                remarks += getLoanType == null ? "|Invalid Loan Type" + "_" + s.LOAN_TYPE : "";
                string statustosubmit = "9";
                if (getLoanType != null)
                {

                    databasetenure = Int32.Parse(getLoanType.Tenure);

                    if (databasetenure != s.TENURE)
                    {
                        remarks += "|Incorrect Loan Tenure Supplied" + "_" + s.TENURE;
                    }
                }

                //check eligibility
                decimal amountYOUcantake = unitOfWork.loanRegisterRepository.loanAmountPerRankBy(getPersonId.rank, getLoanType.Code).Result;

                if (s.AMOUNT > amountYOUcantake)
                {
                    remarks += "|You can not take more than" + "_" + amountYOUcantake;
                }
                statustosubmit = remarks == "" ? "1" : "2";


                unitOfWork.loanRegisterRepository.Create(new Pf_LoanRegister()
                {
                    Amount = s.AMOUNT,
                    PersonID = getPersonId == null ? personidnotavailable : getPersonId.PersonID,
                    FundTypeID = fundid,
                    LoanTypeID = getLoanType == null ? -1 : getLoanType.Id,
                    loanAccount =  getLoanType.loanacct.Substring(0,5)+s.SVC_NO,
                    Tenure = databasetenure != s.TENURE ? "-1" : "" + s.TENURE,
                    remarks = null,
                    Status = "9",
                    createdby = user,
                    StatusAndStatusDate = ":" + remarks + ": Loan Application on: " + System.DateTime.Now,
                    VoucherNo = null,
                    datecreated = System.DateTime.Now,
                    BankID = null,
                    ApproveDate =Convert.ToDateTime(s.BANK),
                    ChequeNo = s.CHEQUE_NO,
                    EffectiveDate = Convert.ToDateTime(s.BANK),
                    ExpDate =Convert.ToDateTime("2021/09/21"),
                    loancount = Convert.ToInt32(s.MONTHS_PAID),
                    LoanAppNo =s.VOUCHER_NO+"-"+ s.SVC_NO
                });
                await unitOfWork.Done();
                // }

            }
        }
    }
}
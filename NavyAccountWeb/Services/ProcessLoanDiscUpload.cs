using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Entities;
using NavyAccountCore.Entities;
using NavyAccountCore.Models;
using NavyAccountWeb.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NavyAccountWeb.Services
{
    public class ProcesLoanDiscUpload
    {
        private readonly List<LoanDiscUploadVM> loanCaptures;
        //private readonly List<ContrDiscUpload> loanCaptureContr;
        //private readonly IPersonService personService;
        //private readonly ILoanTypeService loanTypeService;
        //private readonly ILoanRegisterService loanRegisterService;
        private readonly IUnitOfWork unitOfWork;
        private int fundid;
        private string fundcode;
        private string fundscode;
        private string user;
        private string processingperiod;
        private string batchNo;
        public ProcesLoanDiscUpload(List<LoanDiscUploadVM> loanCaptures, IUnitOfWork unitOfWork, int fundid, string fundcode,string user, string processingperiod,string fundscode,string batchNo) {

            this.loanCaptures = loanCaptures;
            this.fundid = fundid;
            this.fundcode = fundcode;
            this.fundscode = fundscode;
            this.user = user;
            this.processingperiod = processingperiod;
            this.unitOfWork = unitOfWork;
            this.batchNo = batchNo;
        }


        public async Task<string> processUploadInThread()
        {
            string message="";
            List<pf_loandisc> list_loanregister = new List<pf_loandisc>();
            //fetch Persons ID
            foreach (var s in loanCaptures)
            {
                
                var getPersonId = unitOfWork.person.GetPersonBySVC_No(x => x.SVC_NO == s.svcno);
                var getloanType = unitOfWork.loanType.GetLoanTypeByCode(x => x.Code == fundscode);
                string account = getloanType.loanacct.Substring(0, 4) + "-" + getPersonId.SVC_NO;
                string batch = s.batchno + "_" + getloanType.Description;

                var getbatch = unitOfWork.pf_loandisc.GetloanDiscByBatch(account, batch);
                var getcontrDisc = unitOfWork.pf_loandisc.GetloanDiscByCode(x => x.loanact == account);
                var getthebatch = unitOfWork.pf_loandisc.GetloanDiscByCode(x => x.batchno == batch);
                if (getthebatch == null)
                {
                    message = "batch not fund";
                    return message;
                }
                if (getcontrDisc != null)
                {
                    getbatch.extpay = s.amount;
                    unitOfWork.pf_loandisc.Update(getbatch);
                }
                else
                {
                    unitOfWork.pf_loandisc.Create(new pf_loandisc()
                    {

                        createdby = null,
                        datecreated = System.DateTime.Now,
                        extpay = s.amount,
                        fundcode = fundscode,
                        loanact = account,
                        batchno = getloanType.Description,
                        trusteeact = getloanType.trusteeacct,
                        incomeact = getloanType.incomeacct,
                        intract = getloanType.interestacct,
                        loantype = getloanType.Id.ToString(),

                    }) ;
                }
                await unitOfWork.Done();
            }

            return message;

    }


            public void processUploadInThreadContr()
            {


                List<npf_contrdisc> list_loanregister = new List<npf_contrdisc>();
                //fetch Persons ID
                foreach (var s in loanCaptures)
                {
                    var getPersonId = unitOfWork.person.GetPersonBySVC_No(x => x.SVC_NO == s.svcno);
                    var getfundType = unitOfWork.FundType.GetFundTypeByCode(x => x.Code == fundscode).Result;
                    string account = getfundType.fundacct.Substring(0, 4) + "-" + s.svcno;

                    var getcontrDisc = unitOfWork.npf_contrdisc.GetContrDiscByCode(x => x.contract == account);

                if (getcontrDisc != null)
                {
                    getcontrDisc.extamt = s.amount;
                    unitOfWork.npf_contrdisc.Update(getcontrDisc);
                }
                else
                {
                    unitOfWork.npf_contrdisc.Create(new npf_contrdisc()
                    {
                        createdby = null,
                        datecreated = System.DateTime.Now,
                        extamt = s.amount,
                        fundcode = fundscode,
                        contract = account,
                        trusteeact = getfundType.trusteeacct,
                        incomeact = getfundType.incomeacct,
                        intract = getfundType.interestacct,
                        
                    });
                }
                    unitOfWork.Done().Wait();
                }

            }
    }
}

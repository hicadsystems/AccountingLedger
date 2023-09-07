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
    public class ClaimProcessUpload
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly List<ClaimCapture> Captures;
        private readonly IClaimTypeServices services;
        private readonly IFundTypeService funds;


        public ClaimProcessUpload(List<ClaimCapture> Captures,IUnitOfWork unitOfWork, IClaimTypeServices services,IFundTypeService funds)
        {
            this.unitOfWork = unitOfWork;
            this.Captures = Captures;
            this.services = services;
            this.funds = funds;
        }

        public async Task<List<ClaimCapture>> claimUploadInThread()
        {

            List<ClaimCapture> result = new List<ClaimCapture>();
            foreach(var j in Captures)
            {
                int PersonID = 0;
                string svcno = "";
                string typecode = "";
                int bankid = 0;
                int rankid = 0;
                var getperson = unitOfWork.person.GetPersonBySVC_No(x => x.SVC_NO==j.svcno.Trim());
                var getbank = unitOfWork.bank.GetBankbyName(x => x.bankcode == j.bank);
                var getrank = unitOfWork.rank.GetRankbyName(x => x.Description == j.rank);
                var gettype = unitOfWork.FundType.checkfundsByDesc(j.type);

                //var kp = funds.checkfundsByDesc("10"); validateField(getperson) == "valid"||
                //if (validateField(getperson) == "valid")
                //{
                    //if (validateFieldDuplicate(getperson.SVC_NO, gettype.Code) == "valid")
                    //{
                        List<Npf_ClaimRegister> result2 = new List<Npf_ClaimRegister>();
                    //var res = process(j.svcno, gettype.Code);
                    //result2.Add(res);
                    //if (gettype.Description.ToUpper() != "DEPENDANT FUND")
                    //    res.AmountDue = j.amount;
                    if (getperson != null)
                    {
                        PersonID = getperson.PersonID;
                        svcno = getperson.SVC_NO;
                    }
                    else
                    {
                        if (j.type == "Navip") {
                            unitOfWork.person.Create(new Person()
                            {
                                rank = getrank.Id,
                                SVC_NO = j.svcno,
                                LastName = j.accountname,
                                // FirstName = s.OTHERNAME,
                                CreatedDate = System.DateTime.Now,
                            });
                            await unitOfWork.Done();

                            getperson = unitOfWork.person.GetPersonBySVC_No(x => x.SVC_NO == j.svcno.Trim());
                            PersonID = getperson.PersonID;
                            svcno = getperson.SVC_NO;
                        }
                        else { 
                            PersonID = 0;
                            svcno = j.svcno;
                        }
                    }
                if (getbank != null)
                {
                    bankid = getbank.Id;
                }
                else
                {
                    bankid=0;
                }
                
                if (getrank != null)
                {
                    rankid = getrank.Id;
                }
                else
                {
                    rankid = 1;
                 
                }
                unitOfWork.claimregister.Create(new Npf_ClaimRegister
                    {
                        PersonID = PersonID,
                        svcno=svcno,
                        appdate = DateTime.Now,
                        TotalContribution = j.amount,
                        FundTypeID = gettype.Code,
                        title = rankid,
                        AmountDue = j.amount,
                        acctno = j.accountno,
                        bank = bankid,
                        BatchNo = j.batchno,
                        Beneficiary = j.accountname,
                        Remark = j.remark
                    }) ;

                        await unitOfWork.Done();

                       // continue;
                    //}
                    //else
                    //{
                    //    result.Add(new ClaimCapture
                    //    {
                    //        svcno = j.svcno,
                    //        accountname = j.accountname,
                    //        amount = j.amount,
                    //        accountno = j.accountno,
                    //        bank = getbank.bankname,
                    //        batchno = j.batchno,
                    //        type = j.type,
                    //        rank=getrank.Description,
                    //        remark = "Exist, Check "+j.svcno+" and "+j.type+" and load again" 
                    //    });
                    //}
                }
                //else
                //{
                //    result.Add(new ClaimCapture
                //    {
                //        svcno = j.svcno,
                //        accountname = j.accountname,
                //        amount = j.amount,
                //        accountno = j.accountno,
                //        bank = getbank.bankname,
                //        batchno = j.batchno,
                //        type = j.type,
                //        rank = getrank.Description,
                //        remark = "Invalid Service No" +j.svcno
                //    });
                //}
           // }

            return result;


        }

        public async Task<List<Npf_ClaimRegister>> claimProcessUploadInThread()
        {

            List<Npf_ClaimRegister> result = new List<Npf_ClaimRegister>();
            foreach (var j in Captures)
            {

                    var getperson = unitOfWork.person.GetPersonBySVC_No(x => x.SVC_NO == j.svcno);
                    var getbank = unitOfWork.bank.GetBankbyName(x => x.bankname == j.bank);
                    var getrank = unitOfWork.rank.GetRankbyName(x => x.Description == j.rank);
                    var gettype = unitOfWork.FundType.checkfundsByDesc(j.type);
                    var kp = gettype.Code;
                    if (validateField(getperson) == "valid")
                    {
                        var res = process(j.svcno, kp);
                        result.Add(res);


                        unitOfWork.claimregister.Create(new Npf_ClaimRegister
                        {
                            PersonID = getperson.PersonID,
                            svcno = getperson.SVC_NO,
                            appdate = DateTime.Now,
                            TotalContribution = res.TotalContribution,
                            FundTypeID = res.FundTypeID,
                            title = getrank.Id,
                            AmountDue = res.AmountDue,
                            acctno = res.acctno,
                            bank = getbank.Id,
                            BatchNo = j.batchno,
                            //status = j.status
                        });
                    }
                }
            

            await unitOfWork.Done();
            return result;
        }



        public Npf_ClaimRegister process(string serviceno,string fundtype)
        {
            var getperson = unitOfWork.person.GetPersonBySVC_No(x => x.SVC_NO == serviceno);
            decimal amount = 0M;
            decimal outstandAmt = 0M;
            decimal amountDue = 0M;
            decimal amt = 0M;
           
            //if (services.fundtypeDesc(fundtype).ToUpper() == "NAVIP")
            //{
            //    amount = services.GetNavipAmount(getperson.PersonID.ToString(), fundtype, out amt);
            //    outstandAmt = services.OutstandingLoanServices(getperson.PersonID.ToString());
            //    amountDue = amount - outstandAmt;
            //}

            if (services.fundtypeDesc(fundtype).ToUpper() == "DEPENDANT FUND")
            {
                amount = services.GetDependentAmount(getperson.PersonID.ToString(), fundtype);
                amountDue = amount;
            }

            Npf_ClaimRegister val = new Npf_ClaimRegister();
            val.svcno = serviceno;
            val.TotalContribution = amount;
            val.amountPaid = amt;
            val.amountReceived = outstandAmt;
            val.AmountDue = amountDue;
            val.FundTypeID = fundtype;


            return val;

        }

        public string validateField(Person gp)
        {
            string message = "valid";
            if (gp == null)
            {
                message = "invalid service no";
            }
            

            return message;
        }
        public string validateFieldDuplicate(string svcno,string code)
        {
            string message = "valid";
            var ck = services.GetPerson(svcno, code);
            if (ck != null)
            {
                message = "service no Exist";
            }


            return message;
        }

    }
}

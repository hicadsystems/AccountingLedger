using MoreLinq;
using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Entities;
using NavyAccountCore.Core.IRepositories;
using NavyAccountCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountCore.Core.Repositories
{
    public class ClaimTypeRepository : IClaimRepository
    {
        private readonly INavyAccountDbContext context;
        public ClaimTypeRepository(INavyAccountDbContext context)
        {
            this.context = context;
        }

        public bool checkClaimBatchnoExist(string batchno)
        {
            return context.Npf_ClaimRegisters.Any(x => x.BatchNo == batchno);
        }

        public string fundTypeDesc(string fundtype)
        {
            var pol = context.pf_Funds.FirstOrDefault(x => x.Code == fundtype);
            if (pol == null)
            {
                return string.Empty;
            }
            else
            {
                return pol.Description;
            }
        }
        public Npf_ClaimRegister Getperson(string svcNo, string fundtype)
        {
          var ck= context.Npf_ClaimRegisters.Where(x => x.svcno == svcNo && x.FundTypeID == fundtype).FirstOrDefault();
           return ck;
        }
        public ClaimModel2 GetpersonClaim(string svcNo, string fundtype)
        {
          var ck= (from claim in context.Npf_ClaimRegisters
                   join per in context.person on claim.PersonID equals per.PersonID
                   join fund in context.pf_Funds on claim.FundTypeID equals fund.Code
                   join bank in context.py_Banks on claim.bank equals bank.Id
                   where (claim.PersonID.ToString() == svcNo  && fund.Code == fundtype)
             select new ClaimModel2
             {

                 svcno = claim.svcno,
                 TotalContribution = Math.Round(claim.TotalContribution, 2),
                 Beneficiary = claim.Beneficiary,
                 acctno = claim.acctno,
                 bank = bank.bankname,
                 Remark = claim.Remark,
                 designation = "Civilian",
                 batchno = claim.BatchNo,
                 statusdate=claim.statusdate,
                 appdate=claim.appdate
                 

             }).FirstOrDefault();
            return ck;
        }
        public decimal GetAmountPerDependent(string svcNo,string fundtype)
        {
            var per = context.person.FirstOrDefault(x => x.PersonID ==Convert.ToInt32(svcNo));
            var gh = context.npf_Contributions.First(x => x.fundtype == fundtype);
            decimal amount = 0M;

            if (per.rank == 1)
                amount = gh.amount01;
            if (per.rank == 2)
                amount = gh.amount02;
            if (per.rank == 3)
                amount = gh.amount03;
            if (per.rank == 4)
                amount = gh.amount04;
            if (per.rank == 5)
                amount = gh.amount05;
            if (per.rank == 6)
                amount = gh.amount06;
            if (per.rank == 7)
                amount = gh.amount07;
            if (per.rank == 8)
                amount = gh.amount08;
            if (per.rank == 9)
                amount = gh.amount09;
            if (per.rank == 10)
                amount = gh.amount10;
            if (per.rank == 11)
                amount = gh.amount11;
            if (per.rank == 12)
                amount = gh.amount12;
            if (per.rank == 13)
                amount = gh.amount13;
            if (per.rank == 14)
                amount = gh.amount14;
            if (per.rank == 15)
                amount = gh.amount15;
            if (per.rank == 16)
                amount = gh.amount16;
            if (per.rank == 17)
                amount = gh.amount17;
            if (per.rank == 18)
                amount = gh.amount18;
            if (per.rank == 19)
                amount = gh.amount19;
            if (per.rank == 20)
                amount = gh.amount20;

          

            return amount;

        }



        public decimal GetAmountPerNavip(string svcNo, string fundtype,out decimal amt)
        {
            var per = context.person.FirstOrDefault(x => x.PersonID == int.Parse(svcNo));
            var gh = context.npf_Contributions.First(x => x.fundtype == fundtype);
            decimal amount = 0M;

            if (per.rank == 1)
                amount = gh.amount01;
            if (per.rank == 2)
                amount = gh.amount02;
            if (per.rank == 3)
                amount = gh.amount03;
            if (per.rank == 4)
                amount = gh.amount04;
            if (per.rank == 5)
                amount = gh.amount05;
            if (per.rank == 6)
                amount = gh.amount06;
            if (per.rank == 7)
                amount = gh.amount07;
            if (per.rank == 8)
                amount = gh.amount08;
            if (per.rank == 9)
                amount = gh.amount09;
            if (per.rank == 10)
                amount = gh.amount10;
            if (per.rank == 11)
                amount = gh.amount11;
            if (per.rank == 12)
                amount = gh.amount12;
            if (per.rank == 13)
                amount = gh.amount13;
            if (per.rank == 14)
                amount = gh.amount14;
            if (per.rank == 15)
                amount = gh.amount15;
            if (per.rank == 16)
                amount = gh.amount16;
            if (per.rank == 17)
                amount = gh.amount17;
            if (per.rank == 18)
                amount = gh.amount18;
            if (per.rank == 19)
                amount = gh.amount19;
            if (per.rank == 20)
                amount = gh.amount20;

            decimal amount2 = (0.1M * amount) + amount;

            amt = (0.1M * amount);

            return amount2;

        }

        public List<Npf_ClaimRegister> GetBatchList()
        {
            var jk=context.Npf_ClaimRegisters.Where(x=>x.BatchNo!=null && x.status=="Pending").ToList();
            return jk.DistinctBy(x => x.BatchNo).ToList();
        }

        public List<Npf_ClaimRegister> GetSingleBatchno(string batchno)
        {
            return context.Npf_ClaimRegisters.Where(x => x.BatchNo == batchno).ToList();
        }




        public decimal OutStandingLoanClaims(string svcNo)
        {
            decimal? sum = 0M;
            var userBySvcNo= context.person.FirstOrDefault(x => x.PersonID ==Convert.ToInt32(svcNo));
            //var loanRecordbyPerson =(from p in context.pf_LoanRegisters
            //                         where(p.PersonID== Convert.ToInt32(svcNo) && p.Status=="9" || p.Status=="10")
                                     
               // context.pf_LoanRegisters.Where(x => x.PersonID == Convert.ToInt32(svcNo) && x.Status == "9" || x.Status == "10").ToList();
            var loanRecordbyPerson = context.pf_LoanRegisters.Where(x => x.PersonID == Convert.ToInt32(svcNo) && x.Status == "9" || x.Status == "10").ToList();

            foreach(var j in loanRecordbyPerson)
            {
                var pol = context.Pf_loanType.FirstOrDefault(x => x.Id == j.LoanTypeID);
                if (pol != null)
                {
                    decimal? intrest = (int.Parse(pol.Interest) * j.Amount) / 100;
                    decimal? deductions = j.Amount / int.Parse(j.Tenure);
                    decimal? outstndLoans = (int.Parse(j.Tenure) - j.loancount) * deductions;

                    sum +=intrest + outstndLoans;
                }
               
            }

            return (decimal)sum;

        }


       
    }
}

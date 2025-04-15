using Microsoft.EntityFrameworkCore;
using MoreLinq.Extensions;
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
    public class ClaimRepository : Repository<Npf_ClaimRegister>, IClaimRegister
    {
        private readonly INavyAccountDbContext context;

        public ClaimRepository(INavyAccountDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<Npf_ClaimRegister> getClaimByUser2(string svcno,string fundtype)
        {
            return await context.Npf_ClaimRegisters.FirstOrDefaultAsync(x => x.svcno == svcno && x.FundTypeID==fundtype);
        }

        public async Task<Npf_ClaimRegister> getClaimByUser(string svcno)
        {
            return await context.Npf_ClaimRegisters.FirstOrDefaultAsync(x => x.svcno == svcno);
        }

        public IEnumerable<ClaimModel> GetclaimBysvcNo(string svcno)
        {
            return (from claim in context.Npf_ClaimRegisters
                    join per in context.pf_Funds on claim.FundTypeID equals per.Code
                    where ( claim.svcno == svcno)
                    select new ClaimModel
                    {

                        Id = claim.Id,
                        funddesc = per.Description,
                        svcno = claim.svcno,
                        interest = claim.interest,
                        amountPaid = claim.TotalContribution,
                        amountReceived = claim.loan,
                        AmountDue = claim.AmountDue,
                        Beneficiary = claim.Beneficiary,

                    }).ToList();
        }

        public IEnumerable<ClaimModel> Getclaim(string svcno,string fundcode)
        {
            return (from claim in context.Npf_ClaimRegisters
                    join per in context.pf_Funds on claim.FundTypeID equals per.Code
                    where (claim.FundTypeID == fundcode && claim.svcno ==svcno)
                    select new ClaimModel
                    {
                        Id = claim.Id,
                        funddesc = per.Description,
                        svcno = claim.svcno,
                        interest = claim.interest,
                        amountPaid = claim.TotalContribution,
                        amountReceived = claim.loan,
                        AmountDue = claim.AmountDue,
                        Beneficiary = claim.Beneficiary,
                      
                    }).ToList();
        }

       
        public IEnumerable<ClaimModel2> Getclaimbydate(DateTime startdate, DateTime enddate,string status)
        {
            var result =new List<ClaimModel2>();
            var result2 = new List<ClaimModel2>();
            if (status == "All")
            {
               return (from claim in context.Npf_ClaimRegisters
                    join per in context.pf_Funds on claim.FundTypeID equals per.Code
                    join bank in context.py_Banks on claim.bank equals bank.Id
                    where (claim.appdate.Date >= startdate.Date && claim.appdate.Date <= enddate.Date) 
                    select new ClaimModel2
                    {
                   
                        svcno = claim.svcno,
                        TotalContribution =Math.Round(claim.TotalContribution,2),
                        Beneficiary = claim.Beneficiary,
                        acctno=claim.acctno,
                        bank=bank.bankname,
                        Remark=claim.Remark,
                        designation="Civilian",
                        batchno = claim.BatchNo,
                       

                    }).ToList();
            }
            else
            {
                var dd= (from claim in context.Npf_ClaimRegisters
                        join per in context.pf_Funds on claim.FundTypeID equals per.Code
                        join bank in context.py_Banks on claim.bank equals bank.Id
                        where (claim.appdate.Date >= startdate.Date && claim.appdate.Date <= enddate.Date) && claim.FundTypeID==status
                        select new ClaimModel2
                        {

                            svcno = claim.svcno,
                            TotalContribution = claim.TotalContribution,
                            Beneficiary = claim.Beneficiary,
                            acctno = claim.acctno,
                            bank = bank.bankname,
                            Remark = claim.Remark,
                            designation = "Civilian",
                            batchno=claim.BatchNo,
                             startdate = startdate,
                            enddate = enddate,

                        }).ToList();
                return dd.ToList();

            }
        }

        public IEnumerable<ClaimModel> GetClaimNotApproved()
        {
            return (from claim in context.Npf_ClaimRegisters
                    join per in context.person on claim.svcno equals per.SVC_NO
                    where (claim.status == "Pending")
                    select new ClaimModel
                    {
                        PersonID = per.PersonID,
                        svcno = claim.svcno + "-" + per.LastName + per.FirstName
                    }).DistinctBy(x=>x.PersonID).ToList();
        }
        public IEnumerable<ClaimModel> GetClaimApproved()
        {
            return (from claim in context.Npf_ClaimRegisters
                    join per in context.person on claim.svcno equals per.SVC_NO
                    where (claim.status == "Approved")
                    select new ClaimModel
                    {
                        PersonID = per.PersonID,
                        svcno = claim.svcno + "-" + per.LastName + per.FirstName
                    }).DistinctBy(x => x.PersonID).ToList();
        }

        public IEnumerable<ClaimModel> GetClaimNotApprovedList(int personId)
        {
            var opo = context.Npf_ClaimRegisters.Where(x => x.PersonID == personId && x.status!="Approved");

            var result = new List<ClaimModel>();

            foreach(var j in opo)
            {
                var k = new ClaimModel();
                k.svcno = j.svcno;
                k.PersonID = j.PersonID;
                k.TotalContribution = j.TotalContribution;
                k.loan = j.loan;
                k.AmountDue = j.AmountDue;
                k.interest = j.interest;
                k.FundTypeID = j.FundTypeID;
               
                result.Add(k);
            }

            return result;
           
        }
        public IEnumerable<ClaimModel> GetClaimApprovedList(int personId)
        {
            var opo = context.Npf_ClaimRegisters.Where(x => x.PersonID == personId && x.status == "Approved");

            var result = new List<ClaimModel>();

            foreach (var j in opo)
            {
                var k = new ClaimModel();
                k.svcno = j.svcno;
                k.PersonID = j.PersonID;
                k.TotalContribution = j.TotalContribution;
                k.loan = j.loan;
                k.AmountDue = j.AmountDue;
                k.interest = j.interest;
                k.FundTypeID = j.FundTypeID;

                result.Add(k);
            }

            return result;

        }

        public string getDesc(string fundtypeid)
        {
            var desc = context.pf_Funds.FirstOrDefault(x => x.Code == fundtypeid).Description;
            if (desc != null)
            {
                return desc;
            }
            else
            {
                return string.Empty;
            }
        }

     
    }
}

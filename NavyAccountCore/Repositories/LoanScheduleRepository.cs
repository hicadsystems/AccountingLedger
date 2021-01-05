using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.IRepositories;
using NavyAccountCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NavyAccountCore.Core.Repositories
{
    public class LoanScheduleRepository : ILoanSchedule
    {
        private readonly INavyAccountDbContext context;
        public LoanScheduleRepository(INavyAccountDbContext context)
        {
            this.context = context;
        }
        public IEnumerable<LoanView> CalculateLoan(int id, int loantypeid)
        {
            var op = context.Pf_loanType.FirstOrDefault(x => x.Id == loantypeid).Code;

            var gh = context.person.FirstOrDefault(x => x.PersonID == id);
            var fg = context.Pf_loanType.FirstOrDefault(x => x.Code == op);
            int tenure = int.Parse(fg.Tenure);
            double interest = Convert.ToDouble(fg.Interest);
            decimal amount=0M;
            double amount2;

            var rf = context.loanPerRank.FirstOrDefault(x => x.loantype == op);
            if (gh.rank == 1)
                amount = rf.amount01;
            if (gh.rank == 2)
                amount = rf.amount02;
            if (gh.rank == 3)
                amount = rf.amount03;
            if (gh.rank == 4)
                amount = rf.amount04;
            if (gh.rank == 5)
                amount = rf.amount05;
            if (gh.rank == 6)
                amount = rf.amount06;
            if (gh.rank == 7)
                amount = rf.amount07;
            if (gh.rank == 8)
                amount = rf.amount08;
            if (gh.rank == 9)
                amount = rf.amount09;
            if (gh.rank == 10)
                amount = rf.amount10;
            if (gh.rank == 11)
                amount = rf.amount11;
            if (gh.rank == 12)
                amount = rf.amount12;
            if (gh.rank == 13)
                amount = rf.amount13;
            if (gh.rank == 14)
                amount = rf.amount14;
            if (gh.rank == 15)
                amount = rf.amount15;
            if (gh.rank == 16)
                amount = rf.amount16;
            if (gh.rank == 17)
                amount = rf.amount17;
            if (gh.rank == 18)
                amount = rf.amount18;
            if (gh.rank == 19)
                amount = rf.amount19;
            if (gh.rank == 20)
                amount = rf.amount20;


            amount2 = Convert.ToDouble(amount);


            double a = (amount2 * interest) / 100;
            double b = amount2 + a;
            double sumE = 0.0;

            List<LoanView> AllRecord = new List<LoanView>();

            for (int j = 1; j <= tenure; j++)
            {
                for (int k = 1; k < 2; k++)
                {
                    sumE += ((amount2 + a) / tenure);
                    var result = new LoanView
                    {
                        openingBalance = Math.Round(b, 2),
                        PrincipalRepayment = Math.Round((amount2 / tenure), 2),
                        interestPayment = Math.Round((a / tenure), 2),
                        MonthlyDeduction = Math.Round(((amount2 + a) / tenure), 2),
                        TotalRepaymentToDate = Math.Round(sumE, 2),
                        OutstandingBalance = Math.Round((b - ((amount2 + a) / tenure)), 2)
                    };

                    b = result.OutstandingBalance;

                    AllRecord.Add(result);
                }
            }


            return AllRecord;

        }

        public LoanView2 GetLoan(int personid, int loantype)
        {
            int interest = 0;
            var loanr = context.pf_LoanRegisters.FirstOrDefault(x => x.PersonID == personid);
            var loanreview = context.npf_loantypereview.Where(x => Convert.ToInt32(x.LoanType) == loantype);
            foreach (var r in loanreview)
            {


                int rdate = r.ReviewDate.Year;
                int edate = loanr.EffectiveDate.Year;
                if (rdate > edate)
                {
                    continue;

                }
                else
                {
                    interest = r.Interestrate;
                }
            }
            var co=(from p in context.pf_LoanRegisters 
                    join q in context.person on p.PersonID equals q.PersonID
                     join r in context.Pf_loanType on p.LoanTypeID equals r.Id
                     join s in context.ranks on q.rank equals s.Id
                     select new LoanView2
                     {
                         personid=(int)p.PersonID,
                         loantype=(int)p.LoanTypeID,
                         loancode=r.Code,
                         Name=q.LastName+" "+q.FirstName,
                         Email=q.email,
                         Rank=s.Description,
                         rankId=q.rank,
                         Tenor= Convert.ToDecimal(r.Tenure),
                         interest=interest
                     });

            var cop = co.FirstOrDefault(x => x.personid == personid && x.loantype == loantype);

            if (cop == null)
                return cop;
            var rf = context.loanPerRank.FirstOrDefault(x => x.loantype == cop.loancode);

            if (cop.rankId == 1)
                cop.AmountGranted = rf.amount01;
            if (cop.rankId == 2)
                cop.AmountGranted = rf.amount02;
            if (cop.rankId == 3)
                cop.AmountGranted = rf.amount03;
            if (cop.rankId == 4)
                cop.AmountGranted = rf.amount04;
            if (cop.rankId == 5)
                cop.AmountGranted = rf.amount05;
            if (cop.rankId == 6)
                cop.AmountGranted = rf.amount06;
            if (cop.rankId == 7)
                cop.AmountGranted = rf.amount07;
            if (cop.rankId == 8)
                cop.AmountGranted = rf.amount08;
            if (cop.rankId == 9)
                cop.AmountGranted = rf.amount09;
            if (cop.rankId == 10)
                cop.AmountGranted = rf.amount10;
            if (cop.rankId == 11)
                cop.AmountGranted = rf.amount11;
            if (cop.rankId == 12)
                cop.AmountGranted = rf.amount12;
            if (cop.rankId == 13)
                cop.AmountGranted = rf.amount13;
            if (cop.rankId == 14)
                cop.AmountGranted  = rf.amount14;
            if (cop.rankId == 15)
                cop.AmountGranted = rf.amount15;
            if (cop.rankId == 16)
                cop.AmountGranted = rf.amount16;
            if (cop.rankId == 17)
                cop.AmountGranted = rf.amount17;
            if (cop.rankId == 18)
                cop.AmountGranted = rf.amount18;
            if (cop.rankId == 19)
                cop.AmountGranted = rf.amount19;
            if (cop.rankId == 20)
                cop.AmountGranted = rf.amount20;


            return cop;

        }

        public int getLoanCount(int personid, int loantype)
        {
            var dp= context.pf_LoanRegisters.FirstOrDefault(x => x.PersonID == personid && x.LoanTypeID == loantype);
            if (dp == null)
                return 0;
            return dp.loancount;
        }



    }
}

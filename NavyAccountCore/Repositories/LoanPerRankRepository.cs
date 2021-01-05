using Microsoft.EntityFrameworkCore;
using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Entities;
using NavyAccountCore.Core.IRepositories;
using NavyAccountCore.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountCore.Core.Repositories
{
    public class LoanPerRankRepository : Repository<loanPerRank>, ILoanPerRank
    {
        private readonly INavyAccountDbContext context;

        public LoanPerRankRepository(INavyAccountDbContext context) : base(context)
        {
            this.context = context;
        }

        public IEnumerable<LoanReportView> getAllNpf(string loan)
        {
           return(from p in context.loanPerRank join
                  q in context.Pf_loanType on p.loantype equals q.Code
                  where p.loantype.Equals(loan)
                  select new LoanReportView
                  {
                      loan=q.Description,
                      amount01=p.amount01,
                      amount02=p.amount02,
                      amount03=p.amount03,
                      amount04=p.amount04,
                      amount05=p.amount05,
                      amount06=p.amount06,
                      amount07=p.amount07,
                      amount08=p.amount08,
                      amount09=p.amount09,
                      amount10=p.amount10,
                      amount11 = p.amount11,
                      amount12 = p.amount12,
                      amount13 = p.amount13,
                      amount14 = p.amount14,
                      amount15 = p.amount15,
                      amount16 = p.amount16,
                      amount17 = p.amount17,
                      amount18 = p.amount18,
                      amount19 = p.amount19,

                  }).ToList();
        }

        public async Task<loanPerRank> GetLoanByCode(Expression<Func<loanPerRank, bool>> predicate)
        {
            return await context.loanPerRank.FirstOrDefaultAsync(predicate);
        }
    }
}
using NavyAccountCore.Core.Entities;
using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.IRepositories;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using NavyAccountCore.Models;
using System.Collections.Generic;

namespace NavyAccountCore.Core.Repositories
{
    public class LoanStatusRepository : Repository<LoanStatus>, ILoanStatus
    {
        private readonly INavyAccountDbContext context;
        public LoanStatusRepository(INavyAccountDbContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<LoanStatus> GetLoanStatusByCode(Expression<Func<LoanStatus, bool>> predicate)
        {
            return await context.loanStatus.FirstOrDefaultAsync(predicate);
        }
        public async Task<List<LoanStatusVM>> GetloanStatus2(int id)
        {
            return await (from npfLoanStatus in context.loanStatus
                          where npfLoanStatus.Description !="Custom"
                          select new LoanStatusVM
                          {
                              Id=npfLoanStatus.Id,
                              desc = npfLoanStatus.Description,
                          }).ToListAsync();
        }
    }
}
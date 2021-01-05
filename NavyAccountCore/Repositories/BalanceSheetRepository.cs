using System.Collections.Generic;
using System.Linq;
using NavyAccountCore.Core.Entities;
using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.IRepositories;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System;
using Microsoft.EntityFrameworkCore;

namespace NavyAccountCore.Core.Repositories
{
    public class BalanceSheetRepository :Repository<npf_balsheet>, IBalsheet
    {
        private readonly INavyAccountDbContext context;
        public BalanceSheetRepository(INavyAccountDbContext context) : base(context)
        {
            this.context = context;
        }

      
        public async Task<npf_balsheet> GetBalanceSheetByCode(Expression<Func<npf_balsheet, bool>> predicate)
        {
            return await context.npf_Balsheets.FirstOrDefaultAsync(predicate);
        }

    }
}
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
    public class AccountHistoryRepository : Repository<npf_history>, IAccountHistory
    {
        private readonly INavyAccountDbContext context;
        public AccountHistoryRepository(INavyAccountDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<List<npf_history>> GetAccountHistoryList(string refno, string accountCode)
        {
            return await context.npf_Histories.Where(x=> x.refno == refno && x.acctcode == accountCode).ToListAsync();
        }

    }
}
using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Entities;
using NavyAccountCore.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace NavyAccountCore.Core.Repositories
{
    public class TrialBalanceHistoryRepository:Repository<npf_history>,ITrialBalanceHistory
    {
        private readonly INavyAccountDbContext context;
        public TrialBalanceHistoryRepository(INavyAccountDbContext context):base(context)
        {
            this.context = context;
        }
    }
}

using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Entities;
using NavyAccountCore.Core.IRepositories;
using System.Linq;

namespace NavyAccountCore.Core.Repositories
{
    public class TrialBalanceRepository:Repository<npf_ledger>,ITrialBalance
    {
        private readonly INavyAccountDbContext context;

        public TrialBalanceRepository(INavyAccountDbContext context):base(context)
        {
            this.context = context;
        }

        public bool checkAcctCode(string acctcode)
        {
            bool check = false;

            if (context.npf_Charts.Any(x => x.acctcode == acctcode))
            {
                check = true;
            }

            return check;
        }
    }
}

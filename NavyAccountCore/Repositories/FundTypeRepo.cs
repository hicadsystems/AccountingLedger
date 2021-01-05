using Microsoft.EntityFrameworkCore;
using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Entities;
using NavyAccountCore.Core.IRepositories;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;

namespace NavyAccountCore.Core.Repositories
{
    public class FundTypeRepo : Repository<Pf_fund>, IFundType
    {
        private readonly INavyAccountDbContext context;
        public FundTypeRepo(INavyAccountDbContext context) : base(context)
        {
            this.context = context;
        }

        public Pf_fund checkfundsByDesc(string desc)
        {
            return context.pf_Funds.FirstOrDefault(x=>x.Description==desc);
        }

        public async Task<Pf_fund> GetFundTypeByCode(Expression<Func<Pf_fund, bool>> predicate)
        {
            return await context.pf_Funds.FirstOrDefaultAsync(predicate);
        }

    }
}
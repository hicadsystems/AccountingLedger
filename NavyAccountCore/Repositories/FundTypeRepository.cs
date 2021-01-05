using NavyAccountCore.Core.Entities;
using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.IRepositories;
using System.Linq;
using System.Linq.Expressions;
using System;

namespace NavyAccountCore.Core.Repositories
{
    public class FundTypeRepository : Repository<npf_fundType>, IFundTypeCode
    {
        private readonly INavyAccountDbContext context;
        public FundTypeRepository(INavyAccountDbContext context) : base(context)
        {
            this.context = context;
        }

        public npf_fundType Getfundtypebycode(Expression<Func<npf_fundType, bool>> predicate)
        {
            return context.npffundType.FirstOrDefault(predicate);
        }
    }
}
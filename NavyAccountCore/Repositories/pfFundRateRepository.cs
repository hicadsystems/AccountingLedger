using Microsoft.EntityFrameworkCore;
using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Entities;
using NavyAccountCore.Core.IRepositories;
using NavyAccountCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NavyAccountCore.Core.Repositories
{
    public class PfFundRateRepository:Repository<Pf_fundRate>,IPfundrate
    {

        private readonly INavyAccountDbContext context;

        public PfFundRateRepository(INavyAccountDbContext context):base(context)
        {
            this.context = context;
        }

        public async Task<List<fundtypeView>> GetFundList()
        {
            return await (from p in context.pf_Funds
                          select new fundtypeView
                          {
                              pffund_Code=p.Code,
                              pffund_Description=p.Description
                          }).ToListAsync();
               
        }

        public async Task<Pf_fundRate> GetFundRateTypeByCode(Expression<Func<Pf_fundRate, bool>> predicate)
        {
            return await context.pf_FundRates.FirstOrDefaultAsync(predicate);
        }
    }
}

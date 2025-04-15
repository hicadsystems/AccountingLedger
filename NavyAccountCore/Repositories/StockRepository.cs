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
    public class StockRepository :Repository<npf_Stocks>, IStock
    {
        private readonly INavyAccountDbContext context;
        public StockRepository(INavyAccountDbContext context) : base(context)
        {
            this.context = context;
        }

      
        public async Task<npf_Stocks> GetStocksByCode(Expression<Func<npf_Stocks, bool>> predicate)
        {
            return await context.npf_Stocks.FirstOrDefaultAsync(predicate);
        }

    }
}
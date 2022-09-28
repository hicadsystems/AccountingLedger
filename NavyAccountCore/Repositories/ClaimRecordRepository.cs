using Microsoft.EntityFrameworkCore;
using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Repositories;
using NavyAccountCore.Entities;
using NavyAccountCore.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NavyAccountCore.Repositories
{
    public class ClaimRecordRepository:Repository<sr_ClaimRecord>, IClaimRecordRepository
    {
        private readonly INavyAccountDbContext context;
        public ClaimRecordRepository(INavyAccountDbContext context):base(context)
        {
            this.context = context;


        }

        public async Task<sr_ClaimRecord> GetClaimRecordByCode(Expression<Func<sr_ClaimRecord,bool>> predicate)
        {
            return await context.sr_ClaimRecord.FirstOrDefaultAsync(predicate);
        }
    }
}

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
    public class ParentGuardianRecordRepository:Repository<sr_ParentRecord>,IParentGuardianRecordRepository
    {
        private readonly INavyAccountDbContext context;
        public ParentGuardianRecordRepository(INavyAccountDbContext context):base(context)
        {
            this.context = context;
        }

        public async Task<sr_ParentRecord> GetParentByCode(Expression<Func<sr_ParentRecord,bool>> predicate)
        {
            return await context.sr_ParentRecord.FirstOrDefaultAsync(predicate);
        }
    }
}

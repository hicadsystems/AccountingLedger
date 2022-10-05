using Microsoft.EntityFrameworkCore;
using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Repositories;
using NavyAccountCore.Entities;
using NavyAccountCore.IRepositories;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NavyAccountCore.Repositories
{
    public class SchoolRecordRepository:Repository<sr_SchoolRecord>, ISchoolRecordRepository
    {
        private readonly INavyAccountDbContext context;
        public SchoolRecordRepository(INavyAccountDbContext context):base(context)
        {
            this.context = context;
        }

        public async Task<sr_SchoolRecord> GetSchoolByCode(Expression<Func<sr_SchoolRecord, bool>> predicate)
        {
            return await context.sr_SchoolRecord.FirstOrDefaultAsync(predicate);
        }
        public async Task<IEnumerable<sr_SchoolRecord>> GetAllSchool()
        {
            return await context.sr_SchoolRecord.ToListAsync();
                              
        }

    }
}

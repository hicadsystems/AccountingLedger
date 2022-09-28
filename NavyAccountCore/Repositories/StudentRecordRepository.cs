using Microsoft.EntityFrameworkCore;
using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Repositories;
using NavyAccountCore.Entities;
using NavyAccountCore.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NavyAccountCore.Repositories
{
    public class StudentRecordRepository : Repository<sr_StudentRecord>, IStudentRecordRepository
    {
        private readonly INavyAccountDbContext context;
        public StudentRecordRepository(INavyAccountDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<sr_StudentRecord> GetStudentByCode(Expression<Func<sr_StudentRecord, bool>> predicate)
        {
            return await context.sr_StudentRecord.FirstOrDefaultAsync(predicate);
        }
        public async Task<IEnumerable<sr_StudentRecord>> GetAllStudent()
        {
            return await context.sr_StudentRecord.ToListAsync();
        }
    }
}

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
        public decimal GetAmountPerSchoolType(string studentNo, out decimal amt)
        {
            var per = context.sr_StudentRecord.FirstOrDefault(x => x.id == int.Parse(studentNo));
             decimal amount = 0M;
            if (per.SchoolCode == "PRIMARY")
                amount =Convert.ToDecimal(200000);
            if (per.SchoolCode == "SECONDARY")
                amount = Convert.ToDecimal(400000);

           decimal amount2 =  amount;

            amt = (0.1M * amount);

            return amount2;

        }
    }
}

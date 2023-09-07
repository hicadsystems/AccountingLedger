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
   public class SchoolFeeRepository : Repository<sr_SchoolFeeTB>, ISchoolFeeRepository
    {
        private readonly INavyAccountDbContext context;
        public SchoolFeeRepository(INavyAccountDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<sr_SchoolFeeTB> GetSchoolFeeByCode(Expression<Func<sr_SchoolFeeTB, bool>> predicate)
        {
            return await context.sr_SchoolFeeTB.FirstOrDefaultAsync(predicate);
        }
        public async Task<List<SchoolFeeVM>> GetAllSchoolFee()
        {
            var dd = (from pers in context.sr_SchoolFeeTB
                      select new SchoolFeeVM
                      {

                          id = pers.id,
                          Period=pers.Period,
                          Class=pers.Class,
                          School=pers.School,
                          ParentStatus=pers.ParentStatus,
                          Type=pers.Type,
                          ClassCategory = pers.ClassCategory,
                          Amount=pers.Amount,
                          term=pers.term
                      }).ToListAsync();
            return await dd;

        }
        public async Task<List<SchoolFeeVM2>> GetAllSchoolFeeByPeriod(string period)
        {
            var dd = (from pers in context.sr_SchoolFeeTB
                      where (pers.Period==period)
                      select new SchoolFeeVM2
                      {

                          Period = pers.Period,
                          Class = pers.Class,
                          School = pers.School,
                          ParentStatus = pers.ParentStatus,
                          Type = pers.Type,
                          SchoolType = pers.ClassCategory,
                          Amount = pers.Amount,
                          Term = pers.term
                      }).ToListAsync();
            return await dd;

        }

    }
}

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
                      join sch in context.sr_SchoolRecord on pers.SchoolId equals sch.id
                      join cl in context.sr_ClassRecord on pers.ClassId equals cl.id
                      select new SchoolFeeVM
                      {

                          id = pers.id,
                          Period=pers.Period,
                          ClassId=pers.ClassId,
                          SchoolId=pers.SchoolId,
                          SchoolName = sch.Schoolname,
                          ClassName = cl.ClassName,
                          ClassCategory = pers.ClassCategory,
                          Amount=pers.Amount,
                          term=pers.term
                      }).ToListAsync();
            return await dd;

        }
        public async Task<List<SchoolFeeVM2>> GetAllSchoolFeeByPeriod(string period)
        {
            var dd = (from pers in context.sr_SchoolFeeTB
                      join sch in context.sr_SchoolRecord on pers.SchoolId equals sch.id
                      join cl in context.sr_ClassRecord on pers.ClassId equals cl.id
                      where (pers.Period==period)
                      select new SchoolFeeVM2
                      {

                          Period = pers.Period,
                          SchoolName = sch.Schoolname,
                          ClassName = cl.ClassName,
                          SchoolType = pers.ClassCategory,
                          Amount = pers.Amount,
                          Term = pers.term
                      }).ToListAsync();
            return await dd;

        }

    }
}

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

            //var dd= await (from sch in context.sr_SchoolRecord
            //       join st in context.sr_state on sch.SchoolState equals st.description
            //       join lga in context.sr_lga on sch.SchoolCity equals lga.code
            //       select new sr_SchoolRecord
            //       {
            //           Schoolname=sch.Schoolname,
            //           SchoolType=sch.SchoolType,
            //           SchoolAddress=sch.SchoolAddress,
            //           SchoolCity=lga.description,
            //           SchoolState=st.description
            //       }).ToListAsync();

            var dd = await (from sch in context.sr_SchoolRecord
                            select new sr_SchoolRecord
                            {
                                Schoolname = sch.Schoolname,
                                SchoolType = sch.SchoolType,
                                SchoolAddress = sch.SchoolAddress,
                                SchoolCity = sch.SchoolCity,
                                SchoolState = sch.SchoolState
                            }).ToListAsync();
            return dd;
        }

    }
}

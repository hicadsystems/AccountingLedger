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
        public async Task<List<sr_ParentRecord>> getParentList(int iDisplayStart, int iDisplayLength)
        {
             var dd= (from pers in context.sr_ParentRecord
                          select new sr_ParentRecord
                          {
                              id = pers.id,
                              Surname = pers.Surname,
                              OtherNames = pers.OtherNames,
                              Email = pers.Email,
                              Address = pers.Address,
                              PhoneNumber = pers.PhoneNumber,
                              Workclass = pers.Workclass
                          }).Skip(iDisplayStart).Take(iDisplayLength).ToListAsync();
            return await dd;
        }
        public async Task<List<sr_ParentRecord>> getParentListByName(string parentname)
        {
            return await (from pers in context.sr_ParentRecord
                              //join npfranks in context.ranks on pers.rank equals npfranks.Id
                              where pers.Surname.Contains(parentname)|| pers.OtherNames.Contains(parentname)
                          select new sr_ParentRecord
                          {
                              id = pers.id,
                              Surname = pers.Surname,
                              OtherNames = pers.OtherNames,
                              Email = pers.Email,
                              Address = pers.Address,
                              PhoneNumber = pers.PhoneNumber,
                              Workclass = pers.Workclass


                          }).ToListAsync();
        }
        public async Task<IEnumerable<sr_ParentRecord>> getAllParent()
        {
            //return await context.sr_ParentRecord.OrderByDescending(x=>x.Surname).ToListAsync();
            return await (from pers in context.sr_ParentRecord
                          select new sr_ParentRecord
                          {
                              id = pers.id,
                              Surname = pers.Surname + " " + pers.OtherNames,
                          }).OrderByDescending(x => x.Surname).ToListAsync();
        }
        public async Task<int> getParentListCount()
        {
            return await context.sr_ParentRecord.CountAsync();
        }
        }
}

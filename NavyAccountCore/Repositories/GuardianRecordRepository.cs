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
    public class GuardianRecordRepository:Repository<sr_GuardianRecord>,IGuardianRecordRepository
    {
        private readonly INavyAccountDbContext context;
        public GuardianRecordRepository(INavyAccountDbContext context):base(context)
        {
            this.context = context;
        }

        public async Task<sr_GuardianRecord> GetGuardianByCode(Expression<Func<sr_GuardianRecord,bool>> predicate)
        {
            return await context.sr_GuardianRecord.FirstOrDefaultAsync(predicate);
        }
        public async Task<List<sr_GuardianRecord>> getGuardianList(int iDisplayStart, int iDisplayLength)
        {
             var dd= (from pers in context.sr_GuardianRecord
                          select new sr_GuardianRecord
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
        public async Task<List<sr_GuardianRecord>> getGuardianListByName(string Guardianname)
        {
            return await (from pers in context.sr_GuardianRecord
                              //join npfranks in context.ranks on pers.rank equals npfranks.Id
                              where pers.Surname.Contains(Guardianname)|| pers.OtherNames.Contains(Guardianname)
                          select new sr_GuardianRecord
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
        public async Task<int> getGuardianListCount()
        {
            return await context.sr_GuardianRecord.CountAsync();
        }
        }
}

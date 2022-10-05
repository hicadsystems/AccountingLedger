using NavyAccountCore.Core.IRepositories;
using NavyAccountCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NavyAccountCore.IRepositories
{
    public interface IGuardianRecordRepository:IRepository<sr_GuardianRecord>
    {
        Task<sr_GuardianRecord> GetGuardianByCode(Expression<Func<sr_GuardianRecord, bool>> predicate);
        Task<List<sr_GuardianRecord>> getGuardianList(int iDisplayStart, int iDisplayLength);
        Task<int> getGuardianListCount();
        Task<List<sr_GuardianRecord>> getGuardianListByName(string Guardianname);
        Task<IEnumerable<sr_GuardianRecord>> getAllGuardian();
    }
}

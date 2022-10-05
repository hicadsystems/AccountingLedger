using NavyAccountCore.Core.IRepositories;
using NavyAccountCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NavyAccountCore.IRepositories
{
    public interface IParentGuardianRecordRepository:IRepository<sr_ParentRecord>
    {
        Task<sr_ParentRecord> GetParentByCode(Expression<Func<sr_ParentRecord, bool>> predicate);
        Task<List<sr_ParentRecord>> getParentList(int iDisplayStart, int iDisplayLength);
        Task<int> getParentListCount();
        Task<List<sr_ParentRecord>> getParentListByName(string parentname);
        Task<IEnumerable<sr_ParentRecord>> getAllParent();

    }
}

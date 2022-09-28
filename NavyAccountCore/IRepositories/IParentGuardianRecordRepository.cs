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
    }
}

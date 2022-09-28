using NavyAccountCore.Core.IRepositories;
using NavyAccountCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NavyAccountCore.IRepositories
{
    public interface IClaimRecordRepository:IRepository<sr_ClaimRecord>
    {
        Task<sr_ClaimRecord> GetClaimRecordByCode(Expression<Func<sr_ClaimRecord, bool>> predicate);
    }
}

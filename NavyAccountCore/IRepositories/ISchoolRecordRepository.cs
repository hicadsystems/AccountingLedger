using NavyAccountCore.Core.IRepositories;
using NavyAccountCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NavyAccountCore.IRepositories
{
    public interface ISchoolRecordRepository:IRepository<sr_SchoolRecord>
    {
        Task<sr_SchoolRecord> GetSchoolByCode(Expression<Func<sr_SchoolRecord, bool>> predicate);
        Task<IEnumerable<sr_SchoolRecord>> GetAllSchool();
    }
}

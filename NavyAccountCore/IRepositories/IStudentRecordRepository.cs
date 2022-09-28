using NavyAccountCore.Core.IRepositories;
using NavyAccountCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NavyAccountCore.IRepositories
{
    public interface IStudentRecordRepository: IRepository<sr_StudentRecord>
    {
        Task<sr_StudentRecord> GetStudentByCode(Expression<Func<sr_StudentRecord, bool>> predicate);
        Task<IEnumerable<sr_StudentRecord>> GetAllStudent();
    }
}

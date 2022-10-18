using NavyAccountCore.Core.IRepositories;
using NavyAccountCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NavyAccountCore.IRepositories
{
    public interface ISchoolFeeRepository : IRepository<sr_SchoolFeeTB>
    {
        Task<sr_SchoolFeeTB> GetSchoolFeeByCode(Expression<Func<sr_SchoolFeeTB, bool>> predicate);
        Task<List<SchoolFeeVM>> GetAllSchoolFee();
        Task<List<SchoolFeeVM2>> GetAllSchoolFeeByPeriod(string period);
    }
}

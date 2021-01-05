using NavyAccountCore.Core.Entities;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NavyAccountCore.Core.IRepositories
{
    public interface IBalsheet:IRepository<npf_balsheet>
    {
        Task<npf_balsheet> GetBalanceSheetByCode(Expression<Func<npf_balsheet, bool>> predicate);
       
    }
}

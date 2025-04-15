using NavyAccountCore.Core.Entities;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NavyAccountCore.Core.IRepositories
{
    public interface IStock:IRepository<npf_Stocks>
    {
        Task<npf_Stocks> GetStocksByCode(Expression<Func<npf_Stocks, bool>> predicate);
       
    }
}

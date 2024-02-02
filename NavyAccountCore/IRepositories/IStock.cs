using NavyAccountCore.Core.Entities;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NavyAccountCore.Core.IRepositories
{
    public interface IStock:IRepository<npf_stock>
    {
        Task<npf_stock> GetStockByCode(Expression<Func<npf_stock, bool>> predicate);
       
    }
}

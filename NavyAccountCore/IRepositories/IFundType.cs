using NavyAccountCore.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NavyAccountCore.Core.IRepositories
{
    public interface IFundType : IRepository<Pf_fund>
    {
        Task<Pf_fund> GetFundTypeByCode(Expression<Func<Pf_fund, bool>> predicate);
        Pf_fund checkfundsByDesc(string desc);
       
    }
}

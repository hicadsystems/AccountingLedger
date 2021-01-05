using NavyAccountCore.Core.Entities;
using NavyAccountCore.Core.IRepositories;
using NavyAccountCore.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NavyAccountCore.Core.IRepositories
{
    public interface IPfundrate: IRepository<Pf_fundRate>
    {
        Task<List<fundtypeView>> GetFundList();
        Task<Pf_fundRate> GetFundRateTypeByCode(Expression<Func<Pf_fundRate, bool>> predicate);
    }
}

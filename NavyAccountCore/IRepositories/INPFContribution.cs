using NavyAccountCore.Core.Entities;
using NavyAccountCore.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NavyAccountCore.Core.IRepositories
{
    public interface INPFContribution:IRepository<npf_contributions>
    {
        Task<npf_contributions> GetContributionByCode(Expression<Func<npf_contributions, bool>> predicate);
   
        IEnumerable<contributionView> getAllNpf(string fund);
    }
}

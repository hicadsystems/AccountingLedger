using NavyAccountCore.Core.Entities;
using NavyAccountCore.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace NavyAccountCore.Core.IRepositories
{
    public interface IRank:IRepository<Rank>
    {
        Rank GetRankbyName(Expression<Func<Rank, bool>> predicate);
        List<Rank> getrankbytitle();
    }
}
using NavyAccountCore.Core.Entities;
using System;
using System.Linq.Expressions;

namespace NavyAccountCore.Core.IRepositories
{
    public interface IFundTypeCode : IRepository<npf_fundType>
    {
        npf_fundType Getfundtypebycode(Expression<Func<npf_fundType, bool>> predicate);
    }
}

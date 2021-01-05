using NavyAccountCore.Core.Entities;
using NavyAccountCore.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NavyAccountCore.Core.IRepositories
{
    public interface INavipRepo : IRepository<npf_NavipContribution>
    {
        Task<npf_NavipContribution> GetNavipByCode(Expression<Func<npf_NavipContribution, bool>> predicate);
        List<navipVM2> getnavipbybatch(string batch);
        List<navipVM4> getnavipbydate(DateTime startdate, DateTime enddate);
        List<navipVM5> getnavipbydate2(DateTime startdate, DateTime enddate);
    }
}

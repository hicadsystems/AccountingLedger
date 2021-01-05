using NavyAccountCore.Core.Entities;
using NavyAccountCore.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NavyAccountCore.Core.IRepositories
{
    public interface ILoanPerRank : IRepository<loanPerRank>
    {
        Task<loanPerRank> GetLoanByCode(Expression<Func<loanPerRank, bool>> predicate);
        IEnumerable<LoanReportView> getAllNpf(string loan);
    }
}

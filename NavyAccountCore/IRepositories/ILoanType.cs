using NavyAccountCore.Core.Entities;
using NavyAccountCore.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NavyAccountCore.Core.IRepositories
{
    public interface ILoanType: IRepository<Pf_loanType>
    {
        Pf_loanType GetLoanTypeByCode(Expression<Func<Pf_loanType, bool>> predicate);
        Task<List<LoanTypeView>> GetLoanTypeDesc();
      
    }
}

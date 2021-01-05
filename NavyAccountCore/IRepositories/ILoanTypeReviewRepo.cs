using NavyAccountCore.Entities;
using NavyAccountCore.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NavyAccountCore.Core.IRepositories
{
    public interface ILoanTypeReviewRepo : IRepository<npf_LoanTypeReview>
    {
        npf_LoanTypeReview GetLoanTypeByCode(Expression<Func<npf_LoanTypeReview, bool>> predicate);
        Task<List<loanreview>> GetLoanTypeDesc();
    }
}

using NavyAccountCore.Entities;
using NavyAccountCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.IServices
{
    public interface ILoantypeReviewService
    {
        IEnumerable<npf_LoanTypeReview> GetLoanTypes();
        Task<npf_LoanTypeReview> GetLoanTypeById(int id);
        Task<List<loanreview>> GetLoanTypeDesc();
        npf_LoanTypeReview GetLoanTypeByCode(int id);
        Task<bool> AddLoanType(npf_LoanTypeReview Pf_loanType);
        Task<bool> UpdateLoanType(npf_LoanTypeReview Pf_loanType);
        void RemoveLoanType(npf_LoanTypeReview pf_LoanType);
    }
}

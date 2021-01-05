using System;
using System.Collections.Generic;
using System.Linq;
using NavyAccountCore.Core.Entities;
using System.Threading.Tasks;
using NavyAccountCore.Models;

namespace NavyAccountWeb.IServices
{ public interface ILoanTypeService
    {
        IEnumerable<Pf_loanType> GetLoanTypes();
        Task<Pf_loanType> GetLoanTypeById(int id);
        Task<List<LoanTypeView>> GetLoanTypeDesc();
        Pf_loanType GetLoanTypeByCode(string loantypecode);
        Task<bool> AddLoanType(Pf_loanType Pf_loanType);
        Task<bool> UpdateLoanType(Pf_loanType Pf_loanType);
        void RemoveLoanType(Pf_loanType pf_LoanType);
    }
}
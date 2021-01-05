using NavyAccountCore.Core.Entities;
using NavyAccountCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.IServices
{
    public interface ILoanPerRankService
    {
        IEnumerable<loanPerRank> GetLoanTypes();
        IEnumerable<Pf_loanType> GetLoan();
        IEnumerable<LoanReportView> GetAll(string code);
        IEnumerable<loanPerRank> GetAllLoanType();
        Task<loanPerRank> GetLoanPerRankByCode(string code);
        Task<loanPerRank> GetLoanPerRankById(int id);
        Task<bool> AddContribution(loanPerRank contribution);
        Task<bool> UpdateContribution(loanPerRank contribution);
    }
}

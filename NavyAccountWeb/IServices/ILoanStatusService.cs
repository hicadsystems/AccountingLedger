using System;
using System.Collections.Generic;
using System.Linq;
using NavyAccountCore.Core.Entities;
using System.Threading.Tasks;
using NavyAccountCore.Models;

namespace NavyAccountWeb.IServices
{
    public interface ILoanStatusService
    {
        IEnumerable<LoanStatus> GetLoanStatus();
        Task<List<LoanStatusVM>> GetLoanStatus2(int id);
        Task<bool> AddLoanStatus(LoanStatus lstatus);
    }
}

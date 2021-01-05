using NavyAccountCore.Core.Entities;
using NavyAccountCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.IServices
{
    public interface IContributionServices
    {
        IEnumerable<Pf_fund> GetFundTypes();
        IEnumerable<Pf_loanType> GetLoan();
        IEnumerable<contributionView> GetAll(string code);
        IEnumerable<npf_contributions> GetAllNPFContributions();
        Task<npf_contributions> GetContributionByCode(string fundtypecode);
        Task<npf_contributions> GetContributionById(int id);
        Task<bool> AddContribution(npf_contributions contribution);
        Task<bool> UpdateContribution(npf_contributions contribution);
    }
}

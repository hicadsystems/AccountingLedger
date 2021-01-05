using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Entities;
using NavyAccountCore.Models;
using NavyAccountWeb.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.Services
{
    public class ContributionServices : IContributionServices
    {

        private readonly IUnitOfWork unitOfWork;

        public ContributionServices(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<bool> AddContribution(npf_contributions contribution)
        {
            unitOfWork.contribution.Create(contribution);
            return await unitOfWork.Done();
        }

        public IEnumerable<contributionView> GetAll(string code)
        {
            return unitOfWork.contribution.getAllNpf(code);
        }

        public IEnumerable<npf_contributions> GetAllNPFContributions()
        {
            return unitOfWork.contribution.All();
        }

        public Task<npf_contributions> GetContributionByCode(string fundtypecode)
        {
            return unitOfWork.contribution.GetContributionByCode(x => x.fundtype == fundtypecode );
        }

        public Task<npf_contributions> GetContributionById(int id)
        {
            return unitOfWork.contribution.Find(id);
        }

        public IEnumerable<Pf_fund> GetFundTypes()
        {
            return unitOfWork.FundType.All();
        }

        public IEnumerable<Pf_loanType> GetLoan()
        {
            return unitOfWork.loanType.All();
        }

        public async Task<bool> UpdateContribution(npf_contributions contribution)
        {
            unitOfWork.contribution.Update(contribution);
            return await unitOfWork.Done();
        }
    }
}

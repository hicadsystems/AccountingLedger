using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Entities;
using NavyAccountCore.Models;
using NavyAccountWeb.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NavyAccountWeb.Services
{
    public class LoanPerRankService : ILoanPerRankService
    {
        private readonly IUnitOfWork unitOfWork;

        public LoanPerRankService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<bool> AddContribution(loanPerRank contribution)
        {
            unitOfWork.loanPerRank.Create(contribution);
            return await unitOfWork.Done();

        }

        public IEnumerable<LoanReportView> GetAll(string code)
        {
            return unitOfWork.loanPerRank.getAllNpf(code);
        }

        public IEnumerable<loanPerRank> GetAllLoanType()
        {
            return unitOfWork.loanPerRank.All();
        }

        public IEnumerable<Pf_loanType> GetLoan()
        {
            return unitOfWork.loanType.All();
        }

        public Task<loanPerRank> GetLoanPerRankByCode(string code)
        {
            return unitOfWork.loanPerRank.GetLoanByCode(x => x.loantype == code);
        }

        public Task<loanPerRank> GetLoanPerRankById(int id)
        {
            return unitOfWork.loanPerRank.Find(id);
        }

        public IEnumerable<loanPerRank> GetLoanTypes()
        {
            return unitOfWork.loanPerRank.All();
        }

        public async Task<bool> UpdateContribution(loanPerRank contribution)
        {
            unitOfWork.loanPerRank.Update(contribution);
            return await unitOfWork.Done();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Entities;
using NavyAccountCore.Models;
using NavyAccountWeb.IServices;

namespace NavyAccountWeb.Services
{
    public class LoanStatusService : ILoanStatusService
    {
        private readonly IUnitOfWork unitOfWork;
        public LoanStatusService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;

        }
        
        public async Task<bool> AddLoanStatus(LoanStatus lstatus)
        {
            unitOfWork.loanStatus.Create(lstatus);
            return await unitOfWork.Done(); 
        }

        public IEnumerable<LoanStatus> GetLoanStatus()
        {
            return unitOfWork.loanStatus.All();
        }
        public Task<List<LoanStatusVM>> GetLoanStatus2(int id)
        {
            return unitOfWork.loanStatus.GetloanStatus2(id);
        }


    }
}

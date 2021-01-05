using NavyAccountCore.Core.Data;
using NavyAccountCore.Entities;
using NavyAccountCore.Models;
using NavyAccountWeb.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.Services
{
    public class LoantypeReviewService: ILoantypeReviewService
    {
        private readonly IUnitOfWork unitOfWork;
        public LoantypeReviewService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;

        }
        public npf_LoanTypeReview GetLoanTypeByCode(int id)
        {
            return unitOfWork.loantypereview.GetLoanTypeByCode(x => x.Id == id);

        }
        public Task<List<loanreview>> GetLoanTypeDesc()
        {
            return unitOfWork.loantypereview.GetLoanTypeDesc();
        }
        public async Task<bool> AddLoanType(npf_LoanTypeReview loantype)
        {
            unitOfWork.loantypereview.Create(loantype);
            return await unitOfWork.Done();
        }

        public IEnumerable<npf_LoanTypeReview> GetLoanTypes()
        {
            return unitOfWork.loantypereview.All();
        }

        public Task<npf_LoanTypeReview> GetLoanTypeById(int id)
        {
            return unitOfWork.loantypereview.Find(id);
        }

        public async Task<bool> UpdateLoanType(npf_LoanTypeReview Pf_loanType)
        {
            unitOfWork.loantypereview.Update(Pf_loanType);
            return await unitOfWork.Done();
        }

        public void RemoveLoanType(npf_LoanTypeReview pf_LoanType)
        {
            unitOfWork.loantypereview.Remove(pf_LoanType);
            unitOfWork.Done();
        }
    }
}

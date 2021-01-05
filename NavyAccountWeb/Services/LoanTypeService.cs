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
    public class LoanTypeService: ILoanTypeService
    {
        private readonly IUnitOfWork unitOfWork;
        public LoanTypeService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;

        }
        public Pf_loanType GetLoanTypeByCode(string loantypecode)
        {
            return unitOfWork.loanType.GetLoanTypeByCode(x => x.Code == loantypecode);

        }
        public Task<List<LoanTypeView>> GetLoanTypeDesc()
        {
            return unitOfWork.loanType.GetLoanTypeDesc();
        }
        public async Task<bool> AddLoanType(Pf_loanType loantype)
        {
            unitOfWork.loanType.Create(loantype);
            return await unitOfWork.Done();
        }

        public IEnumerable<Pf_loanType> GetLoanTypes()
        {
            return unitOfWork.loanType.All();
        }

        public Task<Pf_loanType> GetLoanTypeById(int id)
        {
            return unitOfWork.loanType.Find(id);
        }

        public async Task<bool> UpdateLoanType(Pf_loanType Pf_loanType)
        {
            unitOfWork.loanType.Update(Pf_loanType);
            return await unitOfWork.Done();
        }

        public void RemoveLoanType(Pf_loanType pf_LoanType)
        {
            unitOfWork.loanType.Remove(pf_LoanType);
            unitOfWork.Done();
        }
    }
}


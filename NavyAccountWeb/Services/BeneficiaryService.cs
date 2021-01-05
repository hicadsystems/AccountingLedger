using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Entities;
using NavyAccountWeb.IServices;

namespace NavyAccountWeb.Services
{
    public class BeneficiaryService : IBeneficiaryService
    {
        private readonly IUnitOfWork unitOfWork;
        public BeneficiaryService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;

        }
        public async Task<bool> AddBeneficiary(Beneficiary beneficiary)
        {
            unitOfWork.beneficiary.Create(beneficiary);
            return await unitOfWork.Done();
        }

        public async Task<bool> UpdateBeneficiary(Beneficiary beneficiary)
        {
            unitOfWork.beneficiary.Update(beneficiary);
            return await unitOfWork.Done();
        }

        public async Task<List<Beneficiary>> GetBeneficiaryByPersonID(int personid)
        {
            return await unitOfWork.beneficiary.GetBeneficiaryByPersonID(personid);
        }

        public async Task<Beneficiary> GetBeneficiaryByID(int beneid)
        {
            return await unitOfWork.beneficiary.GetBeneficiaryByID(beneid);
        }


    }
}

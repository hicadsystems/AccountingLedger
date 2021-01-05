using System;
using System.Collections.Generic;
using System.Linq;
using NavyAccountCore.Core.Entities;
using System.Threading.Tasks;

namespace NavyAccountWeb.IServices
{
    public interface IBeneficiaryService
    {
        Task<bool> AddBeneficiary(Beneficiary beneficiary);
        Task<bool> UpdateBeneficiary(Beneficiary beneficiary);
        Task<List<Beneficiary>> GetBeneficiaryByPersonID(int personid);
        Task<Beneficiary> GetBeneficiaryByID(int beneid);
    }
}

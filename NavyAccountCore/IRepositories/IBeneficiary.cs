

using NavyAccountCore.Core.Entities;
using NavyAccountCore.Core.IRepositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NavyAccountCore.Core.IRepositories
{
        public interface IBeneficiary : IRepository<Beneficiary>
        {
        Task<List<Beneficiary>> GetBeneficiaryByPersonID(int personID);
        Task<Beneficiary> GetBeneficiaryByID(int beneID);
        }
}

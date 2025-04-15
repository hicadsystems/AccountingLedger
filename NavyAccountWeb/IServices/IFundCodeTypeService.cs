using System;
using System.Collections.Generic;
using System.Linq;
using NavyAccountCore.Core.Entities;
using System.Threading.Tasks;

namespace NavyAccountWeb.IServices
{
    public interface IFundTypeCodeService
    {
        IEnumerable<npf_fundType> GetFundTypes();

        npf_fundType GetFundTypeCodeByCode(string fundtypecode);


        Task<npf_fundType> GetFundTypeByID(int fundtypeid);
        Task<bool> AddFundTypeCode(npf_fundType pf_Fund);
        Task<bool> UpdateFundType(npf_fundType pf_Fund);
        int GetCurrentYear();

    }
}

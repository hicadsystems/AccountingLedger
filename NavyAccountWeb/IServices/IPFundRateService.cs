using NavyAccountCore.Core.Entities;
using NavyAccountCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.IServices
{
    public interface IPFundRateService
    {
        IEnumerable<Pf_fundRate> GetFundRateTypes();
        Task<Pf_fundRate> GetFundRateTypeById(int id);
        Task<Pf_fundRate> GetFundRateTypeByCode(string fundtypecode);
        Task<bool> AddFundType(Pf_fundRate pf_Fundrate);
        Task<bool> UpdateFundType(Pf_fundRate pf_Fundrate);
        Task<List<fundtypeView>> GetFundList();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using NavyAccountCore.Core.Entities;
using System.Threading.Tasks;

namespace NavyAccountWeb.IServices
{
    public interface IFundTypeService
    {
        IEnumerable<Pf_fund> GetFundTypes();
        Task<Pf_fund> GetFundTypeById(int id);
        Task<Pf_fund> GetFundTypeByCode(string fundtypecode);
        Task<Pf_fund> GetFundTypeByID(int fundtypeid);
        Task<bool> AddFundType(Pf_fund pf_Fund);
        Task<bool> UpdateFundType(Pf_fund pf_Fund);
        void RemoveFundType(Pf_fund pf_Fund);
        Pf_fund checkfundsByDesc(string desc);
    }
}

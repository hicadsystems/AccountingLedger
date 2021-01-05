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
    public class pFundRateService : IPFundRateService
    {
        private readonly IUnitOfWork unitOfWork;
        public pFundRateService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<bool> AddFundType(Pf_fundRate pf_Fundrate)
        {
            unitOfWork.pfundrate.Create(pf_Fundrate);
            return await unitOfWork.Done();
        }

        public Task<List<fundtypeView>> GetFundList()
        {
            return unitOfWork.pfundrate.GetFundList();
        }

        public Task<Pf_fundRate> GetFundRateTypeByCode(string fundtypecode)
        {
            return unitOfWork.pfundrate.GetFundRateTypeByCode(x => x.FundCode == fundtypecode);
        }

        public Task<Pf_fundRate> GetFundRateTypeById(int id)
        {
            return unitOfWork.pfundrate.Find(id);
        }

        public IEnumerable<Pf_fundRate> GetFundRateTypes()
        {
            return unitOfWork.pfundrate.All();
        }

        public async Task<bool> UpdateFundType(Pf_fundRate pf_Fundrate)
        {
            unitOfWork.pfundrate.Update(pf_Fundrate);
            return  await unitOfWork.Done();
        }
    }
}

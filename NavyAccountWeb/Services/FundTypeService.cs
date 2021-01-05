using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Entities;
using NavyAccountWeb.IServices;

namespace NavyAccountWeb.Services
{
    public class FundTypeService : IFundTypeService
    {
        private readonly IUnitOfWork unitOfWork;
        public FundTypeService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;

        }
        public Task<Pf_fund> GetFundTypeByCode(string fundtypecode)
        {
            return unitOfWork.FundType.GetFundTypeByCode(x => x.Code == fundtypecode);

        }

        public Task<Pf_fund> GetFundTypeByID(int fundtypeid)
        {
            return unitOfWork.FundType.GetFundTypeByCode(x => x.Id == fundtypeid);

        }
        public async Task<bool> AddFundType(Pf_fund fundtype)
        {
            unitOfWork.FundType.Create(fundtype);
            return await unitOfWork.Done();
        }

        public IEnumerable<Pf_fund> GetFundTypes()
        {
            return unitOfWork.FundType.All();
        }

        public Task<Pf_fund> GetFundTypeById(int id)
        {
            return unitOfWork.FundType.Find(id);
        }

        public async Task<bool> UpdateFundType(Pf_fund pf_Fund)
        {
            unitOfWork.FundType.Update(pf_Fund);
            return await unitOfWork.Done();
        }

        public void RemoveFundType(Pf_fund pf_Fund)
        {
            unitOfWork.FundType.Remove(pf_Fund);
            unitOfWork.Done();
        }

        public Pf_fund checkfundsByDesc(string desc)
        {
            return unitOfWork.FundType.checkfundsByDesc(desc);
        }
    }
}

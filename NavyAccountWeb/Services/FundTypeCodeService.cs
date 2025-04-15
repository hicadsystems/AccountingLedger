using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Entities;
using NavyAccountWeb.IServices;

namespace NavyAccountWeb.Services
{
    public class FundTypeCodeService : IFundTypeCodeService
    {
        private readonly IUnitOfWork unitOfWork;
        public FundTypeCodeService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;

        }

        public async Task<bool> AddFundTypeCode(npf_fundType fundtype)
        {
            unitOfWork.fundTypeCode.Create(fundtype);
            return await unitOfWork.Done();
        }

        public Task<npf_fundType> GetFundTypeByID(int fundtypeid)
        {
            return unitOfWork.fundTypeCode.Find(fundtypeid);
        }

        public npf_fundType GetFundTypeCodeByCode(string fundtypecode)
        {
            return unitOfWork.fundTypeCode.Getfundtypebycode(x => x.Code == fundtypecode);

        }
        public int GetCurrentYear()
        {
           var fun= unitOfWork.fundTypeCode.Getfundtypebycode(x=>x.Code=="10").processingYear;
           return fun;
        }
        public IEnumerable<npf_fundType> GetFundTypes()
        {
            return unitOfWork.fundTypeCode.All();
        }

        public async Task<bool> UpdateFundType(npf_fundType pf_Fund)
        {
            unitOfWork.fundTypeCode.Update(pf_Fund);
            return await unitOfWork.Done();
        }
    }
}

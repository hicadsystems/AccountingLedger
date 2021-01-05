using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Entities;
using NavyAccountWeb.IServices;

namespace NavyAccountWeb.Services
{
    public class SubTypeService : ISubTypeService
    {
        private readonly IUnitOfWork unitOfWork;
        public SubTypeService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;

        }
        
        public async Task<bool> AddSubType(sub_type subtype)
        {
            unitOfWork.subtype.Create(subtype);
            return await unitOfWork.Done();
        }

        public IEnumerable<sub_type> GetSubTypes()
        {
            return unitOfWork.subtype.All();
        }


        public Task<sub_type> GetSubTypeById(int id)
        {
            return unitOfWork.subtype.Find(id);
        }

        public async Task<bool> UpdateSubType(sub_type subtype)
        {
            unitOfWork.subtype.Update(subtype);
            return await unitOfWork.Done();
        }
    }
}

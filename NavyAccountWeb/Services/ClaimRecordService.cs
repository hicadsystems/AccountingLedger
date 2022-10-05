using NavyAccountCore.Core.Data;
using NavyAccountCore.Entities;
using NavyAccountWeb.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.Services
{
    public class ClaimRecordService:IClaimRecordService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ClaimRecordService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddClaimRecord(sr_ClaimRecord value)
        {
            _unitOfWork.schclaim.Create(value);
            return await _unitOfWork.Done();
        }

        public void DeleteClaimRecord(sr_ClaimRecord value)
        {
            _unitOfWork.schclaim.Remove(value);
             _unitOfWork.Done();
        }

        public Task<IEnumerable<sr_ClaimRecord>> GetAllClaim()
        {
            throw new NotImplementedException();
        }

        public async Task<sr_ClaimRecord> GetClaimByid(int id)
        {
            return await _unitOfWork.schclaim.GetClaimRecordByCode(x => x.id == id);
        }

        public async Task<sr_ClaimRecord> GetClaimRecordByCode(string code)
        {
            return await _unitOfWork.schclaim.GetClaimRecordByCode(x => x.Reg_Number == code);
        }

        public async Task<bool> UpdateClaimRecord(sr_ClaimRecord value)
        {
            _unitOfWork.schclaim.Update(value);
            return await _unitOfWork.Done();
        }
        public decimal GetAmountPerSchoolType(string studentNo, out decimal amt)
        {
            return _unitOfWork.schclaim.GetAmountPerSchoolType(studentNo,out amt);
        }
    }
}

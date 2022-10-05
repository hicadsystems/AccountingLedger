using NavyAccountCore.Core.Data;
using NavyAccountCore.Entities;
using NavyAccountWeb.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.Services
{
    public class GuardianRecordService: IGuardianRecordService
    {
        private readonly IUnitOfWork _unitOfWork;
        public GuardianRecordService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddGuardian(sr_GuardianRecord value)
        {
            _unitOfWork.Guardian.Create(value);
            return await _unitOfWork.Done();
        }

        public void DeleteGuardian(sr_GuardianRecord value)
        {
            _unitOfWork.Guardian.Remove(value);
            _unitOfWork.Done();
        }
        public async Task<List<sr_GuardianRecord>> GetGuardianList(int iDisplayStart, int iDisplayLength)
        {
            return await _unitOfWork.Guardian.getGuardianList(iDisplayStart, iDisplayLength);
        }

        public async Task<int> getGuardianListCount()
        {
            return await _unitOfWork.Guardian.getGuardianListCount();
        }
        public async Task<List<sr_GuardianRecord>> GetGuardianListByName(string Guardianname)
        {
            return await _unitOfWork.Guardian.getGuardianListByName(Guardianname);
        }
        public async Task<IEnumerable<sr_GuardianRecord>> GetAllGuardian()
        {
            return await _unitOfWork.Guardian.getAllGuardian();
        }

        public async Task<sr_GuardianRecord> GetGuardianByCode(string code)
        {
            return await _unitOfWork.Guardian.GetGuardianByCode(x => x.Reg_Number == code);
        }

        public async Task<sr_GuardianRecord> GetGuardianByid(int id)
        {
            return await _unitOfWork.Guardian.GetGuardianByCode(x => x.id == id);
        }

        public async Task<bool> UpdateGuardian(sr_GuardianRecord value)
        {
            _unitOfWork.Guardian.Update(value);
            return await _unitOfWork.Done();
        }
    }
}

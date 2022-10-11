using NavyAccountCore.Core.Data;
using NavyAccountCore.Entities;
using NavyAccountWeb.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.Services
{
    public class ParentRecordService: IParentRecordService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ParentRecordService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddParent(sr_ParentRecord value)
        {
            _unitOfWork.parent.Create(value);
            return await _unitOfWork.Done();
        }

        public void DeleteParent(sr_ParentRecord value)
        {
            _unitOfWork.parent.Remove(value);
            _unitOfWork.Done();
        }
        public async Task<List<sr_ParentRecord>> GetParentList(int iDisplayStart, int iDisplayLength)
        {
            return await _unitOfWork.parent.getParentList(iDisplayStart, iDisplayLength);
        }

        public async Task<int> getParentListCount()
        {
            return await _unitOfWork.parent.getParentListCount();
        }
        public async Task<List<sr_ParentRecord>> GetParentListByName(string parentname)
        {
            return await _unitOfWork.parent.getParentListByName(parentname);
        }
        public async Task<IEnumerable<sr_ParentRecord>> GetAllParent()
        {
            return await _unitOfWork.parent.getAllParent();
        }

        public async Task<sr_ParentRecord> GetParentByCode(string code)
        {
            return await _unitOfWork.parent.GetParentByCode(x => x.Reg_Number == code);
        }

        public async Task<sr_ParentRecord> GetParentByid(int id)
        {
            return await _unitOfWork.parent.GetParentByCode(x => x.id == id);
        }

        public async Task<bool> UpdateParent(sr_ParentRecord value)
        {
            _unitOfWork.parent.Update(value);
            return await _unitOfWork.Done();
        }
    }
}

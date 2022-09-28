using NavyAccountCore.Core.Data;
using NavyAccountCore.Entities;
using NavyAccountWeb.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.Services
{
    public class SchoolRecordService:ISchoolRecordService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SchoolRecordService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;  
        }
        public async Task<bool> AddSchool(sr_SchoolRecord value)
        {
            _unitOfWork.school.Create(value);
           return await _unitOfWork.Done();
        }
        public async Task<bool> UpdateSchool(sr_SchoolRecord value)
        {
             _unitOfWork.school.Update(value);
             return await _unitOfWork.Done();
        }
        public async Task<sr_SchoolRecord> GetAllSchoolByCode(string code)
        {
            return await _unitOfWork.school.GetSchoolByCode(x => x.SchoolCode == code);
        }
        public async Task<IEnumerable<sr_SchoolRecord>> GetAllSchool()
        {
            return await _unitOfWork.school.GetAllSchool(); 
        }
        public void DeleteSchool(sr_SchoolRecord value)
        {
            _unitOfWork.school.Remove(value);
            _unitOfWork.Done();
        }

        public Task<sr_SchoolRecord> GetSchoolByid(int id)
        {
            return _unitOfWork.school.GetSchoolByCode(x=>x.id==id);
        }
    }
}

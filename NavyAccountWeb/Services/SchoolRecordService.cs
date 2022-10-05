using Dapper;
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
        private readonly IDapper dapper;
        public SchoolRecordService(IUnitOfWork unitOfWork, IDapper dapper)
        {
            _unitOfWork = unitOfWork;
            this.dapper = dapper;
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

        public async Task<List<sr_SchoolRecord>> GetSchoolByName(string schoolName)
        {
            var result = new List<sr_SchoolRecord>();
            var param = new DynamicParameters();

            param.Add("@schoolName", schoolName);
            result = dapper.GetAll<sr_SchoolRecord>("sr_GetSchoolByName", param, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }
    }
}

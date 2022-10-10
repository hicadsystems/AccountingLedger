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
    public class SchoolFeeService:ISchoolFeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDapper dapper;
        public SchoolFeeService(IUnitOfWork unitOfWork, IDapper dapper)
        {
            _unitOfWork = unitOfWork;
            this.dapper = dapper;
        }
        public async Task<bool> AddSchoolFee(sr_SchoolFeeTB value)
        {
            _unitOfWork.schoolFee.Create(value);
           return await _unitOfWork.Done();
        }
        public async Task<bool> UpdateSchoolFee(sr_SchoolFeeTB value)
        {
             _unitOfWork.schoolFee.Update(value);
             return await _unitOfWork.Done();
        }
        public async Task<sr_SchoolFeeTB> GetAllSchoolFeeByCode(string code)
        {
            return await _unitOfWork.schoolFee.GetSchoolFeeByCode(x => x.Period == code);
        }
        public async Task<List<SchoolFeeVM>> GetAllSchoolFee()
        {
            return await _unitOfWork.schoolFee.GetAllSchoolFee(); 
        }
        public void DeleteSchoolFee(sr_SchoolFeeTB value)
        {
            _unitOfWork.schoolFee.Remove(value);
            _unitOfWork.Done();
        }

        public Task<sr_SchoolFeeTB> GetSchoolFeeByid(int id)
        {
            return _unitOfWork.schoolFee.GetSchoolFeeByCode(x=>x.id==id);
        }

        public async Task<List<sr_SchoolFeeTB>> GetSchoolFeeByName(string SchoolFeeName)
        {
            var result = new List<sr_SchoolFeeTB>();
            var param = new DynamicParameters();

            param.Add("@SchoolFeeName", SchoolFeeName);
            result = dapper.GetAll<sr_SchoolFeeTB>("sr_GetSchoolFeeByName", param, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }
    }
}

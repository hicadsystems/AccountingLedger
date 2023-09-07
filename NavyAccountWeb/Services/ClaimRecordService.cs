using Dapper;
using NavyAccountCore.Core.Data;
using NavyAccountCore.Entities;
using NavyAccountWeb.IServices;
using NavyAccountWeb.Models;
using NavyAccountWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.Services
{
    public class ClaimRecordService:IClaimRecordService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDapper dapper;
        public ClaimRecordService(IUnitOfWork unitOfWork, IDapper dapper)
        {
            _unitOfWork = unitOfWork;
            this.dapper = dapper;
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
        public async Task<List<StudentClaimViewModel>> GetStudentSummary(int id )
        {
            var result = new List<StudentClaimViewModel>();
            var param = new DynamicParameters();
            param.Add("@id", id);
            result = dapper.GetAll<StudentClaimViewModel>("sr_GetClaimSummary", param, commandType: System.Data.CommandType.StoredProcedure);

            return result;
        }
        
        public async Task<List<ClaimPaymentReport>> GetStudentCurrentClaim()
        {
            var result = new List<ClaimPaymentReport>();
            var param = new DynamicParameters();
            result = dapper.GetAll<ClaimPaymentReport>("getproposeclaim", param, commandType: System.Data.CommandType.StoredProcedure);

            return result;
        }
        public async Task<List<ClaimPaymentReport>> GetStudentClaim()
        {
            var result = new List<ClaimPaymentReport>();
            var param = new DynamicParameters();
            result = dapper.GetAll<ClaimPaymentReport>("sr_GetClaimRecord", param, commandType: System.Data.CommandType.StoredProcedure);

            return result;
        }
        public async Task<List<ClaimReport>> GetStudentReportClaim()
        {
            var result = new List<ClaimReport>();
            var param = new DynamicParameters();
            result = dapper.GetAll<ClaimReport>("sr_GetClaimReport", param, commandType: System.Data.CommandType.StoredProcedure);
            
            return result;
        }

        public async Task<List<ClaimPaymentReport>> GetStudentClaimBySchool(string schoolname)
        {
            var result = new List<ClaimPaymentReport>();
            var param = new DynamicParameters();
            param.Add("@school", schoolname);

            result = dapper.GetAll<ClaimPaymentReport>("sr_GetClaimRecordBySchool", param, commandType: System.Data.CommandType.StoredProcedure);
            result = result.Where(x => x.Schoolname == schoolname).ToList();
            return result;
        }
        public async Task<int> UpdateAllLedger()
        {
            var param = new DynamicParameters();
            var result = dapper.Get<int>("sr_UpdateClaimLedger", param, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }
        public async Task<int> UpdateStudentClaimLedgerBySchool(string schoolname,string VoucherNumber,string user)
        {
            int result = 0;
            var param = new DynamicParameters();
            param.Add("@schoolname", schoolname);
            param.Add("@globaluser", user);
            param.Add("@refno", VoucherNumber);
            dapper.Execute("sr_UpdateClaimLedger", param, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }
        public async Task<int> UpdateClaimReinBySchool(string reg_num, string school, string classs, decimal amount, string remark, string user)
        {
           
            int result = 0;
            var param = new DynamicParameters();
            param.Add("@reg_num", reg_num);
            param.Add("@school", school);
            param.Add("@classs", classs);
            param.Add("@amount", amount);
            param.Add("@refno", remark);
            param.Add("@globaluser", user);
            dapper.Execute("sr_UpdateClaimRein", param, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

    }
}

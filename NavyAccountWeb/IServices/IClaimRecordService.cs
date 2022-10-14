using NavyAccountCore.Entities;
using NavyAccountWeb.Models;
using NavyAccountWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.IServices
{
   public interface IClaimRecordService
    {
        Task<List<StudentClaimViewModel>> GetStudentSummary(int id);
        Task<List<ClaimReport>> GetStudentReportClaim();
        Task<List<ClaimPaymentReport>> GetStudentClaim();
        Task<List<ClaimPaymentReport>> GetStudentClaimBySchool(string schoolname);
        Task<int> UpdateAllLedger();
        Task<int> UpdateStudentClaimLedgerBySchool(string schoolname, string VoucherNumber, string user);

        Task<bool> AddClaimRecord(sr_ClaimRecord value);
        Task<bool> UpdateClaimRecord(sr_ClaimRecord value);
        void DeleteClaimRecord(sr_ClaimRecord value);
        Task<IEnumerable<sr_ClaimRecord>> GetAllClaim();
        Task<sr_ClaimRecord> GetClaimRecordByCode(string code);
        Task<sr_ClaimRecord> GetClaimByid(int id);
        decimal GetAmountPerSchoolType(string studentNo, out decimal amt);


    }
}

using NavyAccountCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.IServices
{
   public interface IClaimRecordService
    {
        Task<bool> AddClaimRecord(sr_ClaimRecord value);
        Task<bool> UpdateClaimRecord(sr_ClaimRecord value);
        void DeleteClaimRecord(sr_ClaimRecord value);
        Task<IEnumerable<sr_ClaimRecord>> GetAllClaim();
        Task<sr_ClaimRecord> GetClaimRecordByCode(string code);
        Task<sr_ClaimRecord> GetClaimByid(int id);
        decimal GetAmountPerSchoolType(string studentNo, out decimal amt);


    }
}

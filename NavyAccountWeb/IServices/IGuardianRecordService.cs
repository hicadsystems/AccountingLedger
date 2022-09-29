using NavyAccountCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.IServices
{
    public interface IGuardianRecordService
    {
        Task<bool> AddGuardian(sr_GuardianRecord value);
        Task<bool> UpdateGuardian(sr_GuardianRecord value);
        void DeleteGuardian(sr_GuardianRecord value);
        Task<IEnumerable<sr_GuardianRecord>> GetAllGuardian();
        Task<sr_GuardianRecord> GetGuardianByCode(string code);
        Task<sr_GuardianRecord> GetGuardianByid(int id);
        Task<List<sr_GuardianRecord>> GetGuardianList(int iDisplayStart, int iDisplayLength);
        Task<int> getGuardianListCount();
        Task<List<sr_GuardianRecord>> GetGuardianListByName(string Guardianname);
    }
}

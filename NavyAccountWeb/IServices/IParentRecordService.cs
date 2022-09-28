using NavyAccountCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.IServices
{
    public interface IParentRecordService
    {
        Task<bool> AddParent(sr_ParentRecord value);
        Task<bool> UpdateParent(sr_ParentRecord value);
        void DeleteParent(sr_ParentRecord value);
        Task<IEnumerable<sr_ParentRecord>> GetAllParent();
        Task<sr_ParentRecord> GetParentByCode(string code);
        Task<sr_ParentRecord> GetParentByid(int id);
        
    }
}

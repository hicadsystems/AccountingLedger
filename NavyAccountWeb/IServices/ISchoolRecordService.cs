using NavyAccountCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.IServices
{
    public interface ISchoolRecordService
    {
        Task<bool> AddSchool(sr_SchoolRecord value);
        Task<bool> UpdateSchool(sr_SchoolRecord value);
        Task<sr_SchoolRecord> GetAllSchoolByCode(string code);
        Task<List<sr_SchoolRecord>> GetSchoolByName(string schoolName);
        Task<List<sr_SchoolRecord>> GetSchoolByClass(int classid);
        Task<IEnumerable<sr_SchoolRecord>> GetAllSchool();
        Task<sr_SchoolRecord> GetSchoolByid(int id);
        void DeleteSchool(sr_SchoolRecord value);
    }
}

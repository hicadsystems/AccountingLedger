using NavyAccountCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.IServices
{
    public interface IStudentRecordService
    {
        Task<bool> AddStudent(sr_StudentRecord value);
        Task<bool> UpdateStudent(sr_StudentRecord value);
        void DeleteStudent(sr_StudentRecord value);
        Task<IEnumerable<sr_StudentRecord>> GetAllStudent();
        Task<sr_StudentRecord> GetStudentByCode(string code);
        Task<sr_StudentRecord> GetStudentByid(int id);
    }
}

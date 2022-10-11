using NavyAccountCore.Core.IRepositories;
using NavyAccountCore.Entities;
using NavyAccountCore.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NavyAccountCore.IRepositories
{
    public interface IStudentRecordRepository: IRepository<sr_StudentRecord>
    {
        Task<sr_StudentRecord> GetStudentByCode(Expression<Func<sr_StudentRecord, bool>> predicate);
        Task<IEnumerable<sr_StudentRecord>> GetAllStudent();
        Task<List<StudentRecordVM>> getStudentList(int iDisplayStart, int iDisplayLength);
        Task<List<sr_StudentRecord>> getStudentListByName(string Studentname);
        Task<List<sr_StudentRecord>> getOldStudentListByName(string Studentname);
        Task<int> getStudentListCount();
        Task<int> getInactiveStudentListCount();
        Task<List<StudentRecordVM>> GetInactiveStudentList(int iDisplayStart, int iDisplayLength);
        Task<sr_StudentRecord> getStudentById(int id);
        StudentRecordVM getStudentListByID(int id);
        StudentRecordVM getOldStudentListByID(int id);
        Task<List<StudentRecordVM>> GetStudentListOnCliam();
    }
}

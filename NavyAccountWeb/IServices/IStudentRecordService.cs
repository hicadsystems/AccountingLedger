using NavyAccountCore.Entities;
using NavyAccountCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static NavyAccountWeb.Models.SchoolFilterModels;

namespace NavyAccountWeb.IServices
{
    public interface IStudentRecordService
    {
        Task<List<StudentReport>> GetStudentList2(int iDisplayStart, int iDisplayLength);
        Task<List<StudentReport>> GetStudentReport(StudentFilterModel value);
        Task<bool> AddStudent(sr_StudentRecord value);
        Task<bool> UpdateStudent(sr_StudentRecord value);
        void DeleteStudent(sr_StudentRecord value);
        Task<IEnumerable<sr_StudentRecord>> GetAllStudent();
        Task<sr_StudentRecord> GetStudentByCode(string code);
        Task<sr_StudentRecord> GetStudentByid(int id);
        Task<List<StudentRecordVM>> GetStudentList(int iDisplayStart, int iDisplayLength);
        Task<List<StudentRecordVM>> GetInactiveStudentList(int iDisplayStart, int iDisplayLength);
        Task<int> getStudentListCount();
        Task<int> getInactiveStudentListCount();
        Task<List<sr_StudentRecord>> GetStudentListByName(string Studentname);
        Task<List<sr_StudentRecord>> GetOldStudentListByName(string Studenttname);
        Task<sr_StudentRecord> GetAllStudentByID(int id);
        StudentRecordVM GetStudentListByID(int id);
        StudentRecordVM GetOldStudentListByID(int id);
        Task<List<StudentRecordVM>> GetStudentListOnClaim();
    }
}

using Dapper;
using NavyAccountCore.Core.Data;
using NavyAccountCore.Entities;
using NavyAccountCore.Models;
using NavyAccountWeb.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static NavyAccountWeb.Models.SchoolFilterModels;

namespace NavyAccountWeb.Services
{
    public class StudentRecordService:IStudentRecordService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDapper dapper;
        public StudentRecordService(IDapper dapper,IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            this.dapper = dapper;
        }

        public async Task<bool> AddStudent(sr_StudentRecord value)
        {
             _unitOfWork.student.Create(value);
             return await _unitOfWork.Done();
        }

        public void DeleteStudent(sr_StudentRecord value)
        {
            _unitOfWork.student.Remove(value);
            _unitOfWork.Done();
        }

        public async Task<IEnumerable<sr_StudentRecord>> GetAllStudent()
        {
            return await _unitOfWork.student.GetAllStudent();
        }
        public async Task<sr_StudentRecord> GetAllStudentByID(int id)
        {
            return await _unitOfWork.student.getStudentById(id);
        }

        public async Task<sr_StudentRecord> GetStudentByCode(string code)
        {
            return await _unitOfWork.student.GetStudentByCode(x => x.Reg_Number == code);
        }

        public async Task<sr_StudentRecord> GetStudentByid(int id)
        {
            return await _unitOfWork.student.GetStudentByCode(x => x.id == id);
        }
        public async Task<List<StudentRecordVM>> GetStudentList(int iDisplayStart, int iDisplayLength)
        {
            return await _unitOfWork.student.getStudentList(iDisplayStart, iDisplayLength);
        }
        public async Task<List<StudentReport>> GetStudentList2(int iDisplayStart, int iDisplayLength)
        {
            return await _unitOfWork.student.getStudentList2(iDisplayStart, iDisplayLength);
        }
        public StudentRecordVM GetStudentListByID(int id)
        {
            return  _unitOfWork.student.getStudentListByID(id);
        }
        public StudentRecordVM GetOldStudentListByID(int id)
        {
            return _unitOfWork.student.getOldStudentListByID(id);
        }

        public async Task<int> getStudentListCount()
        {
            return await _unitOfWork.student.getStudentListCount();
        }
        public async Task<int> getInactiveStudentListCount()
        {
            return await _unitOfWork.student.getInactiveStudentListCount();
        }
        public async Task<List<sr_StudentRecord>> GetStudentListByName(string Studenttname)
        {
            return await _unitOfWork.student.getStudentListByName(Studenttname);
        }
        public async Task<List<sr_StudentRecord>> GetOldStudentListByName(string Studenttname)
        {
            return await _unitOfWork.student.getOldStudentListByName(Studenttname);
        }
        public async Task<bool> UpdateStudent(sr_StudentRecord value)
        {
            _unitOfWork.student.Update(value);
            return await _unitOfWork.Done();
        }

        public async Task<List<StudentRecordVM>> GetInactiveStudentList(int iDisplayStart, int iDisplayLength)
        {
            return await _unitOfWork.student.GetInactiveStudentList(iDisplayStart, iDisplayLength);
        }
        public async Task<List<StudentRecordVM>> GetStudentListOnClaim()
        {
            return await _unitOfWork.student.GetStudentListOnCliam();
        }

        public async Task<List<StudentReport>> GetStudentReport(StudentFilterModel value)
        {
            var result = new List<StudentReport>();
            var param = new DynamicParameters();
            result = dapper.GetAll<StudentReport>("sr_GetStudentNominalRoll", param, commandType: System.Data.CommandType.StoredProcedure);
            if(value.sortby=="School")
            result = result.Where(x => ((value.SchoolId==0)|| (x.SchoolId == value.SchoolId)) 
                        && ((value.ClassId == 0) || (x.ClassId == value.ClassId))
                        && ((value.ParentalStatus == "0") || (x.ParentalStatus == value.ParentalStatus))
                        && ((value.Status == "0") || (x.Status == value.Status))).OrderBy(x=>x.SchoolId).ToList();
            else if (value.sortby == "Class")
                result = result.Where(x => ((value.SchoolId == 0) || (x.SchoolId == value.SchoolId))
                       && ((value.ClassId == 0) || (x.ClassId == value.ClassId))
                       && ((value.ParentalStatus == "0") || (x.ParentalStatus == value.ParentalStatus))
                       && ((value.Status == "0") || (x.Status == value.Status))).OrderBy(x => x.ClassId).ToList();
            else if (value.sortby == "ParentialStatus")
                result = result.Where(x => ((value.SchoolId == 0) || (x.SchoolId == value.SchoolId))
                       && ((value.ClassId == 0) || (x.ClassId == value.ClassId))
                       && ((value.ParentalStatus == "0") || (x.ParentalStatus == value.ParentalStatus))
                       && ((value.Status == "0") || (x.Status == value.Status))).OrderBy(x => x.ParentalStatus).ToList();
            else if (value.sortby == "Status")
                result = result.Where(x => ((value.SchoolId == 0) || (x.SchoolId == value.SchoolId))
                       && ((value.ClassId == 0) || (x.ClassId == value.ClassId))
                       && ((value.ParentalStatus == "0") || (x.ParentalStatus == value.ParentalStatus))
                       && ((value.Status == "0") || (x.Status == value.Status))).OrderBy(x => x.Status).ToList();
            else 
                result = result.Where(x => ((value.SchoolId == 0) || (x.SchoolId == value.SchoolId))
                       && ((value.ClassId == 0) || (x.ClassId == value.ClassId))
                       && ((value.ParentalStatus == "0") || (x.ParentalStatus == value.ParentalStatus))
                       && ((value.Status == "0") || (x.Status == value.Status))).OrderBy(x => x.SchoolId).ToList();

            return result;
        }
    }
}

using NavyAccountCore.Core.Data;
using NavyAccountCore.Entities;
using NavyAccountWeb.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.Services
{
    public class StudentRecordService:IStudentRecordService
    {
        private readonly IUnitOfWork _unitOfWork;
        public StudentRecordService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

        public async Task<sr_StudentRecord> GetStudentByCode(string code)
        {
            return await _unitOfWork.student.GetStudentByCode(x => x.Reg_Number == code);
        }

        public async Task<sr_StudentRecord> GetStudentByid(int id)
        {
            return await _unitOfWork.student.GetStudentByCode(x => x.id == id);
        }
        public async Task<List<sr_StudentRecord>> GetStudentList(int iDisplayStart, int iDisplayLength)
        {
            return await _unitOfWork.student.getStudentList(iDisplayStart, iDisplayLength);
        }

        public async Task<int> getStudentListCount()
        {
            return await _unitOfWork.student.getStudentListCount();
        }
        public async Task<List<sr_StudentRecord>> GetStudentListByName(string Studenttname)
        {
            return await _unitOfWork.student.getStudentListByName(Studenttname);
        }
        public async Task<bool> UpdateStudent(sr_StudentRecord value)
        {
            _unitOfWork.student.Update(value);
            return await _unitOfWork.Done();
        }

        public async Task<List<sr_StudentRecord>> GetInactiveStudentList(int iDisplayStart, int iDisplayLength)
        {
            return await _unitOfWork.student.GetInactiveStudentList(iDisplayStart, iDisplayLength);
        }
    }
}

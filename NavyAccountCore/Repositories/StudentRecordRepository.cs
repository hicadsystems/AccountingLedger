using Microsoft.EntityFrameworkCore;
using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Repositories;
using NavyAccountCore.Entities;
using NavyAccountCore.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using NavyAccountCore.Models;

namespace NavyAccountCore.Repositories
{
    public class StudentRecordRepository : Repository<sr_StudentRecord>, IStudentRecordRepository
    {
        private readonly INavyAccountDbContext context;
        public StudentRecordRepository(INavyAccountDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<sr_StudentRecord> GetStudentByCode(Expression<Func<sr_StudentRecord, bool>> predicate)
        {
            return await context.sr_StudentRecord.FirstOrDefaultAsync(predicate);
        }
        public async Task<IEnumerable<sr_StudentRecord>> GetAllStudent()
        {
            return await context.sr_StudentRecord.ToListAsync();
        }
        public async Task<List<StudentRecordVM>> getStudentList(int iDisplayStart, int iDisplayLength)
        {

            var dd = (from pers in context.sr_StudentRecord
                      join sch in context.sr_SchoolRecord on pers.SchoolId equals sch.id
                      //join gu in context.sr_GuardianRecord on pers.Guardianid equals gu.id
                      //join par in context.sr_ParentRecord on pers.Parentid.Has equals par.id
                      join cl in context.sr_ClassRecord on pers.ClassId equals cl.id
                      select new StudentRecordVM
                      {

                          id = pers.id,
                          Reg_Number=pers.Reg_Number,
                          Surname = pers.Surname,
                          FirstName = pers.FirstName,
                          MiddleName = pers.MiddleName,
                          Email = pers.Email,
                          Age=pers.Age,
                          Sex=pers.Sex,
                          ParentalStatus=pers.ParentalStatus,
                          PhoneNumber = pers.PhoneNumber,
                          SchoolCode = sch.Schoolname,
                          ClassName = cl.ClassName,
                          //ParentName=par.Surname +" "+par.OtherNames,
                          //GuardianName = gu.Surname + " " + gu.OtherNames,
                          ClassCategory =pers.ClassCategory
                      }).Skip(iDisplayStart).Take(iDisplayLength).ToListAsync();
            return await dd;
        }
        public async Task<List<StudentReport>> getStudentList2(int iDisplayStart, int iDisplayLength)
        {
            var dd = (from pers in context.sr_StudentRecord
                      join sch in context.sr_SchoolRecord on pers.SchoolId equals sch.id
                      join gu in context.sr_GuardianRecord on pers.Guardianid equals gu.id
                      join par in context.sr_ParentRecord on pers.Parentid equals par.id
                      join cl in context.sr_ClassRecord on pers.ClassId equals cl.id
                      select new StudentReport
                      {

                          studentid = pers.id,
                          Reg_Number = pers.Reg_Number,
                          StudentName = pers.Surname+" "+ pers.FirstName+" "+ pers.MiddleName,
                          Email = pers.Email,
                          Age = pers.Age,
                          Sex = pers.Sex,
                          ParentalStatus = pers.ParentalStatus,
                          PhoneNumber = pers.PhoneNumber,
                          SchoolCode = sch.SchoolType,
                          SchoolName = sch.Schoolname,
                          ClassName = cl.ClassName,
                          ParentName = par.Surname + " " + par.OtherNames,
                          GuardianName = gu.Surname + " " + gu.OtherNames,
                          ClassCategory = pers.ClassCategory
                      }).Skip(iDisplayStart).Take(iDisplayLength).ToListAsync();
            return await dd;
        }
        public StudentRecordVM getStudentListByID(int id)
        {
            var dd = (from pers in context.sr_StudentRecord
                      join sch in context.sr_SchoolRecord on pers.SchoolId equals sch.id
                      join gu in context.sr_GuardianRecord on pers.Guardianid equals gu.id
                      join par in context.sr_ParentRecord on pers.Parentid equals par.id
                      join cl in context.sr_ClassRecord on pers.ClassId equals cl.id
                      where (pers.id==id)
                      select new StudentRecordVM
                      {

                          id = pers.id,
                          Reg_Number = pers.Reg_Number,
                          Surname = pers.Surname,
                          FirstName = pers.FirstName,
                          MiddleName = pers.MiddleName,
                          Email = pers.Email,
                          Age = pers.Age,
                          Sex = pers.Sex,
                          ParentalStatus = pers.ParentalStatus,
                          PhoneNumber = pers.PhoneNumber,
                          SchoolCode = sch.Schoolname,
                          ClassName = cl.ClassName,
                          ParentName = par.Surname + " " + par.OtherNames,
                          GuardianName = gu.Surname + " " + gu.OtherNames,
                          ClassCategory = pers.ClassCategory
                      }).FirstOrDefault();
            return dd;
        }
        public StudentRecordVM getOldStudentListByID(int id)
        {
            var dd = (from pers in context.sr_StudentRecord
                      join sch in context.sr_SchoolRecord on pers.SchoolId equals sch.id
                      join gu in context.sr_GuardianRecord on pers.Guardianid equals gu.id
                      join par in context.sr_ParentRecord on pers.Parentid equals par.id
                      join cl in context.sr_ClassRecord on pers.ClassId equals cl.id
                      where (pers.id == id && pers.Status=="Inactive")
                      select new StudentRecordVM
                      {

                          id = pers.id,
                          Reg_Number = pers.Reg_Number,
                          Surname = pers.Surname,
                          FirstName = pers.FirstName,
                          MiddleName = pers.MiddleName,
                          Email = pers.Email,
                          Age = pers.Age,
                          Sex = pers.Sex,
                          ParentalStatus = pers.ParentalStatus,
                          PhoneNumber = pers.PhoneNumber,
                          SchoolCode = sch.Schoolname,
                          ClassName = cl.ClassName,
                          ParentName = par.Surname + " " + par.OtherNames,
                          GuardianName = gu.Surname + " " + gu.OtherNames,
                          ClassCategory = pers.ClassCategory
                      }).FirstOrDefault();
            return dd;
        }
        public async Task<List<StudentRecordVM>> GetInactiveStudentList(int iDisplayStart, int iDisplayLength)
        {
            var dd = (from pers in context.sr_StudentRecord
                      join sch in context.sr_SchoolRecord on pers.SchoolId equals sch.id
                      join gu in context.sr_GuardianRecord on pers.Guardianid equals gu.id
                      join par in context.sr_ParentRecord on pers.Parentid equals par.id
                      join cl in context.sr_ClassRecord on pers.ClassId equals cl.id
                      where (pers.Status=="InActive")
                      select new StudentRecordVM
                      {

                          id = pers.id,
                          Reg_Number = pers.Reg_Number,
                          Surname = pers.Surname,
                          FirstName = pers.FirstName,
                          MiddleName = pers.MiddleName,
                          Email = pers.Email,
                          Age = pers.Age,
                          Sex = pers.Sex,
                          ParentalStatus = pers.ParentalStatus,
                          PhoneNumber = pers.PhoneNumber,
                          SchoolCode = sch.Schoolname,
                          ClassName = cl.ClassName,
                          ParentName = par.Surname + " " + par.OtherNames,
                          GuardianName = gu.Surname + " " + gu.OtherNames,
                          ClassCategory = pers.ClassCategory
                      }).Skip(iDisplayStart).Take(iDisplayLength).ToListAsync();
            return await dd;
        }
        public async Task<List<sr_StudentRecord>> getStudentListByName(string Studentname)
        {
            return await (from pers in context.sr_StudentRecord
                              //join npfranks in context.ranks on pers.rank equals npfranks.Id
                          where pers.Surname.Contains(Studentname) || pers.FirstName.Contains(Studentname)|| pers.Reg_Number.Contains(Studentname)
                          select new sr_StudentRecord
                          {
                              id = pers.id,
                              Surname = pers.Surname,
                              FirstName = pers.FirstName,
                              MiddleName = pers.MiddleName,
                              Email = pers.Email,
                              PhoneNumber = pers.PhoneNumber,
                              Class = pers.Class


                          }).ToListAsync();
        }
        public async Task<List<sr_StudentRecord>> getOldStudentListByName(string Studentname)
        {
            return await (from pers in context.sr_StudentRecord
                          where pers.Status=="InActive" && (pers.Surname.Contains(Studentname) || pers.FirstName.Contains(Studentname) || pers.Reg_Number.Contains(Studentname))
                          select new sr_StudentRecord
                          {
                              id = pers.id,
                              Surname = pers.Surname,
                              FirstName = pers.FirstName,
                              MiddleName = pers.MiddleName,
                              Email = pers.Email,
                              PhoneNumber = pers.PhoneNumber,
                              Class = pers.Class


                          }).ToListAsync();
        }
        public async Task<sr_StudentRecord> getStudentById(int id)
        {
            var dd = (from pers in context.sr_StudentRecord
                      where (pers.id==id)
                      select new sr_StudentRecord
                      {
                          id = pers.id,
                          Reg_Number = pers.Reg_Number,
                          Surname = pers.Surname,
                          FirstName = pers.FirstName,
                          MiddleName = pers.MiddleName,
                          Email = pers.Email,
                          Age = pers.Age,
                          Sex = pers.Sex,
                          ParentalStatus = pers.ParentalStatus,
                          SchoolCode = pers.SchoolCode,
                          PhoneNumber = pers.PhoneNumber,
                          Class = pers.Class,
                          ClassCategory = pers.ClassCategory,
                          Parentid=pers.Parentid,
                          Guardianid=pers.Guardianid,
                          ClassId=pers.ClassId,
                          SchoolId=pers.SchoolId,
                          Status=pers.Status,
                      }).FirstOrDefaultAsync();
            return await dd;
        }
        public async Task<int> getStudentListCount()
        {
            return await context.sr_StudentRecord.Where(x=>x.Status!= "Inactive").CountAsync();
        }
        public async Task<int> getInactiveStudentListCount()
        {
            return await context.sr_StudentRecord.Where(x => x.Status == "Inactive").CountAsync();
        }
        public async Task<List<StudentRecordVM>> GetStudentListOnCliam()
        {
            var dd = (from pers in context.sr_StudentRecord
                      join sch in context.sr_SchoolRecord on pers.SchoolId equals sch.id
                      join gu in context.sr_GuardianRecord on pers.Guardianid equals gu.id
                      join par in context.sr_ParentRecord on pers.Parentid equals par.id
                      join cl in context.sr_ClassRecord on pers.ClassId equals cl.id
                      where (pers.Status == "Active" && pers.ClaimAmount>0)
                      select new StudentRecordVM
                      {

                          id = pers.id,
                          Reg_Number = pers.Reg_Number,
                          Surname = pers.Surname,
                          FirstName = pers.FirstName,
                          MiddleName = pers.MiddleName,
                          Email = pers.Email,
                          Age = pers.Age,
                          Sex = pers.Sex,
                          ParentalStatus = pers.ParentalStatus,
                          PhoneNumber = pers.PhoneNumber,
                          SchoolCode = sch.Schoolname,
                          ClassName = cl.ClassName,
                          ParentName = par.Surname + " " + par.OtherNames,
                          GuardianName = gu.Surname + " " + gu.OtherNames,
                          ClassCategory = pers.ClassCategory,
                          ClaimAmount=pers.ClaimAmount,
                          ClaimDate=pers.ClaimDate
                      }).ToListAsync();
            return await dd;
        }
    }
}

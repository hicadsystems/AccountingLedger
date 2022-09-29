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
        public async Task<List<sr_StudentRecord>> getStudentList(int iDisplayStart, int iDisplayLength)
        {
            var dd = (from pers in context.sr_StudentRecord
                      select new sr_StudentRecord
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
                          SchoolCode = pers.SchoolCode,
                          PhoneNumber = pers.PhoneNumber,
                          Class = pers.Class,
                          ClassCategory=pers.ClassCategory
                      }).Skip(iDisplayStart).Take(iDisplayLength).ToListAsync();
            return await dd;
        }
        public async Task<List<sr_StudentRecord>> GetInactiveStudentList(int iDisplayStart, int iDisplayLength)
        {
            var dd = (from pers in context.sr_StudentRecord
                      where (pers.ExitDate!=null||pers.Status=="InActive")
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
        public async Task<int> getStudentListCount()
        {
            return await context.sr_StudentRecord.CountAsync();
        }
    }
}

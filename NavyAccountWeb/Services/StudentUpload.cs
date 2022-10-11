using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Entities;
using NavyAccountCore.Entities;
using NavyAccountCore.Models;
using NavyAccountWeb.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.Services
{
    public class StudentUpload
    {
        private readonly List<StudentRecordVM> studentRecords;
        private readonly IUnitOfWork unitOfWork;
        private readonly INavyAccountDbContext context;
        private string user;
       
        public StudentUpload(List<StudentRecordVM> studentRecords, IUnitOfWork unitOfWork, INavyAccountDbContext context, string user)
        {

            this.studentRecords = studentRecords;
            this.user = user;
            this.unitOfWork = unitOfWork;
            this.context = context;
        }

        public async Task processUploadInThread()
        {

            foreach (var s in studentRecords)
            {
                var getstudent= unitOfWork.student.GetStudentByCode(x => x.Reg_Number == s.Reg_Number);
                if (getstudent.Result == null)
                {
                    var getschool = unitOfWork.school.GetSchoolByCode(x => x.Schoolname == s.SchoolCode).Result;
                    var cla = context.sr_ClassRecord.Where(x => x.ClassName == s.ClassName).FirstOrDefault();


                    unitOfWork.student.Create(new sr_StudentRecord()
                    {
                        Reg_Number = s.Reg_Number,
                        Surname = s.Surname,
                        FirstName = s.FirstName,
                        MiddleName = s.MiddleName,
                        Sex = s.Sex,
                        Age = s.Age,
                        CommencementDate =Convert.ToDateTime(s.CommencementDate),
                        ClassId = cla.id,
                        ClassCategory = s.ClassCategory,
                        ParentalStatus = s.ParentalStatus,
                        SchoolId = getschool.id,
                        PhoneNumber = s.PhoneNumber,
                        Email = s.Email,  
                        Status="Active",
                        //CreatedBy = user,
                        //CreatedDate = System.DateTime.Now,
                    });
                   await unitOfWork.Done();
                }
            }
        }

    }
}
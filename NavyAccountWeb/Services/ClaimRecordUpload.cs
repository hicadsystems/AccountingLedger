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
    public class ClaimRecordUpload
    {
        private readonly List<ClaimRecordVM> claimRecords;
        private readonly IUnitOfWork unitOfWork;
        private readonly INavyAccountDbContext context;
        private readonly IStudentRecordService strecordService;
        private readonly IClaimRecordService claimrecordService;
        private string user;
        public string errormessage;



        public ClaimRecordUpload(List<ClaimRecordVM> claimRecords, IUnitOfWork unitOfWork, INavyAccountDbContext context, IStudentRecordService strecordService, IClaimRecordService claimrecordService, string user,string errormessage)
        {

            this.claimRecords = claimRecords;
            this.user = user;
            this.unitOfWork = unitOfWork;
            this.context = context;
            this.errormessage=errormessage;
            this.strecordService = strecordService;
            this.claimrecordService = claimrecordService;
        }

        public async Task<string> processUploadInThread()
        {
            try
            {

            foreach (var s in claimRecords)
            {
                var getstudent= unitOfWork.student.GetStudentByCode(x => x.Reg_Number == s.Reg_Number).Result;
                var getclaim = unitOfWork.schclaim.GetClaimRecordByCode(x => x.Reg_Number == s.Reg_Number).Result;

                if (getstudent != null && getclaim == null)
                {
                    decimal ClaimAmount = 0M;
                    if (getstudent.SchoolCode == "Primary")
                        ClaimAmount = Convert.ToDecimal(200000);
                    if (getstudent.SchoolCode == "Secondary")
                         ClaimAmount = Convert.ToDecimal(400000);
                        unitOfWork.schclaim.Create(new sr_ClaimRecord()
                    {
                        Reg_Number = s.Reg_Number,
                        Amount = ClaimAmount,
                        Period=s.Period,
                        Term=s.Term,
                        Transdate = DateTime.Now,
                        CreatedBy = user,
                        CreatedDate=DateTime.Now

                    });
                    await unitOfWork.Done();

                    getstudent.ClaimAmount = ClaimAmount;
                    getstudent.ClaimDate = DateTime.Now;
                    await strecordService.UpdateStudent(getstudent);
                   // unitOfWork.student.Update(getstudent);

                    }
                    else
                    {
                        return errormessage = "Student Record Not Found or Student alrady on claim";
                    }
            }
                return errormessage = "Student Record Uploaded";
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<string> reinUploadInThread()
        {
            try
            {

                foreach (var s in claimRecords)
                {
                    var getstudent = unitOfWork.student.GetStudentByCode(x => x.Reg_Number == s.Reg_Number).Result;
                    if (getstudent != null)
                    {
                      await claimrecordService.UpdateClaimReinBySchool(s.Reg_Number, s.School, s.Class, s.amount,s.remark, user);
                    }
                    else
                    {
                        return errormessage = "Student Record Not Found or Student alrady on claim";
                    }
                }
                return errormessage = "Claim Deduction Uploaded";
            }
            catch (Exception ex)
            {
                return errormessage = "Error";
                throw;
            }
        }

    }
}
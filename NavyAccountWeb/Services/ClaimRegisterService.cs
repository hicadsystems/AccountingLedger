using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Entities;
using NavyAccountCore.Models;
using NavyAccountWeb.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NavyAccountWeb.Services
{
    public class ClaimRegisterService : IClaimRegisterService
    {
        private readonly IUnitOfWork unitOfWork;

        public ClaimRegisterService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<bool> AddNpfClaimRegister(Npf_ClaimRegister bl_sheet)
        {
            unitOfWork.claimregister.Create(bl_sheet);
            return await unitOfWork.Done();
        }

        public IEnumerable<Npf_ClaimRegister> GetClaimRegister()
        {
            return unitOfWork.claimregister.All();
        }

        public Task<Npf_ClaimRegister> GetClaimRegisterByCode(string bcode)
        {
            return unitOfWork.claimregister.getClaimByUser(bcode);
        }

        public Task<Npf_ClaimRegister> GetClaimRegisterByCode2(string bcode,string fundtype)
        {
            return unitOfWork.claimregister.getClaimByUser2(bcode, fundtype);
        }


        public Task<Npf_ClaimRegister> GetClaimRegisterById(int id)
        {
            return unitOfWork.claimregister.Find(id);
        }

        public async Task<bool> UpdateNpfClaimRegister(Npf_ClaimRegister bl_sheet)
        {
            unitOfWork.claimregister.Update(bl_sheet);
            return await unitOfWork.Done();
        }
       public IEnumerable<ClaimModel> GetclaimBysvcNo(string svcno)
        {
            return unitOfWork.claimregister.GetclaimBysvcNo(svcno);
        }
        public IEnumerable<ClaimModel> Getclaim(string svcno, string fundcode)
        {
            return unitOfWork.claimregister.Getclaim(svcno,fundcode);
        }
       public IEnumerable<ClaimModel2> Getclaimbydate(DateTime startdate, DateTime enddate,string status)
        {
            return unitOfWork.claimregister.Getclaimbydate(startdate,enddate,status);
        }

        public IEnumerable<ClaimModel> GetClaimRegisterNotApproved()
        {
            return unitOfWork.claimregister.GetClaimNotApproved();
        }
        public IEnumerable<ClaimModel> GetClaimRegisterApproved()
        {
            return unitOfWork.claimregister.GetClaimApproved();
        }

        public IEnumerable<ClaimModel> GetClaimRegisterNotApprovedList(int personId)
        {
            return unitOfWork.claimregister.GetClaimNotApprovedList(personId);
        }
        public IEnumerable<ClaimModel> GetClaimRegisterApprovedList(int personId)
        {
            return unitOfWork.claimregister.GetClaimApprovedList(personId);
        }
    }
}

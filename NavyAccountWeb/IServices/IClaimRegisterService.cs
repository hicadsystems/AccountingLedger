using NavyAccountCore.Core.Entities;
using NavyAccountCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.IServices
{
    public interface IClaimRegisterService
    {
        IEnumerable<Npf_ClaimRegister> GetClaimRegister();
        Task<Npf_ClaimRegister> GetClaimRegisterById(int id);
        Task<Npf_ClaimRegister> GetClaimRegisterByCode2(string bcode, string fundtype);
        Task<Npf_ClaimRegister> GetClaimRegisterByCode(string bcode);
        Task<bool> AddNpfClaimRegister(Npf_ClaimRegister bl_sheet);
        Task<bool> UpdateNpfClaimRegister(Npf_ClaimRegister bl_sheet);
        IEnumerable<ClaimModel> GetclaimBysvcNo(string svcno);
        IEnumerable<ClaimModel> Getclaim(string svcno, string fundcode);
        IEnumerable<ClaimModel2> Getclaimbydate(DateTime startdate, DateTime enddate,string status);

        IEnumerable<ClaimModel> GetClaimRegisterNotApproved();
        IEnumerable<ClaimModel> GetClaimRegisterApproved();
        IEnumerable<ClaimModel> GetClaimRegisterNotApprovedList(int personId);
        IEnumerable<ClaimModel> GetClaimRegisterApprovedList(int personId);

    }
}

using NavyAccountCore.Core.Entities;
using NavyAccountCore.Entities;
using NavyAccountCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NavyAccountCore.Core.IRepositories
{
    public interface IClaimRegister:IRepository<Npf_ClaimRegister>
    {
        Task<Npf_ClaimRegister> getClaimByUser(string svcno);
        Task<Npf_ClaimRegister> getClaimByUser2(string svcno, string fundtype);
        IEnumerable<ClaimModel> GetclaimBysvcNo(string svcno);
        IEnumerable<ClaimModel> Getclaim(string svcno, string fundcode);
        IEnumerable<ClaimModel2> Getclaimbydate(DateTime startdate, DateTime enddate,string status);

        IEnumerable<ClaimModel> GetClaimNotApproved();
        IEnumerable<ClaimModel> GetClaimApproved();
        IEnumerable<ClaimModel> GetClaimNotApprovedList(int personId);
        IEnumerable<ClaimModel> GetClaimApprovedList(int personId);

    }
}

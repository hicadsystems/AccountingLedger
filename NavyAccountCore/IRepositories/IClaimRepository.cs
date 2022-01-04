using NavyAccountCore.Core.Entities;
using NavyAccountCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NavyAccountCore.Core.IRepositories
{
    public interface IClaimRepository
    {
        decimal GetAmountPerNavip(string svcNo,string fundtype, out decimal amt);
        decimal GetAmountPerDependent(string svcNo, string fundtype);
        decimal OutStandingLoanClaims(string svcNo);
        bool checkClaimBatchnoExist(string batchno);
        List<Npf_ClaimRegister> GetBatchList();
        List<Npf_ClaimRegister> GetSingleBatchno(string batchno);
        Npf_ClaimRegister Getperson(string svcNo, string fundtype);
        ClaimModel2 GetpersonClaim(string svcNo, string fundtype);
        string fundTypeDesc(string fundtype);

    }
}

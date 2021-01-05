using NavyAccountCore.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.IServices
{
    public interface IClaimTypeServices
    {
        decimal GetDependentAmount(string svcNo, string fundtype);
        decimal GetNavipAmount(string svcNo, string fundtype,out decimal amt);

        decimal OutstandingLoanServices(string svcNo);

        bool checkClaimBatchnoExist(string batchno);

        List<Npf_ClaimRegister> GetBatchList();
        List<Npf_ClaimRegister> GetSingleBatchno(string batchno);
        Npf_ClaimRegister GetPerson(string svcNo, string fundtype);

        string fundtypeDesc(string code);

    }
}

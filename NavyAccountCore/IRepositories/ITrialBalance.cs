using NavyAccountCore.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NavyAccountCore.Core.IRepositories
{
    public interface ITrialBalance : IRepository<npf_ledger>
    {
        bool checkAcctCode(string acctcode);
    }
}

using NavyAccountCore.Core.Entities;
using NavyAccountCore.Core.IRepositories;
using NavyAccountCore.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NavyAccountCore.Core.IRepositories
{
    public interface ILedgerRepository : IRepository<npf_ledger>
    {
        npf_ledger Getledger(Expression<Func<npf_ledger, bool>> predicate);
        List<AcctLedgerViewModel> getLedgerInfoCSD(string fundcode);
        npf_ledger getSurplusValue(Expression<Func<npf_ledger, bool>> predicate);

        List<AcctLedgerViewModel> getLedgerInfo(string serviceno, string fundcode);

    }
}

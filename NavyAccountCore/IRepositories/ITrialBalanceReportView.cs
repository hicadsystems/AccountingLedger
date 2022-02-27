using NavyAccountCore.Core.IRepositories;
using NavyAccountCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NavyAccountCore.IRepositories
{
    public interface ITrialBalanceReportView : IRepository<V_TRIALBALANCE>
    {
        IEnumerable<V_TRIALBALANCE> GetTrialBalanceReport();

        IEnumerable<V_TRIALBALANCE> GetTrialBalanceReportProcedure(string fundcode);

        IEnumerable<V_TRIALBALANCE> GetBalSheet_MainTrialBalanceProcedure(string fundcode);

        IEnumerable<V_TRIALBALANCE> GetBalSheet_StoredProcedure(string fundcode);
    }
}

using NavyAccountCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NavyAccountCore.Core.IRepositories
{
    public interface IAuditTrail
    {
        IEnumerable<LedgersView> GetAllNpfChart();
        LedgersView GetOpenBal(string acctcode,string wdoc);

        IEnumerable<LedgersView> GetAllHistory(string speriod,string acctcode);
        IEnumerable<string> GetAllyear();
        string GetAccountDesc(string code);

    }
}

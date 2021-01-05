using NavyAccountCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.IServices
{
    public interface IAuditTrailServices
    {
        IEnumerable<LedgersView> GetNpfChart();
        LedgersView GetSingleRecord(string acctcode, string wdoc);
        IEnumerable<LedgersView> GetAllRecord(string speriod, string acctcode);
        IEnumerable<string> GetDateInHistory();

        string GetNpfDesc(string code);
    }
}

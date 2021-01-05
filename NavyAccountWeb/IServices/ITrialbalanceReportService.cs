using NavyAccountCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.IServices
{
    public interface ITrialbalanceReportService
    {
        IEnumerable<LedgersView> GenerateTrialBalanceReport(string ind,string fundcode);
        IEnumerable<LedgersView> GenMainLedgers(List<LedgersView>fol);
        IEnumerable<LedgersView> GetLoanAct();
        IEnumerable<LedgersView> GenerateMainAcct();
        string getMainActDesc(string code);
        IEnumerable<LedgersView> GenerateNpfHistory(string wdoc);

    }
}

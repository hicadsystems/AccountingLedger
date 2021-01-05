using NavyAccountCore.Models;
using System.Collections.Generic;

namespace NavyAccountCore.Core.IRepositories
{
    public interface ITrialBalanceReport
    {
        IEnumerable<LedgersView> GetAllLedegers(string indicator,string fundcode);
        IEnumerable<LedgersView> GetAllMainAct();
        string GetMainActDescription(string code);
        IEnumerable<LedgersView> GetAllLoanTypes();
        IEnumerable<LedgersView> GetMainLedgersByDate(List<LedgersView> sol);
        IEnumerable<LedgersView> GetAllNPFHistory(string codedate);
    }
}

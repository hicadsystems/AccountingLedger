using NavyAccountCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.IServices
{
    public interface ISurplusService
    {
        IEnumerable<LedgersView2> GetAllSurplus();
        IEnumerable<LedgersView2> GetAllSurplus2();
        IEnumerable<LedgersView2> GetBalSheet();
        IEnumerable<LedgersView2> GetBalSheet_TrialBalance();

        IEnumerable<V_TRIALBALANCE> GetTrialBalanceView();

        IEnumerable<V_TRIALBALANCE> GetTrialBalanceProcedure(string funcode);
        public IEnumerable<LedgersView2> GetBalSheet_MainTrialBalance();

        IEnumerable<V_TRIALBALANCE> GetBalSheet_MainTrialBalanceProcedure(string funcode);

        IEnumerable<V_TRIALBALANCE> GetBalSheet_StoredProcedure(string fundcode);

        IEnumerable<LedgersView2> GetSurplus_Deficit();
        IEnumerable<LedgersView2> GetBalanceSheetSurplus_Deficit();
        IEnumerable<LedgersView2> GetBalSheet2();
        IEnumerable<LedgersView> GetAllSurplusbyDoc(string doc, string fundcode);
        string RefractCode(string code);
        string description(string code);
    }
}

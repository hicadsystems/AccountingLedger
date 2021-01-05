using NavyAccountCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NavyAccountCore.Core.IRepositories
{
    public interface ISuplusRepo
    {
        IEnumerable<LedgersView2> GetAllSurplusOrDeficit();
        IEnumerable<LedgersView2> GetAllSurplusOrDeficit2();

        IEnumerable<LedgersView2> GetBalsheet();
        IEnumerable<LedgersView2> GetBalSheet_TrialBalance();
        IEnumerable<LedgersView2> GetBalSheet_MainTrialBalance();

        IEnumerable<LedgersView2> GetSurplus_Deficit();
        IEnumerable<LedgersView2> GetBalanceSheetSurplus_Deficit();

        IEnumerable<LedgersView2> GetBalsheet2();
        IEnumerable<LedgersView> GetAllSurplusOrdeficitByDoc(string doc, string fundcode);
        string getDescription(string code);
        string RefractCode(string code);
    }
}

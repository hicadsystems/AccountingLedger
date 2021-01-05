using NavyAccountCore.Models;
using NavyAccountWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.ViewModels
{
    public class TrialBalanceViewModel
    {
        public string code { get; set; }
        public string description { get; set; }
        public decimal debit { get; set; }
        public decimal credit { get; set; }
        public string mainacct { get; set; }

        [Required(ErrorMessage ="you need to select it")]
        public string indicator { get; set; }
        public string month { get; set; }
        public string year { get; set; }
        public string ProcessMth { get; set; }
        public string ProcessYear { get; set; }
        public string fundcode { get; set; }
        public IEnumerable<balanceViewModel> IndList { get; set; }
        public IEnumerable<balanceViewModel> getAllMonthList { get; set; }
        public IEnumerable<LedgersView> getMainAct { get; set; }

    }

    public class ReportViewModel
    {
        public IEnumerable<TrialBalanceViewModel> getList { get; set; }
        public decimal TotalCredit { get; set; }
        public decimal TotalDebit { get; set; }
        public string month { get; set; }
        public string year { get; set; }
        public string HeaderVariable { get; set; }
    }

    public class ReportViewModel3
    {
        public IEnumerable<LedgersView> getMain { get; set; }
        public IEnumerable<LedgersView> getTrialbal { get; set; }
        public decimal TotalCredit { get; set; }
        public decimal TotalDebit { get; set; }
        public string month { get; set; }
        public string year { get; set; }
        public string HeaderVariable { get; set; }

        public decimal TotalCr { get; set; }
        public decimal TotalDr { get; set; }
        public decimal TotalCr2 { get; set; }
        public decimal TotalDr2 { get; set; }

    }

    public class LoanPositionViewModel
    {
        public string mainacct { get; set; }
        public IEnumerable<LedgersView> getMainAct { get; set; }
        public string fundcode { get; set; }
    }

    public class History_LedgerViewModel
    {
        public IEnumerable<Name_Value> getYear { get; set; }
        public string fundcode { get; set; }
        public int yeartoprocess { get; set; }
    }


}

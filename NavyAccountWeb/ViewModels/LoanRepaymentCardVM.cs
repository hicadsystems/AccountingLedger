using NavyAccountCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.ViewModels
{
    public class LoanRepaymentCardVM
    {
        public string Name { get; set; }

        public string Rank_Rate { get; set; }

        public string TelephoneNumber { get; set; }

        public List<LoanView> loanViews { get; set; }
    }
}

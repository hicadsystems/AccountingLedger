using NavyAccountCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.ViewModels
{
    public class LoanDiscVM:pf_loandisc
    {
       
        public int PersonID { get; set; }
        public int LoanTypeID { get; set; }
        public int count { get; set; }

    }
}

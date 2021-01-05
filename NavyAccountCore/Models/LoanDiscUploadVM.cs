using System;
using System.Collections.Generic;
using System.Text;

namespace NavyAccountCore.Models
{
    public class LoanDiscUploadVM
    {
        public string svcno { get; set; }

        public string loancode { get; set; }

        public decimal principal { get; set; }

        public decimal loanpay { get; set; }

        public decimal amount { get; set; }

        public decimal extloan { get; set; }

        public string  loanaccount { get; set; }

        public string groupcode { get; set; }
        public string batchno { get; set; }

    }
}

using NavyAccountCore.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NavyAccountCore.Models
{
    public class ContrDiscUpload
    {
        public string svcno { get; set; }

        public string fundcode { get; set; }

        public decimal Amount { get; set; }

        public decimal prevamt { get; set; }

        public decimal extamt { get; set; }

        public string Contraccount { get; set; }
        public string loanacct { get; set; }
        public string groupcode { get; set; }
        public string processingperiod { get; set; }
        public string Batchno { get; set; }
        public IEnumerable<Pf_fund> getfund { get; set; }
        public IEnumerable<Pf_loanType> getloan { get; set; }
    }
}

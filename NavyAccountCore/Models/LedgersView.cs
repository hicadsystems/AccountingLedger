using System;
using System.Collections.Generic;
using System.Text;

namespace NavyAccountCore.Models
{
    public class LedgersView
    {
        public string code { get; set; }
        public string desc { get; set; }
        public string period { get; set; }
        public decimal opBal { get; set; }
        public decimal dbbal { get; set; }
        public decimal crbal { get; set; }
        public string doc { get; set; }
        public Nullable<DateTime> date { get; set; }
        public string remark { get; set; }
        public decimal TotalCredit { get; set; }
        public decimal TotalDebit { get; set; }
        public decimal Amount { get; set; }
        public decimal balance { get; set; }
 

    }

  

    }

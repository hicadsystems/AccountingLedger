using System;
using System.Collections.Generic;
using System.Text;

namespace NavyAccountCore.Models
{
    public class LoandiscVM
    {
        public int count { get; set; }
        public int Id { get; set; }
        public string loanappno { get; set; }
        public string fundcode { get; set; }
        public string loantype { get; set; }
        public string loantypename { get; set; }
        public string personname { get; set; }
        public decimal principal { get; set; }
        public decimal interest { get; set; }
        public decimal loanpay { get; set; }
        public decimal prvloan { get; set; }
        public decimal extpay { get; set; }
        public decimal loanvar { get; set; }
        public decimal loandiscr { get; set; }
        public string loanact { get; set; }
        public string period { get; set; }
        public string intract { get; set; }
        public string trusteeact { get; set; }
        public string incomeact { get; set; }
        public string liabilityact { get; set; }
        public string groupcode { get; set; }
        public DateTime datecreated { get; set; }
        public string createdby { get; set; }

    }
}

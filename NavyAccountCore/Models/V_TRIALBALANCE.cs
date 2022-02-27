using System;
using System.Collections.Generic;
using System.Text;

namespace NavyAccountCore.Models
{
    public class V_TRIALBALANCE
    {
        public string maincode { get; set; }

        public string maindesc { get; set; }

        public string acctcode { get; set; }
        public string subdesc { get; set; }
        public decimal opbalance { get; set; }
        public decimal adbbalance { get; set; }
        public decimal crbalance { get; set; }
        public decimal amount { get; set; }
        public decimal openDB { get; set; }
        public decimal openCR { get; set; }
    }
}

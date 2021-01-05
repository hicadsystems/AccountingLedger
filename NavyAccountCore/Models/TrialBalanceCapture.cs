using System;
using System.Collections.Generic;
using System.Text;

namespace NavyAccountCore.Models
{
    public class TrialBalanceCapture
    {
        public string ACCode { get; set; }
        public decimal DEBIT { get; set; }
        public decimal CREDIT { get; set; }
        public string description { get; set; }
        public string BatchNo { get; set; }
    }

    public class ClaimCapture
    {
        public string svcno { get; set; }
        public string accountname { get; set; }
        public string bank { get; set; }
        public string accountno { get; set; }
        public decimal amount { get; set; }
        public string remark { get; set; }
        public string batchno { get; set; }
        public string type { get; set; }
        public string rank { get; set; }

    }
}

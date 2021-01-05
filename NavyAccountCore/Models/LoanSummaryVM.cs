using System;
using System.Collections.Generic;
using System.Text;

namespace NavyAccountCore.Models
{
    public class LoanSummaryVM
    {
        public string batchno { get; set; }
        public int noofpeople { get; set; }
        public decimal totalloan { get; set; }
        public int noofdefaulter { get; set; }
        public int curcount { get; set; }
        public int term { get; set; }
        public decimal amountpaid { get; set; }
        public decimal amountpaidP { get; set; }
        public decimal amountpaidH { get; set; }
        public decimal amountpaidOP { get; set; }
        public decimal amountowned { get; set; }
        public decimal amountperyear { get; set; }
        public decimal totalamountperyear { get; set; }
        public decimal amountperyearR { get; set; }
        public DateTime Effectivedate { get; set; }
        public string remark { get; set; }
    }
}

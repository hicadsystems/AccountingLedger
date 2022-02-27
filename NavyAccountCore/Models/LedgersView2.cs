using NavyAccountCore.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NavyAccountCore.Models
{
    public class LedgersView2
    {
        public string acctcode { get; set; }
        public Nullable<decimal> opbalance { get; set; }
        public Nullable<decimal> adbbalance { get; set; }
        public Nullable<decimal> crbalance { get; set; }
        public Nullable<decimal> balpl { get; set; }
        public IEnumerable<npf_mainact> mainAccountModel { get; set; }
        public string mainAccountCode { get; set; }
        public string mainAccountDesc { get; set; }
        public string balSheetCode { get; set; }
        public string description { get; set; }
        public string MDesc { get; set; }
        public decimal Amount { get; set; }
        public Nullable<decimal> Total { get; set; }
        public string bl_desc { get; set; }
        public string docNo { get; set; }
    }
}

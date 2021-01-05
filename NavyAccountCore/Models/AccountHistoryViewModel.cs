using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NavyAccountCore.Models
{
    [JsonObject]
    public class AccountHistoryViewModel
    {
        public string dateoftransaction { get; set; }

        public string remarks { get; set; }

        public string documentno { get; set; }

        public decimal debitAmount { get; set; }

        public decimal creditAmount { get; set; }
    }
}

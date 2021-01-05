using NavyAccountCore.Core.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NavyAccountCore.Models
{
    [JsonObject]
    public class LoanTypeView : Pf_loanType
    {
        public string fundtypedesc { get; set; }

      
    }
    public class loanreview
    {
        public int id { get; set; }
        public string LoanType { get; set; }
        public DateTime ReviewDate { get; set; }
        public int Interestrate { get; set; }
        public string Loantypedesc { get; set; }
    }
}

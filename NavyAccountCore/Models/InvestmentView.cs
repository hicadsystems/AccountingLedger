using NavyAccountCore.Core.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NavyAccountCore.Models
{
    [JsonObject]
    public class InvestmentView: Pf_InvestRegister
    {
        
        public string issuancebank { get; set; }
        public string receivingbank { get; set; }
        public string Company { get; set; }
        
    }
}

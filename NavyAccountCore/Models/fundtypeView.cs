using NavyAccountCore.Core.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NavyAccountCore.Models
{
    [JsonObject]
    public class fundtypeView:Pf_fund
    {
        public string pffund_Code { get; set; }
        public string pffund_Description { get; set; }
    }
}

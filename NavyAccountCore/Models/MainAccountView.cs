using NavyAccountCore.Core.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NavyAccountCore.Models
{
    [JsonObject]
    public class MainAccountView: npf_mainact
    {
        public string balSheetdesc { get; set; }

        public string subtypeDesc { get; set; }
    }
}

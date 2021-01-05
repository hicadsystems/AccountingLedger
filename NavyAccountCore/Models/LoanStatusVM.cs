using NavyAccountCore.Core.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NavyAccountCore.Models
{
    [JsonObject]
    public class LoanStatusVM:LoanStatus
    {
        public string desc { get; set; }

    }
}

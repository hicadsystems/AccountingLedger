using NavyAccountCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.ViewModels
{
    public class ReportViewModel4
    {
        public string code { get; set; }
        public string bl_desc { get; set; }
        public decimal total { get; set; }
        public IEnumerable<LedgersView2> tolly { get; set; }
    }
}

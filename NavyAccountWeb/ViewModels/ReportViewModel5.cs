using NavyAccountCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.ViewModels
{
    public class ReportViewModel5
    {
        public IEnumerable<ReportViewModel4> colly { get; set; }
        public decimal deficit { get; set; }
    }

    public class ReportViewModelQ
    {
        public List<LedgersView2> p { get; set; }
        public List<LedgersView2> q { get; set; }
        public decimal sup { get; set; }

    }
}

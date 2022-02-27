using NavyAccountCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.ViewModels
{
    public class ReportViewModel2
    {
        public decimal openBalance { get; set; }
        public decimal dbBalance { get; set; }
        public decimal crBalance { get; set; }
        public decimal Balance { get; set; }
        public string AcctCode { get; set; }
        public string AcctDesc { get; set; }
        public string year { get; set; }
        public string remark { get; set; }
        public IEnumerable<LedgersView>solly{get;set;}
        public LedgersView opy { get; set; }

        
    }


}

using NavyAccountCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.ViewModels
{
    public class LoanRepaymentViewModel
    {
        public IEnumerable<LoanView> solly { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Rank { get; set; }
        public decimal interest { get; set; }
        public decimal Tenor { get; set; }
        public decimal AmountGranted { get; set; }
    }
}

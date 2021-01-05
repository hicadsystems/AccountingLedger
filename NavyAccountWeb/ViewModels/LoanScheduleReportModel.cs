using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.ViewModels
{
    public class LoanScheduleReportModel
    {
        public decimal openingbalance { get; set; }
        public decimal interestpayment { get; set; }
        public decimal monthlydeduction { get; set; }
        public decimal totalrepayment { get; set; }
        public decimal outstandingbalance { get; set; }

        
    }
}

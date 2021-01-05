using System;
using System.Collections.Generic;
using System.Text;

namespace NavyAccountCore.Models
{
    public class LoanView
    {
        public int id { get; set; }
        public double openingBalance { get; set; }
        public double PrincipalRepayment { get; set; }
        public double interestPayment { get; set; }
        public double MonthlyDeduction { get; set; }
        public double TotalRepaymentToDate { get; set; }
        public double OutstandingBalance { get; set; }
    }

    public class LoanView2
    {

        public int personid { get; set; }
        public int loantype { get; set; }
        public string loancode { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Rank { get; set; }
        public int rankId { get; set; }
        public decimal interest { get; set; }
        public decimal Tenor { get; set; }
        public decimal AmountGranted { get; set; }
    }
}

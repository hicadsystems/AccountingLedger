using System;
using System.Collections.Generic;
using System.Text;

namespace NavyAccountCore.Models
{
    public class DischargeCalcView
    {

        public List<PersonContributions> personContributions { get; set; }

        public decimal contrTotal { get; set; }

        public List<PersonLoan> personLoans { get; set; }

        public decimal loanTotal { get; set; }

        public decimal theTwoTotal { get; set; }
    }
    public class ClaimModel
    {

      
        public int Id { get; set; }

        public Nullable<int> PersonID { get; set; }
        public string FundTypeID { get; set; }
        public string funddesc { get; set; }
        public string svcno { get; set; }
        public DateTime appdate { get; set; }
        public string status { get; set; }
        public DateTime statusdate { get; set; }

        public Nullable<decimal> TotalContribution { get; set; }
        public Nullable<decimal> interest { get; set; }
        public Nullable<decimal> loan { get; set; }
        public string Remark { get; set; }
        public string Beneficiary { get; set; }
        public decimal AmountDue { get; set; }
        public string chequeno { get; set; }

        public Nullable<decimal> amountPaid { get; set; }

        public Nullable<decimal> amountReceived { get; set; }

        public string incomeacct { get; set; }
        public string bank { get; set; }
        public string acctno { get; set; }
        public string designation { get; set; }
    }
    public class ClaimModel2
    {
        public string svcno { get; set; }
        public string Beneficiary { get; set; }
        public string designation { get; set; }
        public string bank { get; set; }
        public string acctno { get; set; }
        public decimal TotalContribution { get; set; }     
        public string Remark { get; set; }
        public string batchno { get; set; }
        public DateTime statusdate { get; set; }
        public DateTime appdate { get; set; }
        public DateTime startdate { get; set; }
        public DateTime enddate { get; set; }


    }
}

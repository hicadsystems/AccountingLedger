using System;
using System.Collections.Generic;
using System.Text;

namespace NavyAccountCore.Models
{
    public class LoanRegisterViewModel
    {
        public int Id { get; set; }
        public int PersonID { get; set; }

        public string firstName { get; set; }
        public string lastName { get; set; }
        public string middleName { get; set; }
        public int LoanTypeID { get; set; }
        public string LoanTypecode { get; set; }
        public string LoanTypeDesc { get; set; }
        public int FundTypeID { get; set; }
        public string FundTypecode { get; set; }
        public string FundTypeDesc { get; set; }

        public string loanAccount { get; set; }
        public Nullable<System.DateTime> ApproveDate { get; set; }
        public decimal Amount { get; set; }
        public string Tenure { get; set; }
        public string ChequeNo { get; set; }
        public string BankID { get; set; }
        public string batchNo { get; set; }

        public string applicationNo { get; set; }
        public string BankName { get; set; }
        public System.DateTime EffectiveDate { get; set; }
        public string Status { get; set; }
        public string StatusAndDate { get; set; }
        public string svcno { get; set; }
        public int StatusId { get; set; }
        public System.DateTime ExpDate { get; set; }
        public string VoucherNo { get; set; }
        public string LoanAppNo { get; set; }
        public string remarks { get; set; }
        public System.DateTime datecreated { get; set; }
        public DateTime startdate { get; set; }
        public DateTime enddate { get; set; }
        public string createdby { get; set; }
    }
    public class LoanRegisterViewModelRecieveable
    {

        public string Rank { get; set; }
        public string Name { get; set; }
        public string svcno { get; set; }
        public decimal Amount { get; set; }

        public decimal interstamt { get; set; }
        public decimal totalloantamt { get; set; }
        public string Tenure { get; set; }
        public decimal monthlydeductions { get; set; }
        public int loancount { get; set; }
        public decimal totalloanpay { get; set; }
        public int monthoutstanding { get; set; }
        public decimal OutstandingPay { get; set; }
        public string LoanTypeDesc { get; set; }
        public int rankid { get; set; }

    }
    public class LoanRegisterViewModel22
    {
        public string rank { get; set; }
        public string svcno { get; set; }
        public string Name { get; set; }
        public string LoanTypeDesc { get; set; }
        public decimal Amount { get; set; }
        public decimal AmountWithInterst { get; set; }
        public string Tenure { get; set; }
        public string Status { get; set; }
        public string batchNo { get; set; }
      
  
    
        
    }
    public class LoanRegisterViewModel222
    {
        public string rank { get; set; }
        public string svcno { get; set; }
        public string Name { get; set; }
        public string LoanTypeDesc { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public string batchNo { get; set; }
        public string Tenure { get; set; }



    }
}

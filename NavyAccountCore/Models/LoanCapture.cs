using System;
using System.Collections.Generic;
using System.Text;

namespace NavyAccountCore.Models
{
    public class LoanCapture
    {

        public string SVC_NO { get; set; }

        public string RANK { get; set; }
        public string SURNAME { get; set; }

        public string OTHERNAME { get; set; }

        public string LOAN_TYPE { get; set; }
      
        public decimal AMOUNT { get; set; }

        public int TENURE { get; set; }
        public string STATUS { get; set; }

        public string BANK { get; set; }
        public string CHEQUE_NO { get; set; }
        public string BATCH_NO { get; set; }

        public string VOUCHER_NO { get; set; }
        public string MONTHS_PAID { get; set; }
        
    }
}

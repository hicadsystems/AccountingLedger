using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NavyAccountWeb.ViewModels
{
    public class LoanRegisterViewModel
    {
        public int Id { get; set; }
        public string SvcNo { get; set; }
        public int loantype { get; set; }
        public int fundtype { get; set; }

        [DataType(DataType.Date)]
        public Nullable<System.DateTime> Approve_date { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string tenure { get; set; }
        public string cheque_no { get; set; }
        public string bankcode { get; set; }
        public string batchNo { get; set; }

        [DataType(DataType.Date)]
        public Nullable<System.DateTime> effective_date { get; set; }
        public string status { get; set; }

        [DataType(DataType.Date)]
        public Nullable<System.DateTime> ExpDate { get; set; }
        public string voucher_no { get; set; }
    }
}
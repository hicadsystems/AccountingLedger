using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.ViewModels
{
    public class StudentClaimViewModel
    {
        public int? studentid { get; set; }
        public string Reg_Number { get; set; }
        public string StudentName { get; set; }
        public string SchoolName { get; set; }
        public string ClassName { get; set; }
        public string VoucherNo { get; set; }
        public decimal? ClaimAmount { get; set; }
        public decimal? AmountToDate { get; set; }
        public string session { get; set; }
        public string Term { get; set; }
        public DateTime? TransDate { get; set; }


    }
    public class StudentPayViewModel
    {
        public int? studentid { get; set; }
        public string Reg_Number { get; set; }
        public string StudentName { get; set; }
        public string SchoolName { get; set; }
        public string ClassName { get; set; }
        public decimal? Amount { get; set; }
        public string session { get; set; }
        public string Term { get; set; }
        public DateTime? TransDate { get; set; }


    }
    public class PaymentAndCalimReport
    {
        public List<StudentClaimViewModel> claimRecord { get; set; }
        public List<StudentPayViewModel> paymentRecord { get; set; }
    }

}

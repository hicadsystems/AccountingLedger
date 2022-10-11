using System.Collections.Generic;

namespace NavyAccountWeb.Models
{
    public class ClaimPaymentReport
    {
        public string Reg_Number { get; set; }
        public string Surname { get; set; }
        public string Sex { get; set; }
        public int? Age { get; set; }
        public string ParentName { get; set; }
        public string ClassName { get; set; }
        public string Schoolname { get; set; }
        public string SchoolType { get; set; }
        public decimal? ClaimAmount { get; set; }
        public decimal? FeeAmount { get; set; }
        public string Period { get; set; }
    }

    public class ClaimPaymentReportExcel
    {
        public string Reg_Number { get; set; }
        public string Surname { get; set; }
        public string Sex { get; set; }
        public int? Age { get; set; }
        public string ParentName { get; set; }
        public string ClassName { get; set; }
        public string Schoolname { get; set; }
        public string SchoolType { get; set; }
        public decimal? ClaimAmount { get; set; }
        public decimal? FeeAmount { get; set; }
        public string Period { get; set; }
    }

    public class ClaimPaymentRecordReport
    {
        public List<ClaimPaymentReport> distintrecord { get; set; }
        public List<ClaimPaymentReport> allRecord { get; set; }
    }
}

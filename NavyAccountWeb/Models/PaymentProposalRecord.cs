using System.Collections.Generic;

namespace NavyAccountWeb.Models
{
    public class PaymentProposalRecord
    {
        public string Req_Number { get; set; }
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Schoolname { get; set; }
        public string SchoolCity { get; set; }
        public string SchoolType { get; set; }
        public decimal? Amount { get; set; }
    }

    public class PaymentPoposalExcelRecord
    {
        public string Req_Number { get; set; }
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Schoolname { get; set; }
        public decimal? Amount { get; set; }
    }

    public class StudentPaymentProposal
    {
        public List<PaymentProposalRecord> distintrecord { get; set; }
        public List<PaymentProposalRecord> studentRecord { get; set; }
    }
}

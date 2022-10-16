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

    public class PaymentProposalRecord2
    {

        public int id { get; set; }
        public string name { get; set; }
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

    public class SchoolStudentRecord 
    {
        public string school { get; set; }
        public int strength { get; set; }
        public int studentCount  { get; set; }
    }

    public class SchoolStudentRecordModel
    {
        public string schoolName { get; set; }
        public string schoolType { get; set; }
        public int SchoolCount { get; set; }
        public int ClaimCount { get; set; }
        public string session { get; set; }
    }




    public class DeleteStudentPaymentproposal 
    {
        public string Req_Number { get; set; }
    }

}

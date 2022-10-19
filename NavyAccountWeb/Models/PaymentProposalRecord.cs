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
        public string Period { get; set; }
        public string Term { get; set; }
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
        public string school { get; set; }
        public int strength { get; set; }
        public string Reg_Number { get; set; }
    }

    public class DefaulterModel 
    {
        public string Reg_Number { get; set; }
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string SchoolName { get; set; }
        public string Period { get; set; }
        public string Term { get; set; }
        public decimal Amount { get; set; }
    }


    public class DefaulterViewModel 
    {
        public List<DefaulterModel> distinctRecord { get; set; }
        public List<DefaulterModel> data { get; set; }
    }






    public class DeleteStudentPaymentproposal 
    {
        public string Req_Number { get; set; }
    }

}

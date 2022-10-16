using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NavyAccountCore.Entities
{
    public class sr_StudentRecordHist
    {
        [Key]
        public int id { get; set; }
        public string Reg_Number {set;get;}
        public string Surname {set;get;}
        public string FirstName {set;get;}
        public int Parentid {set;get;}
        public int Guardianid { set; get; }
        public int ClassId { set; get; }
        public int SchoolId { set; get; }
        public string MiddleName { set; get; }
        public string Sex {set;get;}
        public int Age { get; set; }
        public string ClassCategory { get; set; }
        public string ParentalStatus { get; set; }
        public string SchoolCode {set;get;}
        public string Period { get; set; }
        public DateTime CommencementDate {set;get;}
        public string Class {set;get;}
        public string PhoneNumber {set;get;}
        public string Email {set;get;}
        public string Status {set;get;}
        public DateTime ExitDate {set;get;}
        public string ExitReason {set;get;}
        public decimal ClaimAmount { set; get; }
        public DateTime ClaimDate { set; get; }


    }
}

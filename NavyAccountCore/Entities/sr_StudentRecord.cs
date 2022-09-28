using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NavyAccountCore.Entities
{
    public class sr_StudentRecord
    {
        [Key]
        public int id { get; set; }
        public string Reg_Number {set;get;}
        public string Surname {set;get;}
        public string FirstName {set;get;}
        public string MiddleName {set;get;}
        public string Sex {set;get;}
        public int Age { get; set; }
        public string ClassCategory { get; set; }
        public string ParentalStatus { get; set; }
        public string SchoolCode {set;get;}
        public string CommencementDate {set;get;}
        public string Class {set;get;}
        public string PhoneNumber {set;get;}
        public string Email {set;get;}
        public string Status {set;get;}
        public string ExitDate {set;get;}
        public string ExitReason {set;get;}

    }
}

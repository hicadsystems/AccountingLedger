using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NavyAccountCore.Entities
{
   public class sr_ParentRecord
    {
        [Key]
        public int id { get; set; }
        public string Reg_Number {get;set;}
        public string Surname {get;set;}
        public string OtherNames {get;set;}
        public string Address {get;set;}
        public string Workclass {get;set;}
        public string PhoneNumber {get;set;}
        public string Email {get;set;}
        public string Status {get;set;}
        public string StatusReason {get;set;}
        public string StatusDate {get;set;}
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}

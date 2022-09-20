using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NavyAccountCore.Entities
{
   public class sr_SchoolRecord
    {
        [Key]
        public int id { get; set; }
        public string SchoolCode {get;set;}
        public string Schoolname {get;set;}
        public string SchoolAddress {get;set;}
        public string SchoolCity {get;set;}
        public string SchoolLGA {get;set;}
        public string SchoolState {get;set;}
        public string SchoolType {get;set;}
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }


    }
}

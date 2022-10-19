using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NavyAccountCore.Entities
{
    public class sr_Defaulter
    {
        [Key]
        public int Id { get; set; }
        public string Reg_Number { get; set; }
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string SchoolName { get; set; }
        public string Period { get; set; }
        public string Term { get; set; }
        public decimal Amount { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
    }
}

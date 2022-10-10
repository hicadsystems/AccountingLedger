using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NavyAccountCore.Entities
{
    public class sr_SchoolFeeTB
    {
        [Key]
        public int id { get; set; }
        public string Period { get; set; }
        public string term { get; set; }
        public string ClassCategory { get; set; }
        public int ClassId { get; set; }
        public int SchoolId { get; set; }
        public decimal Amount { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}

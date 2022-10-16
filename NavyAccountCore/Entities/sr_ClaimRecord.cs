using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NavyAccountCore.Entities
{
   public class sr_ClaimRecord
    {
        [Key]
        public int id { get; set; }
        public string Reg_Number {get;set;}
        public string VoucherNumber { get;set;}
        public string Period { get; set; }
        public string Term { get; set; }
        public int? SchoolId { get; set; }
        public int? ClassId { get; set; }
        public DateTime? Transdate {get;set;}
        public decimal Amount {get;set;}
        public decimal AmountToDate { get; set; }
        public string DeletedBy {get;set;}
        public DateTime? DeletedDate {get;set;}
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }

    }
}

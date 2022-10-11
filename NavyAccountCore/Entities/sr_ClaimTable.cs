using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NavyAccountCore.Entities
{
   public class sr_ClaimTable
    {
        [Key]
        public int id { get; set; }
        public string Reg_Number {get;set;}
        public decimal Amount {get;set;}



    }
}

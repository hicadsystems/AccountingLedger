using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NavyAccountCore.Models
{
   public class ClaimRecordVM
    {

        public string Reg_Number {get;set;}
        public string Class { get; set; }
        public string School { get; set; }
        public string Term { get; set; }
        public string Period { get; set; }
        public string remark { get; set; }
        public decimal amount { get; set; } = 0M;



    }
}

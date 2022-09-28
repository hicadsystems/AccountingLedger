using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NavyAccountCore.Entities
{
   public class sr_ClassRecord
    {
        [Key]
        public int id { get; set; }
        public string ClassName {get;set;}
        public string SchoolType { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NavyAccountCore.Entities
{
    public class sr_SchoolRecordControl
    {
        [Key]
        public int id { get; set; }
        public string Period { get; set; }
        public string term { get; set; }

        public string Name { get; set; }
        public int SchoolCount { get; set; }
    }
}

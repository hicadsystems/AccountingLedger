using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NavyAccountCore.Entities
{
    public class SchoolFeeVM
    {
        public int id { get; set; }
        public string Period { get; set; }
        public string ClassCategory { get; set; }
        public int ClassId { get; set; }
        public int SchoolId { get; set; }
        public string ClassName { get; set; }
        public string SchoolName { get; set; }
        public decimal Amount { get; set; }
        public string term { get; set; }

    }
    public class SchoolFeeVM2
    {
        public string Period { get; set; }
        public string SchoolType { get; set; }
        public string ClassName { get; set; }
        public string SchoolName { get; set; }
        public decimal Amount { get; set; }
        public string Term { get; set; }

    }
}

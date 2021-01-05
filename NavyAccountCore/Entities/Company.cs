using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NavyAccountCore.Entities
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        public string code { get; set; }
        public string description { get; set; }
    }
}

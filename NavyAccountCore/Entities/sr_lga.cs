using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NavyAccountCore.Entities
{
    public class sr_lga
    {
        [Key]
        public int id { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public string statecode { get; set; }
        public int stateid { get; set; }
    }
}


namespace NavyAccountCore.Core.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class loanPerRank
    {
        [Key]
        public int Id { get; set; }
        public string loantype { get; set; }
        public decimal amount01 { get; set; } 
        public decimal amount02 { get; set; } 
        public decimal amount03 { get; set; } 
        public decimal amount04 { get; set; } 
        public decimal amount05 { get; set; } 
        public decimal amount06 { get; set; } 
        public decimal amount07 { get; set; } 
        public decimal amount08 { get; set; }
        public decimal amount09 { get; set; } 
        public decimal amount10 { get; set; } 
        public decimal amount11 { get; set; } 
        public decimal amount12 { get; set; } 
        public decimal amount13 { get; set; } 
        public decimal amount14 { get; set; } 
        public decimal amount15 { get; set; } 
        public decimal amount16 { get; set; } 
        public decimal amount17 { get; set; } 
        public decimal amount18 { get; set; } 
        public decimal amount19 { get; set; }
        public decimal amount20 { get; set; } 
        public decimal amount21 { get; set; } 
        public decimal amount22 { get; set; } 
        public string Createdby { get; set; } 
        public DateTime Datecreated { get; set; }
    }
}

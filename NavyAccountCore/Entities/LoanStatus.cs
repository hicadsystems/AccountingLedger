using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NavyAccountCore.Core.Entities
{
    public class LoanStatus
    {
        [Key]
        public int Id { get; set; }

        public string Description { get; set; }
        
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NavyAccountCore.Entities
{
    public class npf_LoanTypeReview
    {
        [Key]
        public int Id { get; set; }
        public string LoanType { get; set; }
        public DateTime ReviewDate { get; set; }
        public int Interestrate { get; set; }
    }
}

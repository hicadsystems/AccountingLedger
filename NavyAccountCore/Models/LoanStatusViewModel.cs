using System;
using System.Collections.Generic;
using System.Text;

namespace NavyAccountCore.Models
{
   public class LoanStatusViewModel
    {
        public int Id { get; set; }
        public string code { get; set; }
        public int personid { get; set; }
        public string description { get; set; }
        public string batchno { get; set; }
    }
}

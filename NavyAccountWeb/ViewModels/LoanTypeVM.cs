using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NavyAccountWeb.ViewModels
{
    public class LoanTypeVM
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Tenure { get; set; }
        public string Intrest { get; set; }
        public int FundType { get; set; }

        public string FundDescription { get; set; }
    }
}
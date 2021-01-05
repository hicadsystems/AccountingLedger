using NavyAccountCore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.ViewModels
{
    public class PfaFundRateViewModel
    {
        public int Id { get; set; }

        [Required]
        public string FundCode { get; set; }

        [Required]
        public string Period { get; set; }

        [Required]
        public string Interest { get; set; }

        [Required]
        public string Rate { get; set; }

        public Nullable<System.DateTime> datecreated { get; set; }
        public string createdby { get; set; }

        public IEnumerable<fundtypeView> GetAllFundType { get; set; }
    }
}

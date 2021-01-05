using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.Models
{
    public class balanceViewModel
    {
        public string Indicator { get; set; }
        public string value { get; set; }
        public bool excel { get; set; }

        [DataType(DataType.Date)]
        public DateTime startdate { get; set; }

        [DataType(DataType.Date)]
        public DateTime enddate { get; set; }
    }
}

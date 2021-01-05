using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NavyAccountWeb.ViewModels
{
    public class InvestmentRegisterViewModel
    {
        public int Id { get; set; }
        public string InvBank { get; set; }
        public string User { get; set; }
        public string Voucher { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }

        public Nullable<System.DateTime> Date { get; set; }

        [DataType(DataType.Date)]
        public Nullable<System.DateTime> Due_Date { get; set; }

        public string Investment_Type { get; set; }
    }
}
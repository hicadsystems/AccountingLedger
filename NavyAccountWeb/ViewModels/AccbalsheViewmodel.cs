using NavyAccountCore.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NavyAccountWeb.ViewModels
{
    public class AccbalsheViewmodel
    {
        public int Id { get; set; }
        public string bl_code { get; set; }
        public string bl_desc { get; set; }
        public Nullable<System.DateTime> datecreated { get; set; }
        public string createdby { get; set; }

        public IEnumerable<npf_balsheet> accbalsheets { get; set; }
    }
   
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NavyAccountWeb.ViewModels
{
    public class MainActViewModel
    {
        public int Id { get; set; }
        public string maincode { get; set; }
        public string description { get; set; }
        public int balsheetid { get; set; }
        public System.DateTime datecreated { get; set; }
        public string createdby { get; set; }
    }
}
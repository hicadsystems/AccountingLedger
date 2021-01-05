using NavyAccountCore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.ViewModels
{
    public class AuditViewModel
    {
        public string acctcode { get; set; }
        public string year { get; set; }
        public string Remark { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime startdate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime enddate { get; set; }

        public IEnumerable<LedgersView> loadAllNpfChart { get; set; }
        public IEnumerable<LedgersView> loadAllYear { get; set; }

    }
}

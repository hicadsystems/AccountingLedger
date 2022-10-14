using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.Models
{
    public class SchoolFilterModels
    {
        public class Searchby_Name
        {
            public int Id { get; set; }
            public string name { get; set; }
        }

        public class StudentFilterModel
        {
            public int? SchoolId { get; set; }
            public int? ClassId { get; set; }
            public string Status { get; set; }
            public string ParentalStatus { get; set; }
            public DateTime? CommencementDate { get; set; }
            public string sortby { get; set; }
        }
    }
}

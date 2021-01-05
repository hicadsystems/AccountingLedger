using NavyAccountCore.Core.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NavyAccountCore.Models
{
    [JsonObject]
    public class PersonListViewModel
    {
        public int PersonID { get; set; }
        public string SVC_NO { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Initials { get; set; }
        public string Title { get; set; }
        public string Sex { get; set; }
        public string email { get; set; }
        public string homeaddress { get; set; }
        public string rank { get; set; }

        public System.DateTime BirthDate { get; set; }
        public Nullable<DateTime> dateemployed { get; set; }
        public Nullable<DateTime> dateleft { get; set; }
        public string Phone1 { get; set; }
        public string bank { get; set; }
        public int bankid { get; set; }
        public string accountno { get; set; }

        public int rankid { get; set; }

    }

    public class PersonListID_Name {
        public int Id { get; set; }
        public string name { get; set; }
    }
}

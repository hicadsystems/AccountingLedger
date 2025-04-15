using NavyAccountCore.Core.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NavyAccountCore.Models
{
    public class PersonUploadModel
    {
        public int SRN { get; set; }
        public string SVC_NO { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Sex { get; set; }
        public string Email { get; set; }
        public string HomeAddress { get; set; }
        public string Rank { get; set; }

        public System.DateTime BirthDate { get; set; }
        public Nullable<DateTime> DateEmployed { get; set; }
        public string PhoneNumber { get; set; }
        public string Bank { get; set; }
        public string AccountNo { get; set; }

    }
}

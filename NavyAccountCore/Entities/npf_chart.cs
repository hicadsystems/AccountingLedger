//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NavyAccountCore.Core.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class npf_chart
    {
        [Key]
        public int Id { get; set; }
        public string acctcode { get; set; }
        public string description { get; set; }
        public string mainAccountCode { get; set; }

        public string subtype { get; set; }

        public bool ispersonel { get; set; }

        public string balSheetCode { get; set; }
        public Nullable<System.DateTime> datecreated { get; set; }
        public string createdby { get; set; }
    }
}

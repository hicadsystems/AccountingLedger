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

    public class Pf_fundRate
    {
        [Key]
        public int Id { get; set; }
        public string FundCode { get; set; }
        public string Period { get; set; }
        public string Interest { get; set; }
        public string Rate { get; set; }
        public Nullable<System.DateTime> datecreated { get; set; }
        public string createdby { get; set; }
    }
}
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

    public  class Pf_loanType
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Tenure { get; set; }
        public string Interest { get; set; }
        public Nullable<int> FundTypeID { get; set; }
        public string liabilityacct { get; set; }
        public string interestacct { get; set; }
        public string incomeacct { get; set; }
        public string loanacct { get; set; }
        public string trusteeacct { get; set; }
        public Nullable<System.DateTime> datecreated { get; set; }
        public string createdby { get; set; }
    }
}

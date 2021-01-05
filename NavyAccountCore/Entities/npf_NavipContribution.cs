using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NavyAccountCore.Core.Entities
{
    public class npf_NavipContribution
    {
        public int Id { get; set; }
        
        public string svcno { get; set; }
        [StringLength(30)]
        public string Batch { get; set; }
        public int PersonID { get; set; }
        public Nullable<DateTime> prmdate01 { get; set; }
      
        public Nullable<DateTime> prmdate02 { get; set; }
        
        public Nullable<DateTime> prmdate03 { get; set; }
        
        public Nullable<DateTime> prmdate04 { get; set; }
       
        public Nullable<DateTime> prmdate05 { get; set; }
       
        public Nullable<DateTime> prmdate06 { get; set; }

        public Nullable<DateTime> prmdate07 { get; set; }
        
        public Nullable<DateTime> prmdate08 { get; set; }
       
        public Nullable<DateTime> prmdate09 { get; set; }
        
        public Nullable<DateTime> prmdate10 { get; set; }
        
        public Nullable<DateTime> prmdate11 { get; set; }
        
        public Nullable<DateTime> prmdate12 { get; set; }
       
        public Nullable<DateTime> prmdate13 { get; set; }
        
        public Nullable<DateTime> prmdate14 { get; set; }
        
        public Nullable<DateTime> prmdate15 { get; set; }
       
        public Nullable<DateTime> prmdate16 { get; set; }

        public Nullable<DateTime> prmdate17 { get; set; }
        
        public Nullable<DateTime> prmdate18 { get; set; }
        
        public Nullable<DateTime> prmdate19 { get; set; }
        
        public Nullable<DateTime> prmdate20 { get; set; }

        public Nullable<DateTime> prmdate21 { get; set; }
    
        public Nullable<DateTime> prmdate22 { get; set; }
        public string remark { get; set; }
        public int bank { get; set; }
        public string acctno { get; set; }
        public string beneficiary { get; set; }
        public int title { get; set; }

        public Nullable<DateTime> datecreated { get; set; }
        public string createdby { get; set; }
    }
  
}

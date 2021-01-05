using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NavyAccountCore.Models
{
    public class NavipVM
    {
        public int Id { get; set; }
        public string svcno { get; set; }
        public string amount01 { get; set; }
        public string amount02 { get; set; }
        public string amount03 { get; set; }
        public string amount04 { get; set; }
        public string amount05 { get; set; }
        public string amount06 { get; set; }
        public string amount07 { get; set; }
        public string amount08 { get; set; }
        public string amount09 { get; set; }
        public string amount10 { get; set; }
        public string amount11 { get; set; }
        public string amount12 { get; set; }
        public string amount13 { get; set; }
        public string amount14 { get; set; }
        public string amount15 { get; set; }
        public string amount16 { get; set; }
        public string amount17 { get; set; }
        public string amount18 { get; set; }
        public string amount19 { get; set; }
        public string amount20 { get; set; }
        public string amount21 { get; set; }
        public string amount22 { get; set; }
    

    }
    public class navipVM2
    {

        public int Id { get; set; }
        public string Batch { get; set; }
        public string svcno { get; set; }
        public string Name { get; set; }
        public string rank { get; set; }
        public DateTime reviewdate1 { get; set; }
        public DateTime reviewdate2 { get; set; }
        public decimal surval { get; set; }
        public int smonth { get; set; }
        public DateTime calcdate1 { get; set; }
        public DateTime caldate2 { get; set; }
        public string exitdate { get; set; }
        public string dateemp { get; set; }
        public decimal totalsurval { get; set; }
        public decimal survalperc { get; set; }
        public decimal grandsuvaltotal { get; set; }
        public string remark { get; set; }
        public string bank { get; set; }
        public int acctno { get; set; }
        public DateTime datecreated { get; set; }
        public string createdby { get; set; }


    }
    public class navipVM4
    {
        public string rank { get; set; }
        public string Name { get; set; }
        public string svcno { get; set; }
        public string bank { get; set; }
        public int acctno { get; set; }

        public decimal totalsurval { get; set; }
        public decimal survalperc { get; set; }
        public decimal grandsuvaltotal { get; set; }
        public string remark { get; set; }



    }
    public class navipVM5
    {
        public string rank { get; set; }
        public string Name { get; set; }
        public string svcno { get; set; }
        public string bank { get; set; }
        public string acctno { get; set; }
        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal grandsuvaltotal { get; set; }
        public string remark { get; set; }
        public string batchno { get; set; }
        



    }
    public class NavipVM3
    {

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
        public int bank { get; set; }
        public string acctno { get; set; }
        public string beneficiary { get; set; }
        public string SVC_NO { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public int title2 { get; set; }
        public Nullable<DateTime> dateemployed { get; set; }

        public Nullable<DateTime> datecreated { get; set; }
        public string createdby { get; set; }
    }
}

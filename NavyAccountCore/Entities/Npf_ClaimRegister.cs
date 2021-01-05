using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NavyAccountCore.Core.Entities
{
    public class Npf_ClaimRegister
    {

        [Key]
        public int Id { get; set; }

        public int PersonID { get; set; }
        public string FundTypeID { get; set; }

        [StringLength(12)]
        public string svcno { get; set; }
        public DateTime appdate { get; set; }

        [StringLength(12)]
        public string status { get; set; }
        public DateTime statusdate { get; set; }

        public decimal TotalContribution { get; set; }
        public decimal interest { get; set; }
        public decimal loan { get; set; }

        [StringLength(60)]
        public string Remark { get; set; }
        public string Beneficiary { get; set; }
        public decimal AmountDue { get; set; }

        [StringLength(20)]
        public string chequeno { get; set; }
        public decimal amountPaid { get; set; }
        public decimal amountReceived { get; set; }
        public string BatchNo { get; set; }
        public int bank { get; set; }
        public string acctno { get; set; }
        public int title { get; set; }

    }
}

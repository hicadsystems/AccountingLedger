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

    public class Pf_InvestRegister
    {
        [Key]
        public int Id { get; set; }
        public string company { get; set; }
        public string unit { get; set; }
        public Nullable<int> IssuanceBankId { get; set; }
        public Nullable<int> receivingBankId { get; set; }
        public Nullable<int> StockId { get; set; }
        public string TransactionType { get; set; }
        public string Voucher { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public Nullable<System.DateTime> Maturingdate { get; set; }
        public string Tenure { get; set; }
        public string InvestmentType { get; set; }
        public string closecode { get; set; }
        public string interest { get; set; }
        public string chequeno { get; set; }
        public Nullable<decimal> maturedamt { get; set; }
        public Nullable<System.DateTime> datecreated { get; set; }
        public string createdby { get; set; }
    }
}

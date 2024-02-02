using System;

namespace NavyAccountWeb.ViewModels
{
    public class InvestmentReportModel
    {
        public string InvestmentType {get;set;}
        public string receivingbank {get;set;}
        public string issuancebank {get;set;}
        public string company {get;set;}
        public decimal? Amount {get;set;}
        public string interest {get;set;}
        public string Tenure { get; set; }
        public string Voucher { get; set; }
        public decimal? MaturedAmount { get; set; }
        
        public DateTime? Maturingdate {get;set;}
        public DateTime? DueDate { get; set; }
    }
    public class InvestmentReportModel2
    {
        public string InvestmentType {get;set;}
        public string StockName {get;set;}
        public string TransactionType {get;set;}                        
        public string unit {get;set;}
        public DateTime? Date {get;set;}
        public string issuancebank { get; set; }
        public string company { get; set; }
        public decimal? Amount { get; set; }

    }
}

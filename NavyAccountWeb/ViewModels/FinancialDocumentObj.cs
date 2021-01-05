using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.ViewModels
{
    public class FinancialDocumentObj
    {
        public string documentNo { get; set; }

        public string documentDate { get; set; }
        public string fundCode { get; set; }

        public string documentType { get; set; }

        public string referenceNo { get; set; }


        public List<ItemTransact> transactionsCR { get; set; }
        public List<ItemTransact> transactionsDB { get; set; }

    }

    public class ItemTransact {
        public string code { get; set; }

        public decimal debitAmount { get; set; }

        public decimal creditAmount { get; set; }
       public string trasactiontype { get; set; }

        public string remarks { get; set; }

    }
}

using NavyAccountCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.IServices
{
    public interface IFinancialDocService
    {
        IEnumerable<DocumentEnquiryVM> GetbyFinancialdocByRpt(string reciept);
        IEnumerable<DocumentEnquiryVM> GetbyFinancialdocByRpt2(string reciept);
    }
}

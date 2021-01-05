using NavyAccountCore.Core.Entities;
using NavyAccountCore.Core.IRepositories;
using NavyAccountCore.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NavyAccountCore.Core.IRepositories
{
   public interface IFinancialDocRepo : IRepository<npf_history>
    {
        IEnumerable<DocumentEnquiryVM> GetbyFinancialdocByRpt(string reciept);
        IEnumerable<DocumentEnquiryVM> GetbyFinancialdocByRpt2(string reciept);
        npf_history GetHistory(Expression<Func<npf_history, bool>> predicate);
        IEnumerable<DocumentEnquiryVM> GetHistoryDistinct();

    }
}

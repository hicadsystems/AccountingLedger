using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Entities;
using NavyAccountCore.Core.IRepositories;
using NavyAccountCore.Core.Repositories;
using NavyAccountCore.Entities;
using NavyAccountCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;

namespace NavyAccountCore.Core.Repositories
{
    public class FinancialDocRepo : Repository<npf_history>, IFinancialDocRepo
    {
        private readonly INavyAccountDbContext context;
        public FinancialDocRepo(INavyAccountDbContext context) : base(context)
        {
            this.context = context;
        }
        public npf_history GetHistory(Expression<Func<npf_history, bool>> predicate)
        {

            return context.npf_Histories.FirstOrDefault(predicate);
        }
        public IEnumerable<DocumentEnquiryVM> GetHistoryDistinct()
        {

            return (from rpt in context.npf_Histories
                    select new DocumentEnquiryVM
                    {
                        docno = rpt.docno
                    }
                    ).Distinct().ToList();
        }

        public IEnumerable<DocumentEnquiryVM> GetbyFinancialdocByRpt(string reciept)
        {
            return (from npfh in context.npf_Histories
                    join per in context.npf_Charts on npfh.acctcode equals per.acctcode
                    where (npfh.docno == reciept )
                    select new DocumentEnquiryVM
                    {
                        Id = npfh.Id,
                        docno = npfh.docno,
                        acctcode=npfh.acctcode,
                        accountname = per.description,
                        dbamt = npfh.dbamt,
                        cramt = npfh.cramt,
                        remarks = npfh.remarks,
                      
                    }).ToList();
        }
        public IEnumerable<DocumentEnquiryVM> GetbyFinancialdocByRpt2(string reciept)
        {
            return (from npfh in context.npf_Histories
                    join per in context.npf_Charts on npfh.acctcode equals per.acctcode
                    where (npfh.batchNo == reciept)
                    select new DocumentEnquiryVM
                    {
                        Id = npfh.Id,
                        docno = npfh.docno,
                        acctcode = npfh.acctcode,
                        accountname = per.description,
                        dbamt = npfh.dbamt,
                        cramt = npfh.cramt,
                        remarks = npfh.remarks,

                    }).ToList();
        }
    }
}

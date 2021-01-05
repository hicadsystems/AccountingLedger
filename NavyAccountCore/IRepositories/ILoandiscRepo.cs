using NavyAccountCore.Core.IRepositories;
using NavyAccountCore.Entities;
using NavyAccountCore.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NavyAccountCore.Core.IRepositories
{
    public interface ILoandiscRepo : IRepository<pf_loandisc>
    {
        pf_loandisc GetloanDiscByCode(Expression<Func<pf_loandisc, bool>> predicate);
        IEnumerable<LoandiscVM> Getbyfundcode(string fundcode, string batch);
        IEnumerable<LoandiscVM> Getbyfundcode(string fundcode);
        IEnumerable<LoandiscVM> Getbyfundcodeandsvcno(string fundcode, string svcno);
        pf_loandisc GetloanDiscByBatch(string loanacct, string batchno);
        Task<List<pf_loandisc>> GetloanDiscByBatchdrp();
    }
}

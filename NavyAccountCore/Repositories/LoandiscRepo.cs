using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.IRepositories;
using NavyAccountCore.Entities;
using NavyAccountCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MoreLinq;
using System.Data.SqlTypes;

namespace NavyAccountCore.Core.Repositories
{
    public class LoandiscRepo : Repository<pf_loandisc>, ILoandiscRepo
    {

        private readonly INavyAccountDbContext context;
        public LoandiscRepo(INavyAccountDbContext context) : base(context)
        {
            this.context = context;
        }
        public pf_loandisc GetloanDiscByCode(Expression<Func<pf_loandisc, bool>> predicate)
        {

            return context.pf_loandisc.FirstOrDefault(predicate);
        }
        public IEnumerable<LoandiscVM> Getbyfundcode(string fundcode,string batch)
        {
            return (from loandisc in context.pf_loandisc
                    join per in context.npf_Charts on loandisc.loanact equals per.acctcode
                    where (loandisc.fundcode == fundcode && loandisc.loantype!="0" && loandisc.batchno==batch)
                    select new LoandiscVM
                    {
                        count=0,
                        Id= loandisc.Id,
                        personname=per.description,
                        period=loandisc.period,
                        interest=loandisc.interest,
                        loanappno=loandisc.loanappno,
                        principal = loandisc.principal,
                        prvloan = loandisc.prvloan,
                        loanvar=loandisc.loanvar,
                        extpay=loandisc.extpay,
                        loanpay = loandisc.loanpay,
                        loanact = loandisc.loanact,
                        loandiscr=loandisc.loandiscr
                    }).ToList();
        }
        public IEnumerable<LoandiscVM> Getbyfundcode(string fundcode)
        {
            return (from loandisc in context.pf_loandisc
                    join per in context.npf_Charts on loandisc.loanact equals per.acctcode
                    where (loandisc.fundcode == fundcode && loandisc.loantype != "0")
                    select new LoandiscVM
                    {
                        count = 0,
                        Id = loandisc.Id,
                        personname = per.description,
                        period = loandisc.period,
                        interest = loandisc.interest,
                        loanappno = loandisc.loanappno,
                        principal = loandisc.principal,
                        prvloan = loandisc.prvloan,
                        loanvar = loandisc.loanvar,
                        extpay = loandisc.extpay,
                        loanpay = loandisc.loanpay,
                        loanact = loandisc.loanact,
                        loandiscr = loandisc.loandiscr
                    }).ToList();
        }
        public IEnumerable<LoandiscVM> Getbyfundcodeandsvcno(string fundcode,string svcno)
        {
           var d=(from loandisc in context.pf_loandisc
                    join per in context.npf_Charts on loandisc.loanact equals per.acctcode
                    where (loandisc.fundcode == fundcode && loandisc.loantype != "0" && loandisc.loanact.Substring(5, 8)==svcno)
                    select new LoandiscVM
                    {
                        count = 0,
                        Id = loandisc.Id,
                        personname = per.description,
                        period = loandisc.period,
                        interest = loandisc.interest,
                        loanappno = loandisc.loanappno,
                        principal = loandisc.principal,
                        prvloan = loandisc.prvloan,
                        loanvar = loandisc.loanvar,
                        extpay = loandisc.extpay,
                        loanpay = loandisc.loanpay,
                        loanact = loandisc.loanact,
                        loandiscr = loandisc.loandiscr
                    }).ToList();
            return d;
        }
        public pf_loandisc GetloanDiscByBatch(string loanacct,string batchno)
        {

            return context.pf_loandisc.Where(x=>x.batchno==batchno && x.loanact==loanacct).FirstOrDefault();
        }
        public async Task<List<pf_loandisc>> GetloanDiscByBatchdrp()
        {
            var ba = await context.pf_loandisc.ToListAsync();
            return ba.DistinctBy(x => x.batchno).ToList();
        }

    }
}

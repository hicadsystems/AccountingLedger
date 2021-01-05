using Microsoft.EntityFrameworkCore;
using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Entities;
using NavyAccountCore.Core.IRepositories;
using NavyAccountCore.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using MoreLinq.Extensions;

namespace NavyAccountCore.Core.Repositories
{
    public class NavipRepository :Repository<npf_NavipContribution>, INavipRepo
    {
        private readonly INavyAccountDbContext context;
        public NavipRepository(INavyAccountDbContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<npf_NavipContribution> GetNavipByCode(Expression<Func<npf_NavipContribution, bool>> predicate)
        {
            return await context.npf_NavipContribution.FirstOrDefaultAsync(predicate);
        }
        public List<navipVM2> getnavipbybatch(string batch)
        {
            var result = new List<navipVM2>();
            var navips=  (from navip in context.npf_navip
                          join per in context.person on navip.svcno equals per.SVC_NO
                          join rk in context.ranks on navip.title equals rk.Id
                          join bk in context.py_Banks on navip.bank equals bk.Id
                          where navip.Batch == batch
                          select new navipVM2
                          {
                              svcno = navip.svcno,
                              Name = per.LastName + " " + per.FirstName,
                              remark="Navip Refund For "+ rk.Description+" "+ per.LastName + " " + per.FirstName+" "+ navip.svcno,
                              acctno=navip.acctno,
                              bank=bk.bankname,
                              rank =rk.Description,
                              reviewdate1=navip.reviewdate1,
                              reviewdate2=navip.reviewdate2,
                              calcdate1=navip.calcdate1,
                              caldate2=navip.caldate2,
                              exitdate=per.dateleft.ToString(),
                              dateemp=per.dateemployed.ToString(),
                              surval=Math.Round(navip.surval,2),
                              
                          }).DistinctBy(x=>x.svcno).ToList();
           
            foreach(var s in navips)
            {
                var d = new navipVM2
                {
                    svcno = s.svcno,
                    Name = s.Name,
                    remark=s.remark,
                    acctno=s.acctno,
                    bank=s.bank,
                    rank = s.rank,
                    reviewdate1 = s.reviewdate1,
                    reviewdate2 = s.reviewdate2,
                    calcdate1 = s.calcdate1,
                    caldate2 = s.caldate2,
                    exitdate = s.exitdate,
                    dateemp = s.dateemp.ToString(),
                    surval = s.surval,
                    totalsurval =gettotalval(s.svcno),
                 survalperc=getperval(s.svcno),
                 };
                d.grandsuvaltotal = d.totalsurval + d.survalperc;
                result.Add(d);
            }
            return result.ToList();
           
        }
        public List<navipVM4> getnavipbydate(DateTime startdate, DateTime enddate)
        {
            var result = new List<navipVM4>();
            var navips = (from navip in context.npf_navip
                          join per in context.person on navip.svcno equals per.SVC_NO
                          join rk in context.ranks on navip.title equals rk.Id
                          join bk in context.py_Banks on navip.bank equals bk.Id
                          where (navip.datecreated.Date >= startdate.Date && navip.datecreated.Date <= enddate.Date)
                          select new navipVM4
                          {
                              svcno = navip.svcno,
                              Name = per.LastName + " " + per.FirstName,
                              remark = "Navip Refund For " + rk.Description + " " + per.LastName + " " + per.FirstName + " " + navip.svcno,
                              acctno = navip.acctno,
                              bank = bk.bankname,
                              rank = rk.Description,
            

                          }).DistinctBy(x => x.svcno).ToList();

            foreach (var s in navips)
            {
                var d = new navipVM4
                {
                    svcno = s.svcno,
                    Name = s.Name,
                    remark = s.remark,
                    acctno = s.acctno,
                    bank = s.bank,
                    rank = s.rank,
                    totalsurval = gettotalval(s.svcno),
                    survalperc = getperval(s.svcno),
                };
                d.grandsuvaltotal = d.totalsurval + d.survalperc;
                result.Add(d);
            }
            return result.ToList();

        }
        public decimal gettotalval(string svcno)
        {
            var d= context.npf_navip.ToList().Where(x => x.svcno == svcno);
            decimal totalval = d.Sum(x => x.surval);
            return totalval;
        }
        public decimal getperval(string svcno)
        {
            var d = context.npf_navip.Where(x => x.svcno == svcno).ToList();
            decimal totalval = d.Sum(x => x.surval);
            decimal totaper = 12 * totalval / 100;
            return totaper;
        }
        public List<navipVM5> getnavipbydate2(DateTime startdate, DateTime enddate)
        {
            var result = new List<navipVM5>();
            result=(from navip in context.Npf_ClaimRegisters
                          join per in context.person on navip.svcno equals per.SVC_NO
                          join rk in context.ranks on navip.title equals rk.Id
                          join bk in context.py_Banks on navip.bank equals bk.Id
                          where (navip.appdate.Date >= startdate.Date && navip.appdate.Date <= enddate.Date) && navip.FundTypeID=="10"
                          select new navipVM5
                          {
                              svcno = navip.svcno,
                              Name = per.LastName + " " + per.FirstName,
                              remark = "Navip Refund For " + rk.Description + " " + per.LastName + " " + per.FirstName + " " + navip.svcno,
                              acctno =navip.acctno,
                              bank = bk.bankname,
                              rank = rk.Description,
                              grandsuvaltotal=Math.Round(navip.AmountDue+(navip.AmountDue*navip.interest/100),2),
                              batchno=navip.BatchNo


                          }).ToList();
            return result;
        }

    }
}

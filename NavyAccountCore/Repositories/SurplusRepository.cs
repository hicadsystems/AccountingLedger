using NavyAccountCore.Core.Data;
using NavyAccountCore.Models;
using System.Linq;
using System;
using System.Collections.Generic;
using NavyAccountCore.Core.IRepositories;
using Microsoft.EntityFrameworkCore.Internal;
using MoreLinq;


namespace NavyAccountCore.Core.Repositories
{
    public class SurplusRepository : ISuplusRepo
    {
        private readonly INavyAccountDbContext context;
        public SurplusRepository(INavyAccountDbContext context)
        {
            this.context = context;
        }
        public IEnumerable<LedgersView2> GetAllSurplusOrDeficit()
        {
            var op = (from p in context.npf_Ledgers
                      join q in context.npf_Charts on p.acctcode equals q.acctcode
                      join r in context.mainacts on q.mainAccountCode equals r.maincode
                      join s in context.npf_Balsheets on r.npf_balsheet_bl_code equals s.bl_code
                      select new LedgersView2
                      {
                          acctcode=p.acctcode,
                          opbalance=p.opbalance,
                          adbbalance=p.adbbalance,
                          crbalance=p.crbalance,
                          Amount=p.opbalance+p.adbbalance-p.crbalance,
                          description=q.description,
                          MDesc=r.description,
                          bl_desc=s.bl_desc,
                          balSheetCode=s.bl_code
                      });

            var jk = new List<LedgersView2>();
         
            foreach (var j in op)
            {
                var s = j.acctcode.Substring(0, 1);
                if (int.Parse(s)>3)
                {
                    var kl = new LedgersView2
                    {
                        acctcode = j.acctcode,
                        opbalance = j.opbalance,
                        adbbalance = j.adbbalance,
                        crbalance = j.crbalance,
                        description = j.description,
                        MDesc = j.MDesc,
                        Amount = j.Amount*-1,
                        bl_desc = j.bl_desc,
                        balSheetCode=j.balSheetCode
                    };

                    jk.Add(kl);
                }
               
            }
            var kj = jk.DistinctBy(x => x.acctcode);
            return kj;
        }

        public IEnumerable<LedgersView2> GetAllSurplusOrDeficit2()
        {
            var op = (from p in context.npf_Histories
                      join q in context.npf_Charts on p.acctcode equals q.acctcode
                      join r in context.mainacts on q.mainAccountCode equals r.maincode
                      join s in context.npf_Balsheets on r.npf_balsheet_bl_code equals s.bl_code
                      select new LedgersView2
                      {
                          acctcode = p.acctcode,
                          docNo=p.docno,
                          adbbalance = p.dbamt,
                          crbalance = p.cramt,
                          Amount =  p.dbamt - p.cramt,
                          description = q.description,
                          MDesc = r.description,
                          bl_desc = s.bl_desc,
                          balSheetCode = s.bl_code
                      }).ToList();

            var jk = new List<LedgersView2>();

            foreach (var j in op)
            {
                var s = j.acctcode.Substring(0, 1);
                if (int.Parse(s) > 3)
                {
                    var kl = new LedgersView2
                    {
                        acctcode = j.acctcode,
                        docNo=j.docNo,
                        adbbalance = j.adbbalance,
                        crbalance = j.crbalance,
                        description = j.description,
                        MDesc = j.MDesc,
                        Amount = j.Amount,
                        bl_desc = j.bl_desc,
                        balSheetCode = j.balSheetCode
                    };

                    jk.Add(kl);
                }

            }
            var kj = jk.DistinctBy(x => x.acctcode);

            return kj;
        }

        public IEnumerable<LedgersView> GetAllSurplusOrdeficitByDoc(string doc,string fundcode )
        {
            return (from p in context.npf_Histories
                    join chart in context.npf_Charts on p.acctcode equals chart.acctcode
                    where (p.fundcode == fundcode && p.docno == doc && p.acctcode.StartsWith("4") || p.acctcode.StartsWith("5"))
                    select new LedgersView
                    {
                        code = p.acctcode,
                        desc = chart.description,
                        crbal = (p.cramt == null) ? 0M : (decimal)p.cramt,
                        dbbal = (p.dbamt == null) ? 0M : (decimal)p.dbamt
                    }).ToList();
        }

        public IEnumerable<LedgersView2> GetBalsheet()
        {
            var op = (from p in context.npf_Ledgers
                      join q in context.npf_Charts on p.acctcode equals q.acctcode
                      join m in context.mainacts on q.mainAccountCode equals m.maincode
                      join s in context.npf_Balsheets on m.npf_balsheet_bl_code equals s.bl_code
                      where m.maincode.StartsWith("1") || m.maincode.StartsWith("2") || m.maincode.StartsWith("3") || m.maincode.StartsWith("4") || m.maincode.StartsWith("5")
                      select new LedgersView2
                      {
                          acctcode = p.acctcode.Split('-')[0],
                          Amount = p.opbalance + p.adbbalance - p.crbalance,
                          description = m.description,
                          bl_desc = s.bl_desc,
                          balSheetCode = s.bl_code,
                          mainAccountCode=p.acctcode.Substring(0,1)
                      });

            return op;
            
        }

        public IEnumerable<LedgersView2> GetBalSheet_TrialBalance() {
            var op = (from p in context.npf_Ledgers
                      join q in context.npf_Charts on p.acctcode equals q.acctcode
                      join mainAccont in context.mainacts on q.mainAccountCode equals mainAccont.maincode
                      select new LedgersView2
                      {
                          acctcode = p.acctcode,
                          opbalance = p.opbalance,
                          adbbalance = p.adbbalance,
                          crbalance = p.crbalance,
                          Amount = p.opbalance + p.adbbalance - p.crbalance,
                          description = q.description,
                          mainAccountCode = mainAccont.maincode,
                          mainAccountDesc = mainAccont.description,

                      });

            var jk = new List<LedgersView2>();
            var gettotal = op.GroupBy(p=>p.mainAccountCode)
                .Select(
                  g => new LedgersView2
                  {
                      Amount =Math.Round((decimal)g.Sum(s => s.Amount),2),
                      description = g.First().description,
                      acctcode = g.First().acctcode,
                      mainAccountCode = g.First().mainAccountCode,
                      mainAccountDesc = g.First().mainAccountDesc,

                  }).OrderBy(g => g.acctcode);

            return gettotal;
        }

        public IEnumerable<LedgersView2> GetBalSheet_MainTrialBalance()
        {
            var op = (from p in context.npf_Ledgers
                      join q in context.npf_Charts on p.acctcode equals q.acctcode
                      join m in context.mainacts on q.mainAccountCode equals m.maincode

                      select new LedgersView2
                      {
                          acctcode = p.acctcode.Split('-')[0],
                          opbalance = p.opbalance,
                          adbbalance = p.adbbalance,
                          crbalance = p.crbalance,
                          Amount = p.opbalance + p.adbbalance - p.crbalance,
                          description = m.description
                          
                      });

            var jk = new List<LedgersView2>();
            var gettotal = op.GroupBy(p => p.acctcode)
                .Select(
                  g => new LedgersView2
                  {
                      Amount = g.Sum(s => s.Amount),
                      description = g.First().description,
                      acctcode = g.First().acctcode

                  }).OrderBy(g => g.acctcode);

            return gettotal;
        }


        public IEnumerable<LedgersView2> GetSurplus_Deficit()
        {
            var op = (from p in context.npf_Ledgers
                      join q in context.npf_Charts on p.acctcode equals q.acctcode
                      join m in context.mainacts on q.mainAccountCode equals m.maincode
                      where m.maincode.StartsWith("4") || m.maincode.StartsWith("5")
                      select new LedgersView2
                      {
                          acctcode = p.acctcode.Split('-')[0],
                          opbalance = p.opbalance,
                          adbbalance = p.adbbalance,
                          crbalance = p.crbalance,
                          Amount = p.opbalance + p.adbbalance - p.crbalance,
                          description = m.description

                      });

            //var jk = new List<LedgersView2>();
            var gettotal = op.GroupBy(p => p.acctcode)
                .Select(
                  g => new LedgersView2
                  {
                      Amount = Math.Round((decimal)g.Sum(s => s.Amount),2),
                      description = g.First().description,
                      acctcode = g.First().acctcode,
                      bl_desc=g.First().bl_desc,


                  }).OrderBy(g => g.acctcode);

            return gettotal;
        }

        public IEnumerable<LedgersView2> GetBalanceSheetSurplus_Deficit()
        {
            var op = (from p in context.npf_Ledgers
                      join q in context.npf_Charts on p.acctcode equals q.acctcode
                      join m in context.mainacts on q.mainAccountCode equals m.maincode
                      where m.maincode.StartsWith("1") || m.maincode.StartsWith("2") || m.maincode.StartsWith("3") || m.maincode.StartsWith("4") || m.maincode.StartsWith("5")
                      select new LedgersView2
                      {
                          acctcode = p.acctcode.Split('-')[0],
                          opbalance = p.opbalance,
                          adbbalance = p.adbbalance,
                          crbalance = p.crbalance,
                          Amount = p.opbalance + p.adbbalance - p.crbalance,
                          description = m.description

                      });

            var jk = new List<LedgersView2>();
            var gettotal = op.GroupBy(p => p.acctcode)
                .Select(
                  g => new LedgersView2
                  {
                      Amount = Math.Round((decimal)g.Sum(s => s.Amount), 2),
                      description = g.First().description,
                      acctcode = g.First().acctcode

                  }).OrderBy(g => g.acctcode);

            return gettotal;
        }


        public IEnumerable<LedgersView2> GetBalsheet2()
        {
            var op = (from p in context.npf_Histories
                      join q in context.npf_Charts on p.acctcode equals q.acctcode
                      join r in context.mainacts on q.mainAccountCode equals r.maincode
                      join s in context.npf_Balsheets on r.npf_balsheet_bl_code equals s.bl_code
                      select new LedgersView2
                      {
                          acctcode = p.acctcode,
                          docNo=p.docno,
                          adbbalance = p.dbamt,
                          crbalance = p.cramt,
                          Amount = p.dbamt - p.cramt,
                          description = q.description,
                          MDesc = r.description,
                          bl_desc = s.bl_desc,
                          balSheetCode = s.bl_code
                      }).ToList();

            var jk = new List<LedgersView2>();

            foreach (var j in op)
            {
                var s = j.acctcode.Substring(0, 1);
                if (int.Parse(s) <= 3)
                {
                    var kl = new LedgersView2
                    {
                        acctcode = j.acctcode,
                        adbbalance = j.adbbalance,
                        docNo=j.docNo,
                        crbalance = j.crbalance,
                        description = j.description,
                        MDesc = j.MDesc,
                        Amount = j.Amount,
                        bl_desc = j.bl_desc,
                        balSheetCode = j.balSheetCode
                    };

                    jk.Add(kl);
                }

            }
            var kj = jk.DistinctBy(x => x.acctcode);
            return kj.ToList();
        }

        public string getDescription(string code)
        {
            string result = string.Empty;
            var rf = context.mainacts.FirstOrDefault(x => x.maincode.Trim() == code.Trim());
            return rf.description;

        }



        public string RefractCode(string Code)
        {
            char[] op = Code.ToCharArray();
            var arr = new char[op.Length];
            int k = 1;
            string output = string.Empty;

            for (int j = 0; j < op.Length; j++)
            {
                if (k <= 4)
                {
                    output += op[j].ToString();
                }

                k++;
            }

            return output;
        }
    }
}

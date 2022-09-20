using NavyAccountCore.Core.Data;
using NavyAccountCore.Models;
using System.Linq;
using System.Collections.Generic;
using NavyAccountCore.Core.IRepositories;
using NavyAccountCore.Core.Entities;

namespace NavyAccountCore.Core.Repositories
{
    public class AuditRailRepository : IAuditTrail
    {
        private readonly INavyAccountDbContext context;

        public AuditRailRepository(INavyAccountDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<LedgersView> GetAllHistory(string speriod,string acctcode)
        {
            var result = context.npf_Histories.Where(x => x.acctcode == acctcode).ToList();
            int period2 = int.Parse(speriod);
            var resultj = from s in context.npf_Histories
                          where (s.acctcode == acctcode && int.Parse(s.period) > period2 && s.docno.Substring(0, 4) != "Open" && s.docno.Substring(0, 4) != "Mend")
                          select new LedgersView
                          {
                              doc = s.docno,
                              date = s.docdate,
                              Amount = (decimal)s.netamt,
                              remark=s.remarks
                          };
            //var result3 = new List<npf_history>();

            //foreach (var j in result)
            //{
            //    if (comparePeriod(j.period, speriod))
            //    {
            //        result3.Add(j);
            //    }
            //}

            //var result2 = FilterRecord(result3);


            return resultj.OrderBy(x=>x.date);
        }



        public IEnumerable<LedgersView> GetAllNpfChart()
        {
            return (from c in context.npf_Charts
                    select new LedgersView
                    {
                        code = c.acctcode,
                        desc = c.description
                    }).ToList();
        }

        public LedgersView GetOpenBal(string acctcode,string wdoc)
        {
            var op = new LedgersView();
            var result = context.npf_Histories.FirstOrDefault(x => x.acctcode == acctcode && x.docno==wdoc);
            if (result == null)
            {
                op.opBal = 0M;
                op.period = "0";

            }
            else
            {

                op.opBal = (decimal)result.netamt;
                op.period = result.period;
            }
           

            return op;
        }


        public bool comparePeriod(string perioddb,string speriod)
        {
            int p1 = int.Parse(perioddb);
            int p2 = int.Parse(speriod);
          

            bool check = false;

            if(p1>p2)
            {
                check = true;
            }

            return check;
        }

        public IEnumerable<LedgersView> FilterRecord(List<npf_history> solly)
        {
            var listApp = new List<LedgersView>();

            foreach(var j in solly)
            {
                string code = RefractCode(j.docno);
                if (code!="Mend" && code!="Open")
                {
                    var jk = new LedgersView
                    {
                        period=j.period,
                        doc = j.docno,
                        date = j.docdate,
                        remark = j.remarks,
                        dbbal = (j.dbamt == null) ? 0M : (decimal)j.dbamt,
                        crbal = (j.cramt == null) ? 0M : (decimal)j.cramt
                    };

                    listApp.Add(jk);
                }
               
            }

            return listApp;
        }

        public IEnumerable<string> GetAllyear()
        {
            try
            {

            var jk = context.npf_Histories.ToList();
            var opo = new List<string>();
            
            
            foreach(var j in jk)
            {
                opo.Add(j.period.Substring(0, 4));
            }
            var opo2 = opo.ToList().Distinct();
            return opo2;

            }
            catch (System.Exception ex)
            {

                throw;
            }
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

        public string GetAccountDesc(string code)
        {
            var mess = context.npf_Charts.FirstOrDefault(x => x.acctcode == code);
            return mess.description;
        }
    }
}

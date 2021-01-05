using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Entities;
using NavyAccountCore.Core.IRepositories;
using NavyAccountCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NavyAccountCore.Core.Repositories
{
    public class TrialBalanceReportRepository : Repository<npf_ledger>, ITrialBalanceReport
    {
        private readonly INavyAccountDbContext context;
        public TrialBalanceReportRepository(INavyAccountDbContext context):base(context)
        {
            this.context = context;
        }
        public IEnumerable<LedgersView> GetAllLedegers(string indicator,string fundcode)
        {
            
            var result2 = new List<LedgersView>();

            var TrialBalanceList = (from c in context.npf_Ledgers
                                    join chart in context.npf_Charts on c.acctcode equals chart.acctcode
                                    where c.fundcode == fundcode
                                    select new LedgersView
                                    {
                                        code = c.acctcode,
                                        opBal = c.opbalance,
                                        dbbal = c.adbbalance,
                                        crbal = c.crbalance,
                                        desc = chart.description
                                    }).OrderBy(c => c.code);



            if (indicator == "Subsidiary Ledger")
            {
                
                foreach (var j in TrialBalanceList)
                {

                    var p = new LedgersView
                    {
                        code = j.code,
                        opBal = j.opBal,
                        dbbal = j.dbbal,
                        crbal = j.crbal,
                        desc = j.desc
                    };
                    result2.Add(p);
                }

            }
            else if(indicator== "Main Ledger")
            {
               
                foreach (var j in TrialBalanceList)
                {
                    var p = new LedgersView
                    {
                        code = RefractCode(j.code),

                        opBal = j.opBal,
                        dbbal = j.dbbal,
                        crbal = j.crbal,
                        desc = GetDescription2(j.code)
                    };
                    result2.Add(p);
                }
            }
            else
            {
               
                foreach (var j in TrialBalanceList)
                {
                    var p = new LedgersView
                    {
                        code = j.code,
                        opBal = j.opBal,
                        dbbal = j.dbbal,
                        crbal = j.crbal,
                        desc = j.desc
                    };
                    result2.Add(p);
                }
            }

            return result2;
        }

        public IEnumerable<LedgersView> GetAllLoanTypes()
        {
            var result = context.Pf_loanType.ToList();
            var listApp = new List<LedgersView>();

            foreach (var j in result)
            {
                var t = new LedgersView
                {
                    code = RefractCode(j.loanacct),
                    desc = j.Description
                };

                listApp.Add(t);
            }

            return listApp;

        }

        public IEnumerable<LedgersView> GetAllMainAct()
        {
            var result = context.mainacts.ToList();
            var listApp = new List<LedgersView>();

            foreach(var j in result)
            {
                var t = new LedgersView
                {
                    code=j.maincode,
                    desc=j.description
                };

                listApp.Add(t);
            }

            return listApp;

        }

        public IEnumerable<LedgersView> GetAllNPFHistory(string codedate)
        {
            List<npf_history> result = new List<npf_history>();

            result = context.npf_Histories.Where(x => x.docno.Substring(0,8) == codedate).ToList();

           

            var result2 = new List<LedgersView>();
            foreach(var j in result)
            {
                    var p = new LedgersView
                    {
                        code = j.acctcode,
                        opBal = 0M,
                        dbbal = (decimal)j.dbamt,
                        crbal = (decimal)j.cramt,
                        desc = GetDescription(j.acctcode)
                    };

                    result2.Add(p);
               
            }

            return result2;

        }

        public string GetDescription(string code)
        {
            var rf = context.npf_Charts.FirstOrDefault(x => x.acctcode == code);
            return rf.description;
        }

        public string GetDescription2(string code)
        {
            string code2 = RefractCode(code);
            var rf = context.mainacts.FirstOrDefault(x => x.maincode.Trim() == code2.Trim());
            return (rf==null)?"":rf.description;
        }

        public string GetMainActDescription(string code)
        {
            return context.mainacts.FirstOrDefault(x => x.maincode == code).description;
        }

        public IEnumerable<LedgersView> GetMainLedgersByDate(List<LedgersView> sol)
        {
            var result = new List<LedgersView>();
            foreach (var j in sol)
            {
                var p = new LedgersView
                {
                    code = RefractCode(j.code),
                    opBal = 0M,
                    dbbal = j.dbbal,
                    crbal = j.crbal,
                    desc = GetDescription2(j.code)
                };
                result.Add(p);
            }

            return result;
        }

        public string RefractCode(string Code)
        {
            char[] op = Code.ToCharArray();
            var arr = new char[op.Length];
            int k = 1;
            string output = string.Empty;

            for(int j = 0; j < op.Length; j++)
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

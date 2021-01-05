using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Entities;
using NavyAccountCore.Core.Repositories;
using NavyAccountCore.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using NavyAccountCore.Models;

namespace NavyAccountCore.Core.Repositories
{
    public class LedgerRepositoy : Repository<npf_ledger>, ILedgerRepository
    {

        private readonly INavyAccountDbContext context;
        public LedgerRepositoy(INavyAccountDbContext context) : base(context)
        {
            this.context = context;
        }
        public npf_ledger Getledger(Expression<Func<npf_ledger, bool>> predicate)
        {
  
            return context.npf_Ledgers.FirstOrDefault(predicate);
        }

        public npf_ledger getSurplusValue(Expression<Func<npf_ledger, bool>> predicate)
        {
            return context.npf_Ledgers.FirstOrDefault(predicate);
        }
        public List<AcctLedgerViewModel> getLedgerInfoCSD(string fundcode)
        {
         
            return (from bene in context.npf_Ledgers
              where ((bene.acctcode.Substring(0, 1) == "4" || bene.acctcode.Substring(0, 1) == "5") && bene.fundcode == fundcode)
                  select new AcctLedgerViewModel
                    {
                        acctcode = bene.acctcode,
                        adbbalance = bene.adbbalance,
                        balpl = bene.balpl,
                        crbalance = bene.crbalance,
                        fundcode = bene.fundcode,
                        opbalance = bene.opbalance
                    }).ToList();
        }
        public List<AcctLedgerViewModel> getLedgerInfo(string serviceno, string fundcode)
        {
            return  (from bene in context.npf_Ledgers
                          where (bene.acctcode.EndsWith(serviceno) && bene.fundcode == fundcode)
                          select new AcctLedgerViewModel
                          {
                             acctcode = bene.acctcode, 
                             adbbalance = bene.adbbalance,
                             balpl = bene.balpl,
                             crbalance = bene.crbalance,
                             fundcode = bene.fundcode,
                             opbalance=bene.opbalance
                          }).ToList();
        }

    }
}

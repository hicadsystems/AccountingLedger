using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.IRepositories;
using NavyAccountCore.Entities;
using NavyAccountCore.Models;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System;

namespace NavyAccountCore.Core.Repositories
{
    public class ContrRepo: Repository<npf_contrdisc>, IContrRepo
    {

        private readonly INavyAccountDbContext context;
        public ContrRepo(INavyAccountDbContext context) : base(context)
        {
            this.context = context;
        }
        public npf_contrdisc GetContrDiscByCode(Expression<Func<npf_contrdisc, bool>> predicate)
        {

            return context.npf_contrdisc.FirstOrDefault(predicate);
        }
        public IEnumerable<ContrDiscVM> Getbyfundcode(string fundcode)
        {
            return (from loandisc in context.npf_contrdisc
                    join per in context.npf_Charts on loandisc.contract equals per.acctcode
                    where (loandisc.fundcode == fundcode)
                    select new ContrDiscVM
                    {
                        Id = loandisc.Id,
                        personname = per.description,
                        period = loandisc.period,
                        intract = loandisc.intract,
                        amountpay = loandisc.amountpay,
                        prvamt = loandisc.prvamt,
                        amtvar = loandisc.amtvar,
                        extamt = loandisc.extamt,
                        contract = loandisc.contract,
                        amtdiscr = loandisc.amtdiscr,
                        groupcode=loandisc.groupcode,
                        trusteeact=loandisc.trusteeact
                    }).ToList();
        }

    }
}

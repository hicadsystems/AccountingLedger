using Microsoft.EntityFrameworkCore;
using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Entities;
using NavyAccountCore.Core.IRepositories;
using NavyAccountCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NavyAccountCore.Core.Repositories
{
    public class NPFContributionRepository : Repository<npf_contributions>,INPFContribution
    {
        private readonly INavyAccountDbContext context;

        public NPFContributionRepository(INavyAccountDbContext context):base(context)
        {
            this.context = context;
        }

        public IEnumerable<Pf_fund> getAllFund()
        {
            return context.pf_Funds.ToList();
        }

        public IEnumerable<Pf_loanType> getAllLoan()
        {
            return context.Pf_loanType.ToList();
        }

        public IEnumerable<contributionView> getAllNpf( string fund)
        {
            if (fund== "10") { 
            
            return (from p in context.npf_Contributions join
                    q in context.pf_Funds on p.fundtype equals q.Code
                    where p.fundtype.Equals(fund)
                    select new contributionView
                    {
                       fundcode=q.Description,
                       amount01=p.amount01,
                       amount02=p.amount02,
                       amount03=p.amount03,
                       amount04=p.amount04,
                       amount05=p.amount05,
                       amount06=p.amount06,
                       amount07=p.amount07,
                       amount08=p.amount08,
                       amount09=p.amount09,
                       amount10=p.amount10,
                       amount11=p.amount11,
                       amount12=p.amount12,
                       amount13=p.amount13,
                       amount14=p.amount14,
                       amount15=p.amount15,
                       amount16=p.amount16,
                       amount17=p.amount17,
                       amount18=p.amount18,
                       amount19=p.amount19,
                       reviewDate=Convert.ToInt32(p.reviewDate.Value.Year),
                       reviewDate2=Convert.ToInt32(p.reviewDate.Value.Year)-1
                    }).ToList();
            }
            else
            {
                return (from p in context.npf_Contributions
                        join q in context.pf_Funds on p.fundtype equals q.Code
                        where p.fundtype.Equals(fund)
                        select new contributionView
                        {
                            fundcode = q.Description,
                            amount01 = p.amount01,
                            amount02 = p.amount02,
                            amount03 = p.amount03,
                            amount04 = p.amount04,
                            amount05 = p.amount05,
                            amount06 = p.amount06,
                            amount07 = p.amount07,
                            amount08 = p.amount08,
                            amount09 = p.amount09,
                            amount10 = p.amount10,
                            amount11 = p.amount11,
                            amount12 = p.amount12,
                            amount13 = p.amount13,
                            amount14 = p.amount14,
                            amount15 = p.amount15,
                            amount16 = p.amount16,
                            amount17 = p.amount17,
                            amount18 = p.amount18,
                            amount19 = p.amount19,
                            
                        }).ToList();
            }
        }

        public async Task<npf_contributions> GetContributionByCode(Expression<Func<npf_contributions, bool>> predicate)
        {
            return await context.npf_Contributions.FirstOrDefaultAsync(predicate);
        }
    }
}
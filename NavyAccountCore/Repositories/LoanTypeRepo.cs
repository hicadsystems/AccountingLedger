using Microsoft.EntityFrameworkCore;
using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Entities;
using NavyAccountCore.Core.IRepositories;
using NavyAccountCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NavyAccountCore.Core.Repositories
{
    public class LoanTypeRepo : Repository<Pf_loanType>, ILoanType
    {
        private readonly INavyAccountDbContext context;
        public LoanTypeRepo(INavyAccountDbContext context) : base(context)
        { 
            this.context = context;
        }

       
        public  Pf_loanType GetLoanTypeByCode(Expression<Func<Pf_loanType, bool>> predicate)
        {
            return  context.Pf_loanType.FirstOrDefault(predicate);
        }
       

        public async Task<List<LoanTypeView>> GetLoanTypeDesc()
        {
            return await (from npfloantype in context.Pf_loanType
                          join npffundtype in context.npffundType on npfloantype.FundTypeID equals npffundtype.Id
                          select new LoanTypeView
                          {
                              Id = npfloantype.Id,
                              Code = npfloantype.Code,
                              Description = npfloantype.Description,
                              FundTypeID = npfloantype.FundTypeID,
                              Tenure=npfloantype.Tenure,
                              Interest = npfloantype.Interest,
                              incomeacct = npfloantype.incomeacct,
                              interestacct= npfloantype.interestacct,
                              liabilityacct = npfloantype.liabilityacct,
                              loanacct=npfloantype.loanacct,
                              trusteeacct=npfloantype.trusteeacct,
                              datecreated = npfloantype.datecreated,
                              createdby = npfloantype.createdby,
                              fundtypedesc = npffundtype.Description
                          }).ToListAsync();
        }

    }
}
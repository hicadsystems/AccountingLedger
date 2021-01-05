using Microsoft.EntityFrameworkCore;
using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.IRepositories;
using NavyAccountCore.Entities;
using NavyAccountCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NavyAccountCore.Core.Repositories
{
    public class LoanTypeReviewRepo: Repository<npf_LoanTypeReview>, ILoanTypeReviewRepo
    {
        private readonly INavyAccountDbContext context;
        public LoanTypeReviewRepo(INavyAccountDbContext context) : base(context)
        {
            this.context = context;
        }
        public npf_LoanTypeReview GetLoanTypeByCode(Expression<Func<npf_LoanTypeReview, bool>> predicate)
        {
            return context.npf_loantypereview.FirstOrDefault(predicate);
        }
        public async Task<List<loanreview>> GetLoanTypeDesc()
        {
            return await (from npfloantypeR in context.npf_loantypereview
                          join npffundtype in context.Pf_loanType on npfloantypeR.LoanType equals npffundtype.Code
                          select new loanreview
                          {
                              id=npfloantypeR.Id,
                              ReviewDate = npfloantypeR.ReviewDate,
                              Interestrate = npfloantypeR.Interestrate,
                              Loantypedesc = npffundtype.Description,
                              LoanType=npffundtype.Code
                          }).ToListAsync();
        }

    }
}

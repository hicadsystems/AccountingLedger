using NavyAccountCore.Core.Entities;
using NavyAccountCore.Core.IRepositories;
using NavyAccountCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;

namespace NavyAccountCore.Core.IRepositories
{
    public interface ILoanStatus:IRepository<LoanStatus>
    {
        Task<List<LoanStatusVM>> GetloanStatus2(int id);
        Task<LoanStatus> GetLoanStatusByCode(Expression<Func<LoanStatus, bool>> predicate);
    }
}
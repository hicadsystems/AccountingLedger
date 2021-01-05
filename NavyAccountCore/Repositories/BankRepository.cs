using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Entities;
using NavyAccountCore.Core.IRepositories;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace NavyAccountCore.Core.Repositories
{
    public class BankRepository : Repository<py_bank>, IBank
    {
        private readonly INavyAccountDbContext context;
        public BankRepository(INavyAccountDbContext context) : base(context)
        {
            this.context = context;
        }
        public py_bank GetBankbyName(Expression<Func<py_bank, bool>> predicate)
        {
            return context.py_Banks.FirstOrDefault(predicate);
        }
    }
}
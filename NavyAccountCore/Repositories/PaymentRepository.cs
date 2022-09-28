using Microsoft.EntityFrameworkCore;
using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Repositories;
using NavyAccountCore.Entities;
using NavyAccountCore.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NavyAccountCore.Repositories
{
    public class PaymentRepository : Repository<sr_PaymentRecord>, IPaymentRepository
    {
        private readonly INavyAccountDbContext context;
        public PaymentRepository(INavyAccountDbContext context):base(context)
        {
            this.context = context;

        }

        public async Task<sr_PaymentRecord> GetPaymentByCode(Expression<Func<sr_PaymentRecord,bool>> predicate)
        {
            return await context.sr_PaymentRecord.FirstOrDefaultAsync(predicate);
        }

    }
}

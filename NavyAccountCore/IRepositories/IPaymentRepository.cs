using NavyAccountCore.Core.IRepositories;
using NavyAccountCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NavyAccountCore.IRepositories
{
    public interface IPaymentRepository:IRepository<sr_PaymentRecord>
    {
        Task<sr_PaymentRecord> GetPaymentByCode(Expression<Func<sr_PaymentRecord, bool>> predicate);

    }
}

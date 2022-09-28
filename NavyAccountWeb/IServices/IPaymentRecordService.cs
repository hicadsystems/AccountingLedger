using NavyAccountCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.IServices
{
    public interface IPaymentRecordService
    {
        Task<bool> AddPayment(sr_PaymentRecord value);
        Task<bool> UpdatePayment(sr_PaymentRecord value);
        void DeletePayment(sr_PaymentRecord value);
        Task<IEnumerable<sr_PaymentRecord>> GetAllPayment();
        Task<sr_PaymentRecord> GetPaymentByCode(string code);
        Task<sr_PaymentRecord> GetPaymentByid(int id);
        
    }
}

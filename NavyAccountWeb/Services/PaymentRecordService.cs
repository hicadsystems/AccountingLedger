using NavyAccountCore.Core.Data;
using NavyAccountCore.Entities;
using NavyAccountWeb.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.Services
{
    public class PaymentRecordService:IPaymentRecordService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PaymentRecordService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddPayment(sr_PaymentRecord value)
        {
            _unitOfWork.payment.Create(value);
            return await _unitOfWork.Done();
        }

        public void DeletePayment(sr_PaymentRecord value)
        {
            _unitOfWork.payment.Remove(value);
            _unitOfWork.Done();
        }

        public Task<IEnumerable<sr_PaymentRecord>> GetAllPayment()
        {
            throw new NotImplementedException();
        }

        public async Task<sr_PaymentRecord> GetPaymentByCode(string code)
        {
            return await _unitOfWork.payment.GetPaymentByCode(x => x.Reg_Number == code);
        }

        public async Task<sr_PaymentRecord> GetPaymentByid(int id)
        {
            return await _unitOfWork.payment.GetPaymentByCode(x => x.id == id);
        }

        public async Task<bool> UpdatePayment(sr_PaymentRecord value)
        {
            _unitOfWork.payment.Update(value);
            return await _unitOfWork.Done();
        }
    }
}

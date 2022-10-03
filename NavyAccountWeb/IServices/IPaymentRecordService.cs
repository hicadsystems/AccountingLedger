using NavyAccountCore.Entities;
using NavyAccountWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.IServices
{
    public interface IPaymentRecordService
    {
        Task<List<PaymentProposalRecord>> GetStudentpaymentProposal();
        Task<List<PaymentProposalRecord>> GetStudentpaymentProposalbySchool(string schoolname);
        Task<List<PaymentProposalRecord>> GetDiscrepancyRecord();
        Task<int> UpdatePaymentProposal(PaymentPoposalExcelRecord req);
        Task<List<PaymentProposalRecord>> moveRecord(List<PaymentProposalRecord> record);

        Task<bool> AddPayment(sr_PaymentRecord value);
        Task<bool> UpdatePayment(sr_PaymentRecord value);
        void DeletePayment(sr_PaymentRecord value);
        Task<IEnumerable<sr_PaymentRecord>> GetAllPayment();
        Task<sr_PaymentRecord> GetPaymentByCode(string code);
        Task<sr_PaymentRecord> GetPaymentByid(int id);
        
    }
}

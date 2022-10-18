using NavyAccountCore.Entities;
using NavyAccountWeb.Models;
using NavyAccountWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.IServices
{
    public interface IPaymentRecordService
    {
        Task AddRecordToDefaulter(string regNumer, string createdBy);
        Task<List<StudentPayViewModel>> GetStudentPaySummary(int id);
        Task<List<SchoolStudentRecord>> filterSchoolWithStudent();
        Task<List<PaymentProposalRecord>> GetStudentpaymentProposal();
        Task<List<PaymentProposalRecord>> GetStudentpaymentProposalbySchool(string schoolname);
        Task<Tuple<List<PaymentProposalRecord>,int>> GetDiscrepancyRecord(int iDisplayStart, int iDisplayLength);
        Task<List<PaymentProposalRecord>> GetDiscrepancyRecordAsExcel();
        Task<List<PaymentProposalRecord>> GetpaymentProposalByValue(string paymentProposalValue);
        Task UpdatePaymentProposal(PaymentPoposalExcelRecord req);
        Task<List<PaymentProposalRecord>> moveRecord(List<PaymentProposalRecord> record);
        Task<List<PaymentProposalRecord2>> filteredPaymentProposal(string proposalValue);
        Task<List<PaymentProposalRecord>> GetPaymentProposalByReq(string reqNum);
        Task<int> GetStudentCountUnderDescrepancy();

        Task UpdatePaymentProposal();

        Task<bool> AddPayment(sr_PaymentRecord value);
        Task<bool> UpdatePayment(sr_PaymentRecord value);
        void DeletePayment(sr_PaymentRecord value);
        Task<IEnumerable<sr_PaymentRecord>> GetAllPayment();
        Task<sr_PaymentRecord> GetPaymentByCode(string code);
        Task<sr_PaymentRecord> GetPaymentByid(int id);
        Task DeletePaymentProposal(DeleteStudentPaymentproposal model);


    }
}

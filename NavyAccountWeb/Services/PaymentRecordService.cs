using Dapper;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors.Internal;
using MoreLinq;
using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Entities;
using NavyAccountCore.Entities;
using NavyAccountWeb.IServices;
using NavyAccountWeb.Models;
using NavyAccountWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.Services
{
    public class PaymentRecordService:IPaymentRecordService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDapper dapper;
        public PaymentRecordService(IUnitOfWork unitOfWork, IDapper dapper)
        {
            _unitOfWork = unitOfWork;
            this.dapper = dapper;
        }

        public async Task<List<SchoolStudentRecordModel>> filterSchoolWithStudent()
        {
            var result = new List<SchoolStudentRecordModel>();
            var param = new DynamicParameters();
            result = dapper.GetAll<SchoolStudentRecordModel>("sr_FilterSchoolByStudent", param, commandType: System.Data.CommandType.StoredProcedure);
            return result;
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

        public async Task<Tuple<List<PaymentProposalRecord>,int>> GetDiscrepancyRecord(int iDisplayStart, int iDisplayLength)
        {
            
            var param = new DynamicParameters();
            var result = dapper.GetAll<PaymentProposalRecord>("sr_GetStudentDescrePancyReport", param, commandType:System.Data.CommandType.StoredProcedure);
            int totalCount=result.Count();
            var op=result.Skip(iDisplayStart).Take(iDisplayLength);

            return new Tuple<List<PaymentProposalRecord>, int>(result, totalCount);
        }

        public async Task<List<PaymentProposalRecord>> GetDiscrepancyRecordAsExcel()
        {
            var param = new DynamicParameters();
            var result = dapper.GetAll<PaymentProposalRecord>("sr_GetStudentDescrePancyReport", param, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public async Task<sr_PaymentRecord> GetPaymentByCode(string code)
        {
            return await _unitOfWork.payment.GetPaymentByCode(x => x.Reg_Number == code);
        }

        public async Task<sr_PaymentRecord> GetPaymentByid(int id)
        {
            return await _unitOfWork.payment.GetPaymentByCode(x => x.id == id);
        }

        public async Task<List<PaymentProposalRecord>> GetStudentpaymentProposal()
        {
            var result = new List<PaymentProposalRecord>();
            var param = new DynamicParameters();
            result = dapper.GetAll<PaymentProposalRecord>("sr_GetActiveStudentPaymentProposalRecord", param, commandType:System.Data.CommandType.StoredProcedure);

            return result;
        }

        public async Task<List<PaymentProposalRecord>> GetPaymentProposalByReq(string reqNum)
        {
            var polly = new List<PaymentProposalRecord>();
            var op = reqNum.Split('_');
            var result = new PaymentProposalRecord();
            var param = new DynamicParameters();
            param.Add("@RegNum", op[1]);
            result = dapper.Get<PaymentProposalRecord>("sp_getPaymentproposalByregNum", param, commandType: System.Data.CommandType.StoredProcedure);
            
            polly.Add(result);
            return polly;
        }

        public async Task<List<PaymentProposalRecord2>> filteredPaymentProposal(string proposalValue)
        {
            var op = new List<PaymentProposalRecord2>();
            var result = await GetpaymentProposalByValue(proposalValue);
            int jp = 1;
            foreach(var j in result)
            {
                op.Add(new PaymentProposalRecord2() { 
                    id=jp,
                    name=j.Surname+"_"+j.Req_Number
                });
                jp++;

            }
            return op;
        }

        public async Task<List<PaymentProposalRecord>> GetStudentpaymentProposalbySchool(string schoolname)
        {
            var result = new List<PaymentProposalRecord>();
            var param = new DynamicParameters();
            param.Add("@school", schoolname.ToUpper());
            result = dapper.GetAll<PaymentProposalRecord>("sr_GetActiveStudentPaymentProposalRecordBySchool", param, commandType: System.Data.CommandType.StoredProcedure);
           
            return result;
        }

        public async Task<List<PaymentProposalRecord>> moveRecord(List<PaymentProposalRecord> record)
        {
            var result=new List<PaymentProposalRecord>();
            foreach(var j in record)
            {
                if (j.SchoolType.ToUpper() == "PRIMARY")
                {
                    j.Amount = 500;
                }

                if (j.SchoolType.ToUpper() == "SECONDARY")
                {
                    j.Amount = 1000;
                }

                result.Add(j);

            }

            return result;
        }

        public async Task<bool> UpdatePayment(sr_PaymentRecord value)
        {
            _unitOfWork.payment.Update(value);
            return await _unitOfWork.Done();
        }

        public async Task UpdatePaymentProposal(PaymentPoposalExcelRecord req)
        {
            var param = new DynamicParameters();
            param.Add("@Req_Number", req.Req_Number);
            param.Add("@amount", req.Amount);
            param.Add("@schoolname", req.Schoolname);

            dapper.Execute("sr_UpdatePaymentProposal", param, commandType:System.Data.CommandType.StoredProcedure);

        }
        public async Task<List<StudentPayViewModel>> GetStudentPaySummary(int id)
        {
            var result = new List<StudentPayViewModel>();
            var param = new DynamicParameters();
            param.Add("@id", id);
            result = dapper.GetAll<StudentPayViewModel>("sr_GetPaymentSummary", param, commandType: System.Data.CommandType.StoredProcedure);

            return result;
        }

        public async Task<List<PaymentProposalRecord>> GetpaymentProposalByValue(string paymentProposalValue)
        {
            var param = new DynamicParameters();
            param.Add("@proposalValue", paymentProposalValue);
            var result = dapper.GetAll<PaymentProposalRecord>("sp_GetPaymentProposalByValue", param,commandType:System.Data.CommandType.StoredProcedure);

            return result;
        }

        public async Task DeletePaymentProposal(DeleteStudentPaymentproposal model)
        {
            var param = new DynamicParameters();
            param.Add("@Req_Number", model.Req_Number);

            dapper.Execute("sp_RemoveFromDiscrepancy", param, commandType:System.Data.CommandType.StoredProcedure);
        }

        public async Task UpdatePaymentProposal()
        {
            
        }

        public async Task AddRecordToDefaulter(string regNumer,string createdBy)
        {
            //get payment proposal record
            var result = new PaymentProposalRecord22();
            var param = new DynamicParameters();
            param.Add("@RegNum",regNumer);
            result = dapper.Get<PaymentProposalRecord22>("sp_getPaymentproposalByregNum", param, commandType: System.Data.CommandType.StoredProcedure);

            if (result.SchoolType.ToUpper() == "PRIMARY")
            {
                result.Amount = 500;
            }

            if (result.SchoolType.ToUpper() == "SECONDARY")
            {
                result.Amount = 1000;
            }

            //add record to defaulter table

            var param2 = new DynamicParameters();
            param2.Add("@Reg_Number", result.Req_Number);
            param2.Add("@Surname", result.Surname);
            param2.Add("@FirstName", result.FirstName);
            param2.Add("@MiddleName", result.MiddleName);
            param2.Add("@Schoolname", result.Schoolname);
            param2.Add("@period", result.Period);
            param2.Add("@term", result.Term);
            param2.Add("@Amount", result.Amount);
            param2.Add("@createdby", createdBy);

            dapper.Execute("sr_AddToDefaulter", param2, commandType: System.Data.CommandType.StoredProcedure);

        }

        public async Task<int> GetStudentCountUnderDescrepancy()
        {
            var param = new DynamicParameters();
            int result = dapper.Get<int>("sr_GetStudentCountUnderDescrepancy", param, commandType: System.Data.CommandType.StoredProcedure);

            return result;
        }

        public async Task<List<DefaulterModel>> GetdefaulterRecord()
        {
            var result = new List<DefaulterModel>();
            var param = new DynamicParameters();
            result = dapper.GetAll<DefaulterModel>("sp_GetDefaulter", param, commandType: System.Data.CommandType.StoredProcedure);

            return result;
        }
    }
}

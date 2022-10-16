using Dapper;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors.Internal;
using MoreLinq;
using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Entities;
using NavyAccountCore.Entities;
using NavyAccountWeb.IServices;
using NavyAccountWeb.Models;
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

        public async Task<List<SchoolStudentRecord>> filterSchoolWithStudent()
        {
            var op = new List<SchoolStudentRecord>();
            var param = new DynamicParameters();
            var result = dapper.GetAll<SchoolStudentRecordModel>("sr_FilterSchoolByStudent", param, commandType: System.Data.CommandType.StoredProcedure);
            var distinctSchoolRecord = result.DistinctBy(x => x.school);
            int count = 0;
            foreach(var j in distinctSchoolRecord)
            {
                foreach(var k in result)
                {
                    if (j.school.ToUpper() == k.school.ToUpper())
                    {
                        count++;
                    }
                }
                op.Add(new SchoolStudentRecord {
                    school=j.school,
                    strength=j.strength,
                    studentCount=count
                });

                count = 0;

            }

            return op;
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
                    name=j.Surname+" "+j.FirstName+" "+j.MiddleName+"_"+j.Req_Number
                });
                jp++;

            }
            return op;
        }

        public async Task<List<PaymentProposalRecord>> GetStudentpaymentProposalbySchool(string schoolname)
        {
            var result = new List<PaymentProposalRecord>();
            var param = new DynamicParameters();
            result = dapper.GetAll<PaymentProposalRecord>("sr_GetActiveStudentPaymentProposalRecord", param, commandType: System.Data.CommandType.StoredProcedure);
            result = result.Where(x => x.Schoolname.ToUpper() == schoolname.ToUpper()).ToList();

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
    }
}

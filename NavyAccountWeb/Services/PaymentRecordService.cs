﻿using Dapper;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors.Internal;
using NavyAccountCore.Core.Data;
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

        public async Task<List<PaymentProposalRecord>> GetDiscrepancyRecord()
        {
            var result = new List<PaymentProposalRecord>();
            var param = new DynamicParameters();
            result = dapper.GetAll<PaymentProposalRecord>("sr_GetStudentDescrePancyReport", param, commandType:System.Data.CommandType.StoredProcedure);
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

        public async Task<int> UpdatePaymentProposal(PaymentPoposalExcelRecord req)
        {
            var param = new DynamicParameters();
            param.Add("@Req_Number", req.Req_Number);
            param.Add("@amount", req.Amount);

            var result = dapper.Get<int>("sr_UpdatePaymentProposal", param, commandType:System.Data.CommandType.StoredProcedure);

            return result;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using NavyAccountCore.Core.Entities;
using System.Threading.Tasks;
using NavyAccountCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace NavyAccountWeb.IServices
{ public interface ILoanRegisterService
    {
        Task<decimal> loanAmountPerRankBy(int rankid, string loanType);
        Task<loanPerRank> GetLoanAmountByLoanType(string loanType);
        Task<List<LoanRegisterViewModel>> getListofLoanRegister(int fundtypeid);
        IEnumerable<LoanRegisterViewModel> getListofLoanRegister();
        Task<List<LoanRegisterViewModel>> getListofLoanRegisterByStatus(int fundtypeid, string loanStatus);
        Task<List<Pf_LoanRegister>> getPendingLoanCount(int fundtypeid);
        Task<List<LoanRegisterViewModel>> getApprovedLoancount(int fundtypeid);

        Task<bool> AddMultipleLoanRegister(List<Pf_LoanRegister> loanRegister);
        Task<Pf_LoanRegister> GetLoanRegisterById(int id);
        Task<List<LoanRegisterViewModel>> getListofLoanRegisterBTNByStatus(int fundtypeid, string loanStatus);
        Task<List<LoanRegisterViewModel>> getLoanListPerPerson(int personID, int fundTypeID);
        Task<List<LoanRegisterViewModel>> getListofLoanRegisterBTNByStatusP(int fundtypeid, string loanstatus);
        Task<List<LoanRegisterViewModel>> getListofLoanReport(int fundtypeid, string loanstatus,DateTime startdate, DateTime enddate);
        Task<List<LoanRegisterViewModel>> getListofLoanReportSummary(string batchno);

        Task<decimal> loanAmountPerRankBy2(int rankid, string loanType, int fundtypecode);
        Task<bool> AddLoanRegister(Pf_LoanRegister pf_LoanRegister);
        Task<bool> UpdateLoanRegister(Pf_LoanRegister pf_LoanRegister);
        Task<List<LoanRegisterViewModel222>> getListofLoanRegisterByBatch(string batchs);
        Task<List<Pf_LoanRegister>> getListofLoanRegisterByBatchDrp();
        Task<List<Pf_LoanRegister>> getListofLoanRegisterBatchDrp();
        Task<List<LoanRegisterViewModel22>> getListofLoanRegisterByBatch2(string batchs);
        Task<List<LoanRegisterViewModelRecieveable>> getListofLoanRegisterRecieveable(int loantypeid, string batch,bool ckforoutst);
        Task<List<Pf_LoanRegister>> GetLoanRegisterByApplication();
        Task<List<npf_loanstatus>> getListofLoanStatus();
        Task<List<npf_history>> getListofHistoryPeriod(string loanacct,string batchNo);
        Task<List<npf_history>> getListofHistoryPeriod2(string loanacct, string period);
    }
}
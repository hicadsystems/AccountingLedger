using NavyAccountCore.Core.Entities;
using NavyAccountCore.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NavyAccountCore.Core.IRepositories
{
    public interface ILoanRegisterRepository : IRepository<Pf_LoanRegister>
    {
        Pf_LoanRegister GetloanregisterByCode(Expression<Func<Pf_LoanRegister, bool>> predicate);
       // Task<bool> checkIfPersonExistInLoanRegister(int personid);
        Task<decimal> loanAmountPerRankBy(int rankid, string loanType);
        Task<loanPerRank> GetLoanAmountByLoanType(string loanType);
        Task<decimal> loanAmountPerRankBy2(int rankid, string loanType);
        Task<List<LoanRegisterViewModel>> getListofLoanRegister(int fundtypeid);
        IEnumerable<LoanRegisterViewModel> getListofLoanRegister();
        Task<List<LoanRegisterViewModel>> getListofLoanRegisterByStatus(int fundtypeid, string loanstatus);
        Task<List<Pf_LoanRegister>> getLoanRegisterPending(int fundtypeid);
        Task<List<Pf_LoanRegister>> GetLoanRegisterByApplication();
        Task<List<LoanRegisterViewModel>> getLoanRegisterApproved(int fundtypeid);
        Task<List<LoanRegisterViewModel>> getListofLoanRegisterPerPerson(int PersonID, int fundtypeid);
        Task<List<LoanRegisterViewModel>> getListofLoanRegisterBTNByStatus(int fundtypeid, string loanstatus);
        Task<List<LoanRegisterViewModel>> getListofLoanRegisterBTNByStatusP(int fundtypeid, string loanstatus);
        Task<List<LoanRegisterViewModel>> getListofLoanReport(int fundtypeid, string loanstatus, DateTime startdate, DateTime enddate);
        Task<List<LoanRegisterViewModel222>> getListofLoanRegisterByBatch(string batchs);
        Task<List<LoanRegisterViewModel22>> getListofLoanRegisterByBatch2(string batchs);
        Task<List<LoanRegisterViewModelRecieveable>> getListofLoanRegisterRecieveable(int loantypeid, string batch,bool ckforoutst);
        Task<List<Pf_LoanRegister>> getListofLoanRegisterByBatchDrp();
        Task<List<Pf_LoanRegister>> getListofLoanRegisterBatchDrp();
        Task<List<npf_loanstatus>> getListofLoanStatus();
        Task<List<npf_history>> getListofHistoryPeriod(string loanacct,string batchNo);
        Task<List<npf_history>> getListofHistoryPeriod2(string loanacct, string period);
        Task<List<LoanRegisterViewModel>> getListofLoanReportSummary(string batchno);


    }
}

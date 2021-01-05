using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Entities;
using NavyAccountCore.Models;
using NavyAccountWeb.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.Services
{
    public class LoanRegisterService: ILoanRegisterService
    {
        private readonly IUnitOfWork unitOfWork;
        public LoanRegisterService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;

        }
        public async Task<bool> AddLoanRegister(Pf_LoanRegister loanRegister)
        {
            unitOfWork.loanRegisterRepository.Create(loanRegister);
            return await unitOfWork.Done();
        }

        public async Task<bool> AddMultipleLoanRegister(List<Pf_LoanRegister> loanRegister)
        {
            unitOfWork.loanRegisterRepository.CreateRange(loanRegister);
            return await unitOfWork.Done();
        }


        public Task<Pf_LoanRegister> GetLoanRegisterById(int id)
        {
            return unitOfWork.loanRegisterRepository.Find(id);
            
        }

        public Task<decimal> loanAmountPerRankBy2(int rankid, string loanType, int fundtypecode)
        {
            return unitOfWork.loanRegisterRepository.loanAmountPerRankBy2(rankid, loanType);
        }
        public Task<decimal> loanAmountPerRankBy(int rankid, string loanType)
        {
            return unitOfWork.loanRegisterRepository.loanAmountPerRankBy(rankid,loanType);
        }

        public Task<loanPerRank> GetLoanAmountByLoanType(string loanType)
        {
            return unitOfWork.loanRegisterRepository.GetLoanAmountByLoanType(loanType);
        }

        public Task<List<LoanRegisterViewModel>> getListofLoanRegister(int fundtypeid)
        {
            return unitOfWork.loanRegisterRepository.getListofLoanRegister(fundtypeid);
        }
    
        public IEnumerable<LoanRegisterViewModel> getListofLoanRegister()
        {
            return unitOfWork.loanRegisterRepository.getListofLoanRegister();
        }
       
        public Task<List<LoanRegisterViewModel>> getListofLoanRegisterByStatus(int fundtypeid, string loanStatus)
        {
            return unitOfWork.loanRegisterRepository.getListofLoanRegisterByStatus(fundtypeid, loanStatus);
        }
        public Task<List<LoanRegisterViewModel222>> getListofLoanRegisterByBatch(string batchs)
        {
            return unitOfWork.loanRegisterRepository.getListofLoanRegisterByBatch(batchs);
        }
        public Task<List<LoanRegisterViewModel22>> getListofLoanRegisterByBatch2(string batchs)
        {
            return unitOfWork.loanRegisterRepository.getListofLoanRegisterByBatch2(batchs);
        }
        public Task<List<LoanRegisterViewModel>> getListofLoanReport(int fundtypeid, string loanstatus, DateTime startdate, DateTime enddate)
        {
            return unitOfWork.loanRegisterRepository.getListofLoanReport(fundtypeid,loanstatus,startdate,enddate);
        }
        public Task<List<LoanRegisterViewModel>> getListofLoanReportSummary(string batchno)
        {
            return unitOfWork.loanRegisterRepository.getListofLoanReportSummary(batchno);
        }
        public Task<List<LoanRegisterViewModel>> getLoanListPerPerson(int personID, int fundTypeID)
        {
            return unitOfWork.loanRegisterRepository.getListofLoanRegisterPerPerson(personID, fundTypeID);
        }

        public Task<List<LoanRegisterViewModel>> getListofLoanRegisterBTNByStatus(int fundtypeid, string loanStatus)
        {
            return unitOfWork.loanRegisterRepository.getListofLoanRegisterBTNByStatus(fundtypeid, loanStatus);
        }
        public Task<List<LoanRegisterViewModel>> getListofLoanRegisterBTNByStatusP(int fundtypeid, string loanStatus)
        {
            return unitOfWork.loanRegisterRepository.getListofLoanRegisterBTNByStatusP(fundtypeid, loanStatus);
        }

        public async Task<List<LoanRegisterViewModelRecieveable>> getListofLoanRegisterRecieveable(int loantypeid, string batch, bool ckforoutst)
        {
            return await unitOfWork.loanRegisterRepository.getListofLoanRegisterRecieveable(loantypeid, batch, ckforoutst);
        }
        public async Task<bool> UpdateLoanRegister(Pf_LoanRegister pf_LoanRegister)
        {
            unitOfWork.loanRegisterRepository.Update(pf_LoanRegister);
            return await unitOfWork.Done();
        }

        public async Task<List<Pf_LoanRegister>> getPendingLoanCount(int fundtypeid)
        {
            return await unitOfWork.loanRegisterRepository.getLoanRegisterPending(fundtypeid);
        }

        public async Task<List<LoanRegisterViewModel>> getApprovedLoancount(int fundtypeid)
        {
            return await unitOfWork.loanRegisterRepository.getLoanRegisterApproved(fundtypeid);
        }
        public async Task<List<Pf_LoanRegister>> GetLoanRegisterByApplication()
        {
            return await unitOfWork.loanRegisterRepository.GetLoanRegisterByApplication();
        }
        public async Task<List<Pf_LoanRegister>> getListofLoanRegisterByBatchDrp()
        {
            return await unitOfWork.loanRegisterRepository.getListofLoanRegisterByBatchDrp();
        }
        public async Task<List<Pf_LoanRegister>> getListofLoanRegisterBatchDrp()
        {
            return await unitOfWork.loanRegisterRepository.getListofLoanRegisterBatchDrp();
        }
        public async Task<List<npf_loanstatus>> getListofLoanStatus()
        {
            return await unitOfWork.loanRegisterRepository.getListofLoanStatus();
        }
        public async Task<List<npf_history>> getListofHistoryPeriod(string loanacct,string batchNo)
        {
            return await unitOfWork.loanRegisterRepository.getListofHistoryPeriod(loanacct,batchNo);
        }
        public async Task<List<npf_history>> getListofHistoryPeriod2(string loanacct,string period)
        {
            return await unitOfWork.loanRegisterRepository.getListofHistoryPeriod2(loanacct,period);
        }

    }
}


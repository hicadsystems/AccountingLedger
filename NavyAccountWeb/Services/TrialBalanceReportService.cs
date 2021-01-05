using NavyAccountCore.Core.Data;
using NavyAccountCore.Models;
using NavyAccountWeb.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.Services
{
    public class TrialBalanceReportService : ITrialbalanceReportService
    {
        private readonly IUnitOfWork unitOfWork;
        public TrialBalanceReportService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<LedgersView> GenerateMainAcct()
        {
            return unitOfWork.report.GetAllMainAct();
        }

        public IEnumerable<LedgersView> GenerateNpfHistory(string wdoc)
        {
            return unitOfWork.report.GetAllNPFHistory(wdoc);
        }

        public IEnumerable<LedgersView> GenerateTrialBalanceReport(string ind,string fundcode)
        {
            return unitOfWork.report.GetAllLedegers(ind,fundcode);
        }

      
        public IEnumerable<LedgersView> GenMainLedgers(List<LedgersView>pol)
        {
            return unitOfWork.report.GetMainLedgersByDate(pol);
        }

        public IEnumerable<LedgersView> GetLoanAct()
        {
            return unitOfWork.report.GetAllLoanTypes();
        }

        public string getMainActDesc(string code)
        {
            return unitOfWork.report.GetMainActDescription(code);
        }
    }
}

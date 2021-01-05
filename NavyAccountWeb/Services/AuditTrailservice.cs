using NavyAccountCore.Core.Data;
using NavyAccountCore.Models;
using NavyAccountWeb.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.Services
{
    public class AuditTrailservice : IAuditTrailServices
    {
        private readonly IUnitOfWork unitOfWork;
        public AuditTrailservice(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<LedgersView> GetAllRecord(string speriod,string acctcode)
        {
            return unitOfWork.trail.GetAllHistory(speriod,acctcode);
        }

     
        public IEnumerable<string> GetDateInHistory()
        {
            return unitOfWork.trail.GetAllyear();
        }

        public IEnumerable<LedgersView> GetNpfChart()
        {
            return unitOfWork.trail.GetAllNpfChart();
        }

        public string GetNpfDesc(string code)
        {
            return unitOfWork.trail.GetAccountDesc(code);
        }

        public LedgersView GetSingleRecord(string acctcode,string wdoc)
        {
            return unitOfWork.trail.GetOpenBal(acctcode,wdoc);
        }
    }
}

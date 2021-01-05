using NavyAccountCore.Core.Data;
using NavyAccountCore.Models;
using NavyAccountWeb.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.Services
{
    public class LoanScheduleServices : ILoanScheduleservices
    {
        private readonly IUnitOfWork unitOfWork;
        public LoanScheduleServices(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IEnumerable<LoanView> FilterAllLoanSchedule(int id, int loantypeid)
        {
            return unitOfWork.schedule.CalculateLoan(id,loantypeid);
        }

        public LoanView2 GetLoan(int personid, int loantype)
        {
            return unitOfWork.schedule.GetLoan(personid, loantype);
        }

        public int getLoanCount(int personid, int loantype)
        {
            return unitOfWork.schedule.getLoanCount(personid, loantype);
        }
    }
}

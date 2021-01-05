using NavyAccountCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.IServices
{
    public interface ILoanScheduleservices
    {
        IEnumerable<LoanView> FilterAllLoanSchedule(int id, int loantypeid);
        int getLoanCount(int personid, int loantype);
        LoanView2 GetLoan(int personid, int loantype);
    }
}

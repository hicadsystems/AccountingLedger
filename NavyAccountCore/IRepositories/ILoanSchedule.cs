using NavyAccountCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NavyAccountCore.Core.IRepositories
{
    public interface ILoanSchedule
    {
        IEnumerable<LoanView> CalculateLoan(int id, int loantypeid);
        int getLoanCount(int personid, int loantype);
        LoanView2 GetLoan(int personid, int loantype);
    }
}

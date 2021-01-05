using NavyAccountCore.Core.Entities;
using NavyAccountCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.IServices
{
    public interface ILedgerService
    {
        decimal getSurplus_LossC(string acctcode, string fundcode);
        decimal getSurplus_Loss(string acctcode, string fundcode);
        decimal getsumbybank(string acctcode);
        List<AcctLedgerViewModel> getLedgerInfoCSD(string fundcode);

        DischargeCalcView dischargeCalculation(string svcno, string fundcode);
    }
}

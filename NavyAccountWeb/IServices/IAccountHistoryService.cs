using System;
using System.Collections.Generic;
using System.Linq;
using NavyAccountCore.Core.Entities;
using System.Threading.Tasks;
using NavyAccountCore.Models;

namespace NavyAccountWeb.IServices
{
    public interface IAccountHistoryService
    {
        Task<List<AccountHistoryViewModel>> GetAccountHistory(string refno, string accountcode);
        List<int> getYearForReport();
    }
}

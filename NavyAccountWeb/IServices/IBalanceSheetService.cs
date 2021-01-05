using System;
using System.Collections.Generic;
using System.Linq;
using NavyAccountCore.Core.Entities;
using System.Threading.Tasks;

namespace NavyAccountWeb.IServices
{
    public interface IBalanceSheetService
    {
        IEnumerable<npf_balsheet> GetBalanceSheets();
        void RemoveBalsheet(npf_balsheet bl_sheet);
        Task<npf_balsheet> GetBalanceSheetById(int id);
        Task<npf_balsheet> GetBalanceSheetByCode(string bcode);
        Task<bool> AddBalanceSheet(npf_balsheet bl_sheet);
        Task<bool> UpdateBalanceSheet(npf_balsheet bl_sheet);
    }
}

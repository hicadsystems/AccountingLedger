using System;
using System.Collections.Generic;
using System.Linq;
using NavyAccountCore.Core.Entities;
using System.Threading.Tasks;

namespace NavyAccountWeb.IServices
{
    public interface IStockService
    {
        IEnumerable<npf_Stocks> GetStocks();
        void RemoveStock(npf_Stocks stock);
        Task<npf_Stocks> GetStockById(int id);
        Task<npf_Stocks> GetStockByCode(string bcode);
        Task<bool> AddStock(npf_Stocks stock);
        Task<bool> UpdateStock(npf_Stocks stock);
    }
}

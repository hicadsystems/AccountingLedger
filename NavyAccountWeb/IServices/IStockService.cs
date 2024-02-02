using System;
using System.Collections.Generic;
using System.Linq;
using NavyAccountCore.Core.Entities;
using System.Threading.Tasks;

namespace NavyAccountWeb.IServices
{
    public interface IStockService
    {
        IEnumerable<npf_stock> GetStocks();
        void RemoveStock(npf_stock stock);
        Task<npf_stock> GetStockById(int id);
        Task<npf_stock> GetStockByCode(string stockname);
        Task<bool> AddStock(npf_stock stock);
        Task<bool> UpdateStock(npf_stock stock);
    }
}

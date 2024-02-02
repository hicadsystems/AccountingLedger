using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Entities;
using NavyAccountWeb.IServices;

namespace NavyAccountWeb.Services
{
    public class StockService : IStockService
    {
        private readonly IUnitOfWork unitOfWork;
        public StockService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;

        }
        public Task<npf_stock> GetStockByCode(string stockname)
        {
            return unitOfWork.stock.GetStockByCode(x => x.stockname == stockname);

        }
        public async Task<bool> AddStock(npf_stock stock)
        {
            unitOfWork.stock.Create(stock);
            return await unitOfWork.Done();
        }

        public IEnumerable<npf_stock> GetStocks()
        {
            return unitOfWork.stock.All();
        }


        public Task<npf_stock> GetStockById(int id)
        {
            return unitOfWork.stock.Find(id);
        }

        public async Task<bool> UpdateStock(npf_stock stock)
        {
            unitOfWork.stock.Update(stock);
            return await unitOfWork.Done();
        }

        public void RemoveStock(npf_stock stock)
        {
            unitOfWork.stock.Remove(stock);
            unitOfWork.Done();
        }
    }
}

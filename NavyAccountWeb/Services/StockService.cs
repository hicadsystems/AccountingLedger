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
        public Task<npf_Stocks> GetStockByCode(string balcode)
        {
            return unitOfWork.stock.GetStocksByCode(x => x.stockname == balcode);

        }
        public async Task<bool> AddStock(npf_Stocks stock)
        {
            unitOfWork.stock.Create(stock);
            return await unitOfWork.Done();
        }

        public IEnumerable<npf_Stocks> GetStocks()
        {
            return unitOfWork.stock.All();
        }


        public Task<npf_Stocks> GetStockById(int id)
        {
            return unitOfWork.stock.Find(id);
        }

        public async Task<bool> UpdateStock(npf_Stocks stock)
        {
            unitOfWork.stock.Update(stock);
            return await unitOfWork.Done();
        }

        public void RemoveStock(npf_Stocks bl_sheet)
        {
            unitOfWork.stock.Remove(bl_sheet);
            unitOfWork.Done();
        }
    }
}

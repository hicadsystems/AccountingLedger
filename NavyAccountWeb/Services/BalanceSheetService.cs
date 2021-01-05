using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Entities;
using NavyAccountWeb.IServices;

namespace NavyAccountWeb.Services
{
    public class BalanceSheetService : IBalanceSheetService
    {
        private readonly IUnitOfWork unitOfWork;
        public BalanceSheetService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;

        }
        public Task<npf_balsheet> GetBalanceSheetByCode(string balcode)
        {
            return unitOfWork.balSheet.GetBalanceSheetByCode(x => x.bl_code == balcode);

        }
        public async Task<bool> AddBalanceSheet(npf_balsheet balsheet)
        {
            unitOfWork.balSheet.Create(balsheet);
            return await unitOfWork.Done();
        }

        public IEnumerable<npf_balsheet> GetBalanceSheets()
        {
            return unitOfWork.balSheet.All();
        }


        public Task<npf_balsheet> GetBalanceSheetById(int id)
        {
            return unitOfWork.balSheet.Find(id);
        }

        public async Task<bool> UpdateBalanceSheet(npf_balsheet balsheet)
        {
            unitOfWork.balSheet.Update(balsheet);
            return await unitOfWork.Done();
        }

        public void RemoveBalsheet(npf_balsheet bl_sheet)
        {
            unitOfWork.balSheet.Remove(bl_sheet);
            unitOfWork.Done();
        }
    }
}

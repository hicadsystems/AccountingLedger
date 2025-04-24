using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Entities;
using NavyAccountCore.Core.IRepositories;
using NavyAccountCore.Models;
using System.Linq;
using NavyAccountWeb.IServices;

namespace NavyAccountWeb.Services
{
    public class AccountHistoryService : IAccountHistoryService
    {
        private readonly IUnitOfWork unitOfWork;
        public AccountHistoryService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<List<AccountHistoryViewModel>> GetAccountHistory(string refno, string accountcode)
        {
            var getAc = await unitOfWork.accountHistory.GetAccountHistoryList(refno, accountcode);
            List<AccountHistoryViewModel> llA = new List<AccountHistoryViewModel>();

            foreach (var e in getAc)
            {
                llA.Add(new AccountHistoryViewModel()
                {
                    documentno = e.docno,
                    creditAmount = (decimal)e.cramt,
                    debitAmount = (decimal)e.dbamt,
                    dateoftransaction = e.docdate.ToString(),
                    remarks = e.remarks
                });
            }

            return llA
                .OrderBy(x => x.dateoftransaction)
                .ToList();
        }


        public List<int> getYearForReport()
        {

            return  unitOfWork.npfHistories.All().Select(x=>x.docdate.Value.Year).Distinct().ToList();
        }

    }
}

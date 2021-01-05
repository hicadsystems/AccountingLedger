using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Entities;
using NavyAccountCore.Models;
using NavyAccountWeb.IServices;

namespace NavyAccountWeb.Services
{
    public class MainAccountService : IMainAccountService
    {
        private readonly IUnitOfWork unitOfWork;
        public MainAccountService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;

        }
        public Task<npf_mainact> GetMainAccountByCode(string maincode)
        {
            return unitOfWork.mainAccount.GetMainAccountByCode(x => x.maincode == maincode);

        }

        public Task<npf_mainact> GetLastMainAccountByCode(string maincode)
        {
            return unitOfWork.mainAccount.GetLastMainAccountByCode(maincode);

        }

        public async Task<bool> AddMainAccount(npf_mainact npf_Mainact)
        {
            unitOfWork.mainAccount.Create(npf_Mainact);
            return await unitOfWork.Done();
        }

        public IEnumerable<npf_mainact> GetMainAccounts()
        {
            return unitOfWork.mainAccount.All();
        }

        public Task<List<MainAccountView>> GetMainAccountDesc() {
            return unitOfWork.mainAccount.GetMainAccountDesc();
        }

        public Task<npf_mainact> GetMainAccountById(int id)
        {
            return unitOfWork.mainAccount.Find(id);
        }

        public async Task<bool> UpdateMainAccount(npf_mainact npf_Mainact)
        {
            unitOfWork.mainAccount.Update(npf_Mainact);
            return await unitOfWork.Done();
        }

        public void RemoveMainAct(npf_mainact mainact)
        {
            unitOfWork.mainAccount.Remove(mainact);
            unitOfWork.Done();
        }

        public IEnumerable<MainAccountView> GetMainPerPage(int iDisplayStart, int iDisplayLength, out int totalcount)
        {
            return unitOfWork.mainAccount.GetMainPerPage(iDisplayStart, iDisplayLength, out totalcount);
        }
    }
}

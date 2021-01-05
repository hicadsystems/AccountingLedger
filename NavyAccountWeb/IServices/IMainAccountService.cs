using System;
using System.Collections.Generic;
using System.Linq;
using NavyAccountCore.Core.Entities;
using System.Threading.Tasks;
using NavyAccountCore.Models;

namespace NavyAccountWeb.IServices
{
    public interface IMainAccountService
    {
        IEnumerable<npf_mainact> GetMainAccounts();

        IEnumerable<MainAccountView> GetMainPerPage(int iDisplayStart, int iDisplayLength, out int totalcount);
        Task<npf_mainact> GetMainAccountById(int id);
        Task<npf_mainact> GetMainAccountByCode(string bcode);
        Task<npf_mainact> GetLastMainAccountByCode(string bcode);
        Task<List<MainAccountView>> GetMainAccountDesc();
        Task<bool> AddMainAccount(npf_mainact npf_Mainact);
        Task<bool> UpdateMainAccount(npf_mainact npf_Mainact);
        void RemoveMainAct(npf_mainact mainact);
    }
}



using NavyAccountCore.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NavyAccountWeb.IServices
{
    public interface IMenuService
    {
        IEnumerable<Menu> GetActiveMenus();
        IEnumerable<Menu> GetMenus();
        Task<Menu> GetMenuById(int id);
        Task<bool> AddMenu(Menu menu);
        Task<bool> UpdateMenu(Menu menu);
        Menu GetMenuByCode(string code);
    }
}

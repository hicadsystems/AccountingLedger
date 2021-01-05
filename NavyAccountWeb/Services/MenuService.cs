using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Entities;
using NavyAccountWeb.IServices;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.Services
{
    public class MenuService : IMenuService
    {
        private readonly IUnitOfWork unitOfWork;
        public MenuService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Menu> GetMenus()
        {
            return unitOfWork.Menus.All();
        }

        public IEnumerable<Menu> GetActiveMenus()
        {
            return unitOfWork.Menus.GetByExpression(x => x.IsActive);
        }

        public async Task<bool> AddMenu(Menu menu)
        {
            unitOfWork.Menus.Create(menu);
            return await unitOfWork.Done();

        }

        public async Task<Menu> GetMenuById(int id)
        {
            return await unitOfWork.Menus.Find(id);
        }

        public Menu GetMenuByCode(string code)
        {
            return unitOfWork.Menus.GetByExpression(x => x.Code == code).FirstOrDefault();
        }

        public async Task<bool> UpdateMenu(Menu menu)
        {
            unitOfWork.Menus.Update(menu);
            return await unitOfWork.Done();
        }
    }
}

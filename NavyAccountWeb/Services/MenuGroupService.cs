
using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Entities;
using NavyAccountWeb.IServices;
using System.Collections.Generic;

namespace NavyAccountWeb.Services
{
    public class MenuGroupService : IMenuGroupService
    {
        private readonly IUnitOfWork unitOfWork;
        public MenuGroupService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<MenuGroup> GetMenuGroupss()
        {
            return unitOfWork.MenuGroups.All();
        }

        public IEnumerable<MenuGroup> GetActiveMenuGroups()
        {
            return null;
        }
    }
}

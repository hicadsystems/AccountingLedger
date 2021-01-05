using NavyAccountCore.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.IServices
{
    public interface IMenuGroupService
    {
        IEnumerable<MenuGroup> GetActiveMenuGroups();
        IEnumerable<MenuGroup> GetMenuGroupss();
    }
}

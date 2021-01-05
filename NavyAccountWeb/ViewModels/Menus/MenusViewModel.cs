using NavyAccountCore.Core.Entities;
using System.Collections.Generic;

namespace NavyAccountWeb.ViewModels.Menus
{
    public class MenusViewModel
    {
        public IEnumerable<Menu> Menus { get; set; }
        public IEnumerable<MenuGroup> MenuGroups { get; set; }
    }
}

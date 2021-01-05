using NavyAccountCore.Core.Entities;
using System.Collections.Generic;

namespace NavyAccountWeb.ViewModels.Roles
{
    public class RolesViewModel
    {
        public IEnumerable<Role> Roles { get; set; }
        public IEnumerable<Menu> Menus { get; set; }
    }
}

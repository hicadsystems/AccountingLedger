

using NavyAccountCore.Core.Entities;
using System.Collections.Generic;

namespace NavyAccountWeb.ViewModels.Users
{
    public class UsersViewModel
    {
        public List<User> Users { get; set; }

        public IEnumerable<Role> Roles { get; set; }

    }
}



using Microsoft.AspNetCore.Identity;
using NavyAccountCore.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NavyAccountWeb.IServices
{
    public interface IRoleService
    {
        IEnumerable<Role> GetRoles();
        IEnumerable<Role> GetActiveRoles();
        Task<bool> AddRole(Role role, int[] menus);
        Task<Role> GetRoleById(int id);
        Task<IdentityResult> UpdateRole(Role role);
        Task<Role> GetRoleWithMenuById(int id);
        Task<Role> GetRoleByName(string name);
        Task<IdentityResult> UpdateRoleInfo(Role role, int[] menus);
    }
}

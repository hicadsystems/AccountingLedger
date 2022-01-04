

using Microsoft.EntityFrameworkCore;
using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Entities;
using NavyAccountCore.Core.IRepositories;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NavyAccountCore.Core.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly INavyAccountDbContext context;

        public UserRepository(INavyAccountDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<User> User(Expression<Func<User, bool>> predicate)
        {
           var reu = await context.Users
                .Include(x => x.UserRoles)
                .ThenInclude(x => x.Role)
                .FirstOrDefaultAsync(predicate);
            return reu;
        }

        public async Task<User> UserWithRolesandMenus(Expression<Func<User, bool>> predicate)
        {
            return await context.Users
                .Include(x => x.UserRoles)
                .ThenInclude(x => x.Role)
                .ThenInclude(x => x.RoleMenus)
                .ThenInclude(x => x.Menu)
                .ThenInclude(x => x.MenuGroup)
                .FirstOrDefaultAsync(predicate);
        }

        public async Task<User> UserWithRoles(Expression<Func<User, bool>> predicate)
        {

            try
            {
                var pp = await context.Users
                            .Include(x => x.UserRoles)
                            .ThenInclude(x => x.Role).FirstOrDefaultAsync(predicate);
                return pp;
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }

      
    }
}

using NavyAccountCore.Core.Entities;
using NavyAccountCore.Core.IRepositories;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NavyAccountCore.Core.IRepositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> UserWithRoles(Expression<Func<User, bool>> predicate);
        Task<User> UserWithRolesandMenus(Expression<Func<User, bool>> predicate);
        Task<User> User(Expression<Func<User, bool>> predicate);
    }
}

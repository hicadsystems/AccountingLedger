
using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Entities;
using NavyAccountCore.Core.IRepositories;

namespace NavyAccountCore.Core.Repositories
{
    public class UserRoleRepository : Repository<UserRole>, IUserRoleRepository
    {
        private readonly INavyAccountDbContext context;

        public UserRoleRepository(INavyAccountDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}

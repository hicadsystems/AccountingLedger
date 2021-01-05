using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Entities;
using NavyAccountCore.Core.IRepositories;

namespace NavyAccountCore.Core.Repositories
{
    public class RoleMenuRepository : Repository<RoleMenu>, IRoleMenuRepository
    {
        private readonly INavyAccountDbContext context;

        public RoleMenuRepository(INavyAccountDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}

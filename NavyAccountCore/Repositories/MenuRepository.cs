using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Entities;
using NavyAccountCore.Core.IRepositories;

namespace NavyAccountCore.Core.Repositories
{
    public class MenuRepository : Repository<Menu>, IMenuRepository
    {
        private readonly INavyAccountDbContext context;
        public MenuRepository(INavyAccountDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}

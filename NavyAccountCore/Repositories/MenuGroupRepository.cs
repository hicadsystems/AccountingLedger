using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Entities;
using NavyAccountCore.Core.IRepositories;


namespace NavyAccountCore.Core.Repositories
{
    public class MenuGroupRepository : Repository<MenuGroup>, IMenuGroupRepository
    {
        private readonly INavyAccountDbContext context;

        public MenuGroupRepository(INavyAccountDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}

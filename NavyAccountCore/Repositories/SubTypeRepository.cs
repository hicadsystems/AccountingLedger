using NavyAccountCore.Core.Entities;
using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.IRepositories;

namespace NavyAccountCore.Core.Repositories
{
    public class SubTypeRepository : Repository<sub_type>, ISubType
    {
        private readonly INavyAccountDbContext context;
        public SubTypeRepository(INavyAccountDbContext context) : base(context)
        {
            this.context = context;
        }


    }
}
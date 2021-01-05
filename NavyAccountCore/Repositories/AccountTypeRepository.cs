using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Entities;
using NavyAccountCore.Core.IRepositories;
using NavyAccountCore.Core.Repositories;

namespace NavyAccountCore.Core.Repositories
{
    public class AccountTypeRepository : Repository<ac_accounttypes>, IAccountType
    {
        private readonly INavyAccountDbContext context;
        public AccountTypeRepository(INavyAccountDbContext context) : base(context)
        {
            this.context = context;
        }

    }
}


using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Entities;
using NavyAccountCore.Core.IRepositories;

namespace NavyAccountCore.Core.Repositories
{
    public class RelationshipRepo: Repository<Relationship>, IRelationship
    {
        private readonly INavyAccountDbContext context;

        public RelationshipRepo(INavyAccountDbContext context) : base(context)
        {
            this.context = context;
        }

    }
}
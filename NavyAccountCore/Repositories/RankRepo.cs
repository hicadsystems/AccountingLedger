using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Entities;
using NavyAccountCore.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace NavyAccountCore.Core.Repositories
{
    public class RankRepo : Repository<Rank>, IRank
    {
        private readonly INavyAccountDbContext context;
        public RankRepo(INavyAccountDbContext context) : base(context)
        {
            this.context = context;
        }
        public Rank GetRankbyName(Expression<Func<Rank, bool>> predicate)
        {
            return context.ranks.FirstOrDefault(predicate);
        }
        public List<Rank> getrankbytitle()
        {
            return context.ranks.Where(x => x.Id > 21).ToList();
        }
    }
}
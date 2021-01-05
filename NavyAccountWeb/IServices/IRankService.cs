using System;
using System.Collections.Generic;
using System.Linq;
using NavyAccountCore.Core.Entities;
using System.Threading.Tasks;

namespace NavyAccountWeb.IServices
{
    public interface IRankService
    {
        IEnumerable<Rank> GetRanks();
        Task<bool> AddRank(Rank rank);
        List<Rank> GetRanks2();
    }
}

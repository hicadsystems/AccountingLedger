using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Entities;
using NavyAccountWeb.IServices;

namespace NavyAccountWeb.Services
{
    public class RankService : IRankService
    {
        private readonly IUnitOfWork unitOfWork;
        public RankService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;

        }
        
        public async Task<bool> AddRank(Rank rank)
        {
            unitOfWork.rank.Create(rank);
            return await unitOfWork.Done();
        }

        public IEnumerable<Rank> GetRanks()
        {
            return unitOfWork.rank.All();
        }
        public List<Rank> GetRanks2()
        {
            return unitOfWork.rank.getrankbytitle();
        }


    }
}

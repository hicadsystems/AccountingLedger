using NavyAccountCore.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NavyAccountCore.Core.IRepositories
{
    public interface IAccountHistory : IRepository<npf_history>
    {
        Task<List<npf_history>> GetAccountHistoryList(string refno, string accountCode);
    }
}

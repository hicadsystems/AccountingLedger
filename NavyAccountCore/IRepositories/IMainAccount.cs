using NavyAccountCore.Core.Entities;
using NavyAccountCore.Core.IRepositories;
using NavyAccountCore.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NavyAccountCore.Core.IRepositories
{
    public interface IMainAccount:IRepository<npf_mainact>
    {
        Task<npf_mainact> GetMainAccountByCode(Expression<Func<npf_mainact, bool>> predicate);
        Task<npf_mainact> GetLastMainAccountByCode(string maincode);
        Task<List<MainAccountView>> GetMainAccountDesc();
        IEnumerable<MainAccountView> GetMainPerPage(int iDisplayStart, int iDisplayLength, out int totalcount);

    }
}

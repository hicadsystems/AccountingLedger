
using NavyAccountCore.Core.Entities;
using NavyAccountCore.Core.IRepositories;
using NavyAccountCore.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NavyAccountCore.Core.IRepositories
{
    public interface IAccountChart:IRepository<npf_chart>
    {
        Task<IEnumerable<ChartofAccountView>> getAllChartofAccountsCH();
        Task<List<ChartofAccountView>> GetChartofAccountByCode5(string maincode);
        Task<npf_chart> GetChartofAccountByCode(Expression<Func<npf_chart, bool>> predicate);
        Task<List<ChartofAccountView>> GetChartofAccountByCode2(string maincode);
        Task<List<ChartofAccountView>> GetChartofAccountByCode3(string maincode);
        Task<List<ChartofAccountView>> GetChartofAccountByCode4(string maincode);
        Task<List<ChartofAccountView>> GetChartofAccountByCode1(string maincode);
        Task<npf_chart> GetLasChartofAccountByCode(string maincode);
        Task<List<ChartofAccountView>> getAllChartofAccounts_Desc();
        Task<List<ChartofAccountView2>> getAllChartofAccounts_Desc2(int iDisplayStart, int iDisplayLength);
        Task<int> getChartofAccountListCount();
        Task<List<ChartofAccountView>> getListChartofAccounts_DescBymainAccount(string mainActCode);
        Task<ChartofAccountView> getSingleChartofAccounts_Desc(string mainActCode);
        
        
    }
}
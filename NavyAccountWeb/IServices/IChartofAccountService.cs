using System;
using System.Collections.Generic;
using System.Linq;
using NavyAccountCore.Core.Entities;
using System.Threading.Tasks;
using NavyAccountCore.Models;

namespace NavyAccountWeb.IServices
{
    public interface IChartofAccountService
    {
        Task<IEnumerable<ChartofAccountView>> getAllChartofAccountsCH();
        IEnumerable<npf_chart> GetChartofAccounts();
        Task<npf_chart> GetChartofAccountById(int id);
        Task<npf_chart> GetChartofAccountByCode(string bcode);
        Task<List<ChartofAccountView>> GetChartofAccountByCode1(string bcode);
        Task<List<ChartofAccountView>> GetChartofAccountByCode2(string bcode);
        Task<List<ChartofAccountView>> GetChartofAccountByCode3(string bcode);
        Task<List<ChartofAccountView>> GetChartofAccountByCode4(string bcode);
        Task<List<ChartofAccountView>> GetChartofAccountByCode5(string bcode);
        Task<List<ChartofAccountView2>> getChartofAccountAndDesc2(int iDisplayStart, int iDisplayLength);
        Task<int> getChartofAccountCount();
        Task<List<ChartofAccountView>> getChartofAccountAndDesc();
        Task<npf_chart> GetLastChartofAccountByCode(string bcode);
        Task<List<ChartofAccountView>> getListChartofAccounts_DescBymainAccount(string mainActCode);
      
        Task<ChartofAccountView> getSingleChartofAccounts_Desc(string actCode);
        Task<bool> AddChartofAccount(npf_chart npf_Chart);
        Task<bool> UpdateChartofAccount(npf_chart npf_Chart);
        void RemoveChart(npf_chart npf_Chart);
    }
}

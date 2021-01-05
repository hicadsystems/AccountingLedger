using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Entities;
using NavyAccountCore.Models;
using NavyAccountWeb.IServices;

namespace NavyAccountWeb.Services
{
    public class ChartofAccountService : IChartofAccountService
    {
        private readonly IUnitOfWork unitOfWork;
        public ChartofAccountService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;

        }
        public async Task<int> getChartofAccountCount()
        {
            return await unitOfWork.accountChart.getChartofAccountListCount();
        }

        public async Task<List<ChartofAccountView2>> getChartofAccountAndDesc2(int iDisplayStart, int iDisplayLength)
        {
            return await unitOfWork.accountChart.getAllChartofAccounts_Desc2(iDisplayStart, iDisplayLength);
        }

        public Task<npf_chart> GetChartofAccountByCode(string actcode)
        {
            return unitOfWork.accountChart.GetChartofAccountByCode(x => x.acctcode == actcode);

        }
        public Task<IEnumerable<ChartofAccountView>> getAllChartofAccountsCH()
        {
            return unitOfWork.accountChart.getAllChartofAccountsCH();

        }
        public Task<List<ChartofAccountView>> GetChartofAccountByCode1(string actcode)
        {
            return unitOfWork.accountChart.GetChartofAccountByCode1(actcode);

        }
        public Task<List<ChartofAccountView>> GetChartofAccountByCode2(string actcode)
        {
            return unitOfWork.accountChart.GetChartofAccountByCode2(actcode);

        }
        public Task<List<ChartofAccountView>> GetChartofAccountByCode3(string actcode)
        {
            return unitOfWork.accountChart.GetChartofAccountByCode3(actcode);

        }
        public Task<List<ChartofAccountView>> GetChartofAccountByCode4(string actcode)
        {
            return unitOfWork.accountChart.GetChartofAccountByCode4(actcode);

        }
        public Task<List<ChartofAccountView>> GetChartofAccountByCode5(string actcode)
        {
            return unitOfWork.accountChart.GetChartofAccountByCode5(actcode);

        }

        public Task<npf_chart> GetLastChartofAccountByCode(string actcode)
        {
            return unitOfWork.accountChart.GetLasChartofAccountByCode(actcode);

        }
        public async Task<bool> AddChartofAccount(npf_chart npf_Chart)
        {
            unitOfWork.accountChart.Create(npf_Chart);
            return await unitOfWork.Done();
        }

        public IEnumerable<npf_chart> GetChartofAccounts()
        {
            return unitOfWork.accountChart.All();
        }

        public Task<List<ChartofAccountView>> getChartofAccountAndDesc()
        {
            return unitOfWork.accountChart.getAllChartofAccounts_Desc();
        }
      
       
        public Task<List<ChartofAccountView>> getListChartofAccounts_DescBymainAccount(string mainActCode)
        {
            return unitOfWork.accountChart.getListChartofAccounts_DescBymainAccount(mainActCode);
        }

        public Task<ChartofAccountView> getSingleChartofAccounts_Desc(string actCode)
        {
            return unitOfWork.accountChart.getSingleChartofAccounts_Desc(actCode);
        }


        public Task<npf_chart> GetChartofAccountById(int id)
        {
            return unitOfWork.accountChart.Find(id);
        }

        public async Task<bool> UpdateChartofAccount(npf_chart npf_Chart)
        {
            unitOfWork.accountChart.Update(npf_Chart);
            return await unitOfWork.Done();
        }

        public void RemoveChart(npf_chart npf_Chart)
        {
            unitOfWork.accountChart.Remove(npf_Chart);
            unitOfWork.Done();
        }
    }
}

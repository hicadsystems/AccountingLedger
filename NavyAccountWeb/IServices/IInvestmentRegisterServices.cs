using NavyAccountCore.Core.Entities;
using NavyAccountCore.Models;
using NavyAccountWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.IServices
{
    public interface IInvestmentRegisterServices
    {
        IEnumerable<InvestmentView> GetAllInvestRegister();
        List<InvestmentView> GetAllInvestRegisterOST(DateTime startdate,DateTime enddate);
        IEnumerable<InvestmentView> GetAllInvestRegister2();
        Task<Pf_InvestRegister> GetinvestRegisterById(int id);
        Task<Pf_InvestRegister> GetInvestRegisterByCompanyId(int companyId);
        Task<bool> AddInvestRegister(Pf_InvestRegister register);
        Task<bool> UpdateInvestRegister(Pf_InvestRegister register);
        Task<List<PersonView>> GetAllCompany();
        Task<List<BankView>> GetBanKRecord();
        List<InvestmentView> GetALLInvestListOT();
        List<InvestmentView> GetALLInvestListCapitalMk();
        List<InvestmentView> GetALLInvestListCapitalMk(DateTime startdate, DateTime enddate);
    }
}

using NavyAccountCore.Core.Entities;
using NavyAccountCore.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NavyAccountCore.Core.IRepositories
{
    public interface IInvestmentRegister : IRepository<Pf_InvestRegister>
    {
        Task<List<PersonView>> GetCompanyList();
        Task<List<BankView>> GetBankList();
        List<InvestmentView> GetInvestList();
        List<InvestmentView> GetInvestListOST(DateTime startdate, DateTime enddate);
        List<InvestmentView> GetInvestList2();
        Task<Pf_InvestRegister> GetInvesRegisterByUser(Expression<Func<Pf_InvestRegister, bool>> predicate);
        List<InvestmentView> GetALLInvestListOT();
    }
}

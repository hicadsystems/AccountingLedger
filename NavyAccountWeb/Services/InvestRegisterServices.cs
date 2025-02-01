using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Entities;
using NavyAccountCore.Models;
using NavyAccountWeb.IServices;
using NavyAccountWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.Services
{
    public class InvestRegisterServices : IInvestmentRegisterServices
    {
        private readonly IUnitOfWork unitOfWork;

        public InvestRegisterServices(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<bool> AddInvestRegister(Pf_InvestRegister register)
        {
            unitOfWork.register.Create(register);
            return await unitOfWork.Done();
        }

        public IEnumerable<InvestmentView> GetAllInvestRegister()
        {
            return unitOfWork.register.GetInvestList().ToList();
        }
        public IEnumerable<InvestmentView> GetAllInvestRegisterOST(DateTime startdate, DateTime enddate)
        {
            return unitOfWork.register.GetInvestListOST(startdate,enddate).ToList();
        }
        public IEnumerable<InvestmentView> GetAllInvestRegister2()
        {
            return unitOfWork.register.GetInvestList2().ToList();
        }
        public List<InvestmentView> GetALLInvestListOT()
        {
            return unitOfWork.register.GetALLInvestListOT().ToList();
        }

        public Task<List<PersonView>> GetAllCompany()
        {
            return unitOfWork.register.GetCompanyList();
        }

        public Task<List<BankView>> GetBanKRecord()
        {
            return unitOfWork.register.GetBankList();
        }

        public Task<Pf_InvestRegister> GetinvestRegisterById(int id)
        {
            return unitOfWork.register.Find(id);
        }

        public Task<Pf_InvestRegister> GetInvestRegisterByCompanyId(int id)
        {
            return unitOfWork.register.GetInvesRegisterByUser(x => x.IssuanceBankId==id);
        }


        public async Task<bool> UpdateInvestRegister(Pf_InvestRegister register)
        {
            unitOfWork.register.Update(register);
            return await unitOfWork.Done();
        }
    }
}

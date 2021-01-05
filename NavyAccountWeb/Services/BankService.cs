using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Entities;
using NavyAccountWeb.IServices;

namespace NavyAccountWeb.Services
{
    public class BankService : IBankService
    {
        private readonly IUnitOfWork unitOfWork;
        public BankService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;

        }
        

        public IEnumerable<py_bank> GetBanks()
        {
            return unitOfWork.bank.All();
        }


    }
}

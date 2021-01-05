using NavyAccountCore.Core.Data;
using NavyAccountCore.Entities;
using NavyAccountCore.Models;
using NavyAccountWeb.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.Services
{
    public class ContrdiscService : IContrService
    {
        private readonly IUnitOfWork unitOfWork;
        public ContrdiscService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;

        }
        public IEnumerable<ContrDiscVM> GetAllbyFundcode(string fundcode)
        {
            return unitOfWork.npf_contrdisc.Getbyfundcode(fundcode);
        }
        public async Task<bool> AddLoandisc(npf_contrdisc loan)
        {
            unitOfWork.npf_contrdisc.Create(loan);
            return await unitOfWork.Done();
        }
        public async Task<bool> DeleteLoandisc(npf_contrdisc loan)
        {
            unitOfWork.npf_contrdisc.Remove(loan);
            return await unitOfWork.Done();
        }
      }
    }

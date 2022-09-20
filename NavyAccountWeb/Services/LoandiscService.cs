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
    public class LoandiscService: ILoandiscService
    {
        private readonly IUnitOfWork unitOfWork;
        public LoandiscService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;

        }
        public IEnumerable<LoandiscVM> GetAllbyFundcode(string fundcode, string batch)
        {
            return unitOfWork.pf_loandisc.Getbyfundcode(fundcode,batch);
        }
        public IEnumerable<LoandiscVM> GetAllbyFundcode(string fundcode)
        {
            return unitOfWork.pf_loandisc.Getbyfundcode(fundcode);
        }
        public IEnumerable<LoandiscVM> GetAllbyFundcodeandsvcno(string fundcode,string svcno,string batchno)
        {
            return unitOfWork.pf_loandisc.Getbyfundcodeandsvcno(fundcode,svcno,batchno);
        }
        public async Task<bool> AddLoandisc(pf_loandisc loan)
        {
            unitOfWork.pf_loandisc.Create(loan);
            return await unitOfWork.Done();
        }
        public async Task<bool> DeleteLoandisc(pf_loandisc loan)
        {
            unitOfWork.pf_loandisc.Remove(loan);
            return await unitOfWork.Done();
        }
        public async Task<List<pf_loandisc>> getListofLoandiscByBatchDrp()
        {
            return await unitOfWork.pf_loandisc.GetloanDiscByBatchdrp();
        }

    }
}

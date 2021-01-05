using NavyAccountCore.Entities;
using NavyAccountCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.IServices
{
    public interface ILoandiscService
    {
        IEnumerable<LoandiscVM> GetAllbyFundcode(string fundcode, string batch);
        IEnumerable<LoandiscVM> GetAllbyFundcode(string fundcode);
        IEnumerable<LoandiscVM> GetAllbyFundcodeandsvcno(string fundcode, string svcno);
        Task<bool> AddLoandisc(pf_loandisc loan);
        Task<bool> DeleteLoandisc(pf_loandisc loan);
        Task<List<pf_loandisc>> getListofLoandiscByBatchDrp();
    }
}

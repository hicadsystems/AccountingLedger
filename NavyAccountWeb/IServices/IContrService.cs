using NavyAccountCore.Entities;
using NavyAccountCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.IServices
{
    public interface IContrService
    {
        IEnumerable<ContrDiscVM> GetAllbyFundcode(string fundcode);
        Task<bool> AddLoandisc(npf_contrdisc loan);
        Task<bool> DeleteLoandisc(npf_contrdisc loan);
    }
}

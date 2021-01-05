using NavyAccountCore.Core.Entities;
using NavyAccountCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.IServices
{
   public interface INavipService
    {
        Task<npf_NavipContribution> checksvc(int personid);
        Task<bool> AddNavip(npf_NavipContribution navips);
        Task<bool> updateNavip(npf_NavipContribution navips);
        List<navipVM2> getallnavip(string batch);
        List<navipVM4> getnavipbydate(DateTime startdate, DateTime enddate);
        List<navipVM5> getnavipbydate2(DateTime startdate, DateTime enddate);
    }
}

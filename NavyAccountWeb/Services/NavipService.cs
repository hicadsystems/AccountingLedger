using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Entities;
using NavyAccountCore.Models;
using NavyAccountWeb.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.Services
{
    public class NavipService: INavipService
    {
        private readonly IUnitOfWork unitOfWork;
        public NavipService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public Task<npf_NavipContribution> checksvc(int personid)
        {
            return unitOfWork.navip.GetNavipByCode(x=>x.PersonID==personid);
        }
        public List<navipVM2> getallnavip(string batch)
        {
            return unitOfWork.navip.getnavipbybatch(batch);
        }
        public List<navipVM4> getnavipbydate(DateTime startdate, DateTime enddate)
        {
            return unitOfWork.navip.getnavipbydate(startdate, enddate);
        }
        public List<navipVM5> getnavipbydate2(DateTime startdate, DateTime enddate)
        {
            return unitOfWork.navip.getnavipbydate2(startdate, enddate);
        }
        public async Task<bool> AddNavip(npf_NavipContribution navips)
        {
            unitOfWork.navip.Create(navips);
            return await unitOfWork.Done();
        }
        public async Task<bool> updateNavip(npf_NavipContribution navips)
        {
            unitOfWork.navip.Update(navips);
            return await unitOfWork.Done();
        }

    }
}

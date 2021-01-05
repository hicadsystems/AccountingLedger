using NavyAccountCore.Core.Data;
using NavyAccountCore.Models;
using NavyAccountWeb.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.Services
{
    public class SurplusService : ISurplusService
    {
        private IUnitOfWork unitOfWork;
        public SurplusService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public string description(string code)
        {
            return unitOfWork.surplus.getDescription(code);
        }

        public IEnumerable<LedgersView2> GetAllSurplus()
        {
            return unitOfWork.surplus.GetAllSurplusOrDeficit();
        }

        public IEnumerable<LedgersView2> GetAllSurplus2()
        {
            return unitOfWork.surplus.GetAllSurplusOrDeficit2();
        }

        public IEnumerable<LedgersView> GetAllSurplusbyDoc(string doc, string fundcode)
        {
           return unitOfWork.surplus.GetAllSurplusOrdeficitByDoc(doc, fundcode);
        }

        public IEnumerable<LedgersView2> GetBalSheet()
        {
            return unitOfWork.surplus.GetBalsheet();
        }
        public IEnumerable<LedgersView2> GetBalSheet_TrialBalance()
        {
            return unitOfWork.surplus.GetBalSheet_TrialBalance();
        }

        public IEnumerable<LedgersView2> GetBalSheet_MainTrialBalance()
        {
            return unitOfWork.surplus.GetBalSheet_MainTrialBalance();
        }


        public IEnumerable<LedgersView2> GetSurplus_Deficit()
        {
            return unitOfWork.surplus.GetSurplus_Deficit();
        }

        public IEnumerable<LedgersView2> GetBalanceSheetSurplus_Deficit()
        {
            return unitOfWork.surplus.GetBalanceSheetSurplus_Deficit();
        }
        public IEnumerable<LedgersView2> GetBalSheet2()
        {
            return unitOfWork.surplus.GetBalsheet2();
        }

        public string RefractCode(string code)
        {
            return unitOfWork.surplus.RefractCode(code);
        }
    }
}

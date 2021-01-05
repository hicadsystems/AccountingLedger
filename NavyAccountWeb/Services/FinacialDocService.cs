using NavyAccountCore.Core.Data;
using NavyAccountCore.Models;
using NavyAccountWeb.IServices;
using NavyAccountWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.Services
{
    public class FinacialDocService: IFinancialDocService
    {
        private readonly IUnitOfWork unitOfWork;
        public FinacialDocService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;

        }
        public IEnumerable<DocumentEnquiryVM> GetbyFinancialdocByRpt(string reciept)
        {
            return unitOfWork.npfHistories.GetbyFinancialdocByRpt(reciept);
        }
        public IEnumerable<DocumentEnquiryVM> GetbyFinancialdocByRpt2(string reciept)
        {
            return unitOfWork.npfHistories.GetbyFinancialdocByRpt2(reciept);
        }
    }
}

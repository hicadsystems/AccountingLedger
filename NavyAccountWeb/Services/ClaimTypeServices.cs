using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Entities;
using NavyAccountWeb.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.Services
{
    public class ClaimTypeServices : IClaimTypeServices
    {
        private readonly IUnitOfWork unitOfWork;
        public ClaimTypeServices(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public bool checkClaimBatchnoExist(string batchno)
        {
            return unitOfWork.cam.checkClaimBatchnoExist(batchno);
        }

        public string fundtypeDesc(string code)
        {
            return unitOfWork.cam.fundTypeDesc(code);
        }

        public List<Npf_ClaimRegister> GetBatchList()
        {
            return unitOfWork.cam.GetBatchList();
        }

        public decimal GetDependentAmount(string svcNo, string fundtype)
        {
           return  unitOfWork.cam.GetAmountPerDependent(svcNo, fundtype);
        }
        public Npf_ClaimRegister GetPerson(string svcNo, string fundtype)
        {
            return unitOfWork.cam.Getperson(svcNo, fundtype);
        }

        public decimal GetNavipAmount(string svcNo, string fundtype, out decimal amt)
        {
            return unitOfWork.cam.GetAmountPerNavip(svcNo, fundtype,out amt);
        }

        public List<Npf_ClaimRegister> GetSingleBatchno(string batchno)
        {
            return unitOfWork.cam.GetSingleBatchno(batchno);
        }

        public decimal OutstandingLoanServices(string svcNo)
        {
            return unitOfWork.cam.OutStandingLoanClaims(svcNo);
        }
    }
}

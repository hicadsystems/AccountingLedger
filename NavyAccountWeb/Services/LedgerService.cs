using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Entities;
using NavyAccountCore.Core.IRepositories;
using NavyAccountCore.Models;
using NavyAccountWeb.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.Services
{
    public class LedgerService: ILedgerService
    {

        private readonly IUnitOfWork unitofWork;

        public LedgerService(IUnitOfWork unitofWork)
        {
            this.unitofWork = unitofWork;

        }
        public decimal getSurplus_Loss(string acctcode, string fundcode)
        {
            var getRecord = unitofWork.npf_Ledgers.getSurplusValue(x => x.acctcode == acctcode && x.fundcode == fundcode);
            decimal rret = 0;
            if (getRecord != null)
            {
                rret = getRecord.opbalance + getRecord.adbbalance - getRecord.crbalance;
            }

            return rret;
        }
         public List<AcctLedgerViewModel> getLedgerInfoCSD(string fundcode)
        {
            return unitofWork.npf_Ledgers.getLedgerInfoCSD(fundcode);
        }
        public decimal getSurplus_LossC(string acctcode,string fundcode)
        {
            var getRecord =  unitofWork.npf_Ledgers.getSurplusValue(x => x.acctcode.Substring(0,1) == "4" && x.acctcode.Substring(0, 1) == "5" && x.fundcode == fundcode);
            decimal rret = 0;
            if(getRecord != null)
            {
                rret = getRecord.opbalance + getRecord.adbbalance - getRecord.crbalance;
            }

            return rret;
        }
        public decimal getsumbybank(string acctcode)
        {
            var getRecord = unitofWork.npf_Ledgers.getSurplusValue(x => x.acctcode == acctcode);
            decimal rret = 0;
            if (getRecord != null)
            {
                rret = getRecord.opbalance + getRecord.adbbalance - getRecord.crbalance;
            }

            return rret;
        }

        public List<PersonContributions> getPersonContribution(List<AcctLedgerViewModel> getAllAccountRecords, string svcno, string fundcode) {

            var getAllPfFunds = unitofWork.FundType.All();
            
            string[] fundAccount = new string[2];
            List<PersonContributions> personContributions = new List<PersonContributions>();
            if (getAllPfFunds != null) {
                decimal sum = 0;
                foreach (var fundSingle in getAllPfFunds) {
                    if (!String.IsNullOrEmpty(fundSingle.fundacct)) {
                        PersonContributions personContribution = new PersonContributions();
                         fundAccount = fundSingle.fundacct.Split('-');
                        var getfVarious = getAllAccountRecords.Where(x => x.acctcode == (fundAccount[0] + "-" + svcno)).FirstOrDefault();
                        if (getfVarious != null) {
                            personContribution.description = fundSingle.Description;
                            personContribution.amount = getfVarious.opbalance + getfVarious.adbbalance - getfVarious.crbalance;
                            personContributions.Add(personContribution);
                            sum += personContribution.amount;
                        }
                    }
                }

                if (sum != 0) {
                    personContributions.Add(
                        new PersonContributions()
                        {
                            description = "Total",
                            amount = sum
                        });
                    sum = sum * (10);
                    sum = sum / 100;
                    personContributions.Add(
                        new PersonContributions()
                        {
                            description = "10% on Contribution",
                            amount = sum
                        });
                }
            }

            return personContributions;
        }

        public DischargeCalcView dischargeCalculation(string svcno, string fundcode) {

            var getAllAccountRecords = unitofWork.npf_Ledgers.getLedgerInfo(svcno,fundcode);

            DischargeCalcView dischargeCalcView = new DischargeCalcView();
            List<PersonContributions> personContributions = new List<PersonContributions>();

            List<PersonLoan> personLoans = new List<PersonLoan>();

            personContributions = getPersonContribution(getAllAccountRecords, svcno, fundcode);
            personLoans = getPersonLoan(getAllAccountRecords, svcno, fundcode);

            dischargeCalcView.personContributions = personContributions;
            dischargeCalcView.personLoans = personLoans;

            return dischargeCalcView;
        }

        public List<PersonLoan> getPersonLoan(List<AcctLedgerViewModel> getAllAccountRecords, string svcno, string fundcode)
        {
            var getAllLoanType = unitofWork.loanType.All();
            string[] fundAccount = new string[2];
            List<PersonLoan> personLoans = new List<PersonLoan>();
            if (getAllLoanType != null)
            {

                foreach (var loanTypeSingle in getAllLoanType)
                {
                    if (!String.IsNullOrEmpty(loanTypeSingle.loanacct))
                    {
                        PersonLoan personLoan = new PersonLoan();
                        fundAccount = loanTypeSingle.loanacct.Split('-');
                        var getfVarious = getAllAccountRecords.Where(x => x.acctcode == (fundAccount[0] + "-" + svcno)).FirstOrDefault();
                        if (getfVarious != null)
                        {
                            personLoan.description = loanTypeSingle.Description;
                            personLoan.amount = getfVarious.opbalance + getfVarious.adbbalance - getfVarious.crbalance;
                            personLoans.Add(personLoan);
                        }
                    }
                }
            }

            return personLoans;
        }

    }
}

using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Entities;
using NavyAccountCore.Core.IRepositories;
using NavyAccountCore.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NavyAccountCore.Core.Repositories
{
    public class InvestmentRegisterRepository: Repository<Pf_InvestRegister>,IInvestmentRegister
    {
        private readonly INavyAccountDbContext context;

        public InvestmentRegisterRepository(INavyAccountDbContext context):base(context)
        {
            this.context = context;
        }

        public async Task<List<BankView>> GetBankList()
        {
            return await (from p in context.py_Banks
                          select new BankView
                          {
                               Id=p.Id,
                               bankname=p.bankname
                          }).ToListAsync();
        }

        public async Task<Pf_InvestRegister> GetInvesRegisterByUser(Expression<Func<Pf_InvestRegister, bool>> predicate)
        {
            return await context.pf_InvestRegisters.FirstOrDefaultAsync(predicate);
        }
        
        public List<InvestmentView> GetInvestListOST(DateTime startdate, DateTime enddate)
        {
            return (from p in context.pf_InvestRegisters
                    join r in context.npf_Charts on p.IssuanceBankId equals r.Id
                    where p.InvestmentType == "Money Market"
                        && (p.Maturingdate >= startdate && p.Maturingdate<=enddate)
                    select new InvestmentView
                    {
                        Id = p.Id,
                        Amount = (p.Amount == null) ? 0M : (decimal)p.Amount,
                        issuancebank = r.description,
                        IssuanceBankId = p.IssuanceBankId,
                        receivingBankId = p.receivingBankId,
                        Date = p.Date,
                        DueDate = p.DueDate,
                        Tenure = p.Tenure,
                        interest = p.interest,
                        Maturingdate = p.Maturingdate,
                        Voucher = p.Voucher,

                        maturedamt = (p.maturedamt == null) ? 0M : (decimal)p.maturedamt,
                        Description = p.Description,
                        InvestmentType = p.InvestmentType,
                        chequeno = p.chequeno,
                        datecreated = p.datecreated,
                        createdby = p.createdby
                    }).ToList();

        }
        public List<InvestmentView> GetInvestList()
        {
            return (from p in context.pf_InvestRegisters
                     join r in context.npf_Charts on p.IssuanceBankId equals r.Id
                    where p.InvestmentType == "Money Market"
                    select new InvestmentView
                    {
                        Id = p.Id,
                        Amount = (p.Amount == null) ? 0M : (decimal)p.Amount,
                        issuancebank = r.description,
                        IssuanceBankId = p.IssuanceBankId,
                        receivingBankId = p.receivingBankId,
                        Date = p.Date,
                        DueDate = p.DueDate,
                        Tenure = p.Tenure,
                        interest = p.interest,
                        Maturingdate = p.Maturingdate,
                        Voucher = p.Voucher,
                        TransactionType= p.TransactionType,
                        maturedamt = (p.maturedamt == null) ? 0M : (decimal)p.maturedamt,
                        Description = p.Description,
                        InvestmentType = p.InvestmentType,
                        chequeno = p.chequeno,
                        datecreated = p.datecreated,
                        createdby = p.createdby
                    }).ToList();

        }
        public List<InvestmentView> GetALLInvestListOT()
        {
            return  (from p in context.pf_InvestRegisters
                    join r in context.npf_Charts on p.IssuanceBankId equals r.Id
                     where p.Maturingdate >= DateTime.Now && p.InvestmentType=="Money Market"
                     select new InvestmentView
                    {
                        Id=p.Id,
                        //Company=q.description,
                        Amount=(p.Amount==null)?0M:(decimal)p.Amount,
                        issuancebank=r.description,
                        IssuanceBankId=p.IssuanceBankId,
                        receivingBankId=p.receivingBankId,
                        Date=p.Date,
                        DueDate=p.DueDate,
                        Tenure=p.Tenure,
                        interest=p.interest,
                        Maturingdate=p.Maturingdate,
                        Voucher=p.Voucher,
          
                        maturedamt= (p.maturedamt == null) ? 0M : (decimal)p.maturedamt,
                        Description=p.Description,
                        InvestmentType=p.InvestmentType,
                        chequeno=p.chequeno,
                        datecreated=p.datecreated,
                        createdby=p.createdby
                    }).ToList();
           
        }
        public List<InvestmentView> GetALLInvestListCapitalMk()
        {
            return (from p in context.pf_InvestRegisters
                    join q in context.npf_Stocks on p.StockId equals q.Id
                    join r in context.npf_Charts on p.IssuanceBankId equals r.Id
                    where p.InvestmentType == "Capital Market" 

                    select new InvestmentView
                    {
                        Id = p.Id,
                        StockName = q.description,
                        StockId = p.StockId,
                        TransactionType = p.TransactionType,
                        Amount = (p.Amount == null) ? 0M : (decimal)p.Amount,
                        issuancebank = r.description,
                        Company = p.company,
                        Date = p.Date,
                        DueDate = p.DueDate,
                        Tenure = p.Tenure,
                        IssuanceBankId = p.IssuanceBankId,
                        interest = p.interest,
                        Maturingdate = p.Maturingdate,
                        Voucher = p.Voucher,
                        unit = p.unit,
                        maturedamt = (p.maturedamt == null) ? 0M : (decimal)p.maturedamt,
                        Description = p.Description,
                        InvestmentType = p.InvestmentType,
                        chequeno = p.chequeno,
                        datecreated = p.datecreated,
                        createdby = p.createdby
                    }).ToList();

        }
        public List<InvestmentView> GetALLInvestListCapitalMk(DateTime startdate, DateTime enddate)
        {
            return (from p in context.pf_InvestRegisters
                    join q in context.npf_Stocks on p.StockId equals q.Id
                    join r in context.npf_Charts on p.IssuanceBankId equals r.Id
                    where p.InvestmentType == "Capital Market"
                    && p.Date>=startdate && p.Date<=enddate

                    select new InvestmentView
                    {
                        Id = p.Id,
                        StockName = q.description,
                        StockId = p.StockId,
                        TransactionType = p.TransactionType,
                        Amount = (p.Amount == null) ? 0M : (decimal)p.Amount,
                        issuancebank = r.description,
                        Company = p.company,
                        Date = p.Date,
                        DueDate = p.DueDate,
                        Tenure = p.Tenure,
                        IssuanceBankId = p.IssuanceBankId,
                        interest = p.interest,
                        Maturingdate = p.Maturingdate,
                        Voucher = p.Voucher,
                        unit = p.unit,
                        maturedamt = (p.maturedamt == null) ? 0M : (decimal)p.maturedamt,
                        Description = p.Description,
                        InvestmentType = p.InvestmentType,
                        chequeno = p.chequeno,
                        datecreated = p.datecreated,
                        createdby = p.createdby
                    }).ToList();

        }
        public List<InvestmentView> GetInvestList2()
        {
            return  (from p in context.pf_InvestRegisters
                     join q in context.npf_Stocks on p.StockId equals q.Id
                     join r in context.npf_Charts on p.IssuanceBankId equals r.Id
                          where p.InvestmentType=="Capital Market"

                     select new InvestmentView
                          {
                              Id = p.Id,
                              StockName=q.description,
                              StockId=p.StockId,
                              TransactionType=p.TransactionType,
                              Amount = (p.Amount == null) ? 0M : (decimal)p.Amount,
                              issuancebank = r.description,
                              Company = p.company,
                              Date = p.Date,
                              DueDate = p.DueDate,
                              Tenure = p.Tenure,
                              IssuanceBankId=p.IssuanceBankId,
                              interest = p.interest,
                              Maturingdate = p.Maturingdate,
                              Voucher = p.Voucher,
                              unit=p.unit,
                              maturedamt = (p.maturedamt == null) ? 0M : (decimal)p.maturedamt,
                              Description = p.Description,
                              InvestmentType = p.InvestmentType,
                              chequeno = p.chequeno,
                              datecreated = p.datecreated,
                              createdby = p.createdby
                          }).ToList();

        }

        public async Task<List<PersonView>> GetCompanyList()
        {
            return await (from p in context.company
                          select new PersonView
                          {
                              id=p.Id,
                              Name=p.description
                          }).ToListAsync();
        }
    }
}

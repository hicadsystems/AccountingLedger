using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Entities;
using NavyAccountCore.Core.Repositories;
using NavyAccountCore.Core.IRepositories;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NavyAccountCore.Models;
using System.Linq.Expressions;
using MoreLinq.Extensions;

namespace NavyAccountCore.Core.Repositories
{
    public class LoanRegisterRepository : Repository<Pf_LoanRegister>, ILoanRegisterRepository
    {

        //check eligibility
        //loan register list
        //Loan register create
        //Loan register update

        private readonly INavyAccountDbContext context;
        public LoanRegisterRepository(INavyAccountDbContext context) : base(context)
        {
            this.context = context;
        }
        public Pf_LoanRegister GetloanregisterByCode(Expression<Func<Pf_LoanRegister, bool>> predicate)
        {
            return context.pf_LoanRegisters.FirstOrDefault(predicate);
        }
        public async Task<decimal> loanAmountPerRankBy(int rankid, string loanType) {
            decimal amountformyrank = 0;
            var payperrank = await GetLoanAmountByLoanType(loanType);
            if (payperrank != null)
            {
                if (rankid == 1)
                    amountformyrank = payperrank.amount01;
                if (rankid == 2)
                    amountformyrank = payperrank.amount02;
                if (rankid == 3)
                    amountformyrank = payperrank.amount03;
                if (rankid == 4)
                    amountformyrank = payperrank.amount04;
                if (rankid == 5)
                    amountformyrank = payperrank.amount05;
                if (rankid == 6)
                    amountformyrank = payperrank.amount06;
                if (rankid == 7)
                    amountformyrank = payperrank.amount07;
                if (rankid == 8)
                    amountformyrank = payperrank.amount08;
                if (rankid == 9)
                    amountformyrank = payperrank.amount09;
                if (rankid == 10)
                    amountformyrank = payperrank.amount10;

                if (rankid == 11)
                    amountformyrank = payperrank.amount11;
                if (rankid == 12)
                    amountformyrank = payperrank.amount12;
                if (rankid == 13)
                    amountformyrank = payperrank.amount13;
                if (rankid == 14)
                    amountformyrank = payperrank.amount14;
                if (rankid == 15)
                    amountformyrank = payperrank.amount15;
                if (rankid == 16)
                    amountformyrank = payperrank.amount16;
                if (rankid == 17)
                    amountformyrank = payperrank.amount17;
                if (rankid == 18)
                    amountformyrank = payperrank.amount18;
                if (rankid == 19)
                    amountformyrank = payperrank.amount19;
                if (rankid == 20)
                    amountformyrank = payperrank.amount20;
                if (rankid == 21)
                    amountformyrank = payperrank.amount21;
                if (rankid == 22)
                    amountformyrank = payperrank.amount22;
            }
            return amountformyrank;
        }

        public async Task<decimal> loanAmountPerRankBy2(int rankid, string loanType)
        {
            decimal amountformyrank = 0;
            var payperrank = await GetLoanAmountByLoanType(loanType);
            if (rankid == 1)
                amountformyrank = payperrank.amount01;
            if (rankid == 2)
                amountformyrank = payperrank.amount02;
            if (rankid == 3)
                amountformyrank = payperrank.amount03;
            if (rankid == 4)
                amountformyrank = payperrank.amount04;
            if (rankid == 5)
                amountformyrank = payperrank.amount05;
            if (rankid == 6)
                amountformyrank = payperrank.amount06;
            if (rankid == 7)
                amountformyrank = payperrank.amount07;
            if (rankid == 8)
                amountformyrank = payperrank.amount08;
            if (rankid == 9)
                amountformyrank = payperrank.amount09;
            if (rankid == 10)
                amountformyrank = payperrank.amount10;

            if (rankid == 11)
                amountformyrank = payperrank.amount11;
            if (rankid == 12)
                amountformyrank = payperrank.amount12;
            if (rankid == 13)
                amountformyrank = payperrank.amount13;
            if (rankid == 14)
                amountformyrank = payperrank.amount14;
            if (rankid == 15)
                amountformyrank = payperrank.amount15;
            if (rankid == 16)
                amountformyrank = payperrank.amount16;
            if (rankid == 17)
                amountformyrank = payperrank.amount17;
            if (rankid == 18)
                amountformyrank = payperrank.amount18;
            if (rankid == 19)
                amountformyrank = payperrank.amount19;
            if (rankid == 20)
                amountformyrank = payperrank.amount20;
            if (rankid == 21)
                amountformyrank = payperrank.amount21;
            if (rankid == 22)
                amountformyrank = payperrank.amount22;
            return amountformyrank;
        }

        public async Task<loanPerRank> GetLoanAmountByLoanType(string loanType)
        {
            return await (from payperrank in context.loanPerRank
                          join npfloantype in context.Pf_loanType on payperrank.loantype equals npfloantype.Code
                          where (payperrank.loantype == loanType )
                          select new loanPerRank
                          {
                              amount01 = payperrank.amount01,
                              amount02 = payperrank.amount02,
                              amount03 = payperrank.amount03,
                              amount04 = payperrank.amount04,
                              amount05 = payperrank.amount05,
                              amount06 = payperrank.amount06,
                              amount07 = payperrank.amount07,
                              amount08 = payperrank.amount08,
                              amount09 = payperrank.amount09,
                              amount10 = payperrank.amount10,
                              amount11 = payperrank.amount11,
                              amount12 = payperrank.amount12,
                              amount13 = payperrank.amount13,
                              amount14 = payperrank.amount14,
                              amount15 = payperrank.amount15,
                              amount16 = payperrank.amount16,
                              amount17 = payperrank.amount17,
                              amount18 = payperrank.amount18,
                              amount19 = payperrank.amount19,
                              amount20 = payperrank.amount20,
                              amount21 = payperrank.amount21,
                              amount22 = payperrank.amount22

                          }).FirstOrDefaultAsync();
        }

        public async Task<List<LoanRegisterViewModel>> getListofLoanRegister(int fundtypeid)
        {
            return await (from loanRegister in context.pf_LoanRegisters
                          join pers in context.person on loanRegister.PersonID equals pers.PersonID
                          join npffundcode in context.pf_Funds on loanRegister.FundTypeID equals npffundcode.Id
                          join loantype in context.Pf_loanType on loanRegister.LoanTypeID equals loantype.Id
                          join statuss in context.loanStatus on loanRegister.Status equals statuss.Id.ToString()
                          join npf_chart in context.npf_Charts on loanRegister.BankID equals npf_chart.acctcode
                          where (loanRegister.FundTypeID == fundtypeid)
                          select new LoanRegisterViewModel
                          {
                              Id = loanRegister.Id,
                              PersonID = (int)loanRegister.PersonID,
                              firstName = pers.FirstName ,
                              middleName = pers.MiddleName,
                              lastName = pers.LastName,
                              Amount = (decimal)loanRegister.Amount,
                              FundTypeID = (int)loanRegister.FundTypeID,
                              LoanTypeID = (int)loanRegister.LoanTypeID,
                              ApproveDate = loanRegister.ApproveDate,
                              BankID = loanRegister.BankID,
                              BankName = npf_chart.acctcode,
                              ChequeNo = loanRegister.ChequeNo,
                              FundTypeDesc = npffundcode.Description,
                              LoanTypeDesc = loantype.Description,
                              Tenure = loanRegister.Tenure,
                              applicationNo = loanRegister.LoanAppNo,
                              remarks = loanRegister.remarks,
                              Status = statuss.Description,
                              StatusId = statuss.Id,
                              svcno = pers.SVC_NO,
                              StatusAndDate = loanRegister.StatusAndStatusDate
                          }).ToListAsync();
        }
        public IEnumerable<LoanRegisterViewModel> getListofLoanRegister()
        {
            return (from loanRegister in context.pf_LoanRegisters
                    where Convert.ToInt32(loanRegister.Status)==8
                    select new LoanRegisterViewModel
                    {
                        Amount = (decimal)loanRegister.Amount,
                        batchNo = loanRegister.batchNo
                    }).DistinctBy(x => x.batchNo).ToList();
            
         }

        public async Task<List<LoanRegisterViewModel>> getListofLoanRegisterPerPerson(int PersonID, int fundtypeid)
        {
            return await (from loanRegister in context.pf_LoanRegisters
                          join pers in context.person on loanRegister.PersonID equals pers.PersonID
                          join npffundcode in context.pf_Funds on loanRegister.FundTypeID equals npffundcode.Id
                          join loantype in context.Pf_loanType on loanRegister.LoanTypeID equals loantype.Id
                          join statuss in context.loanStatus on loanRegister.Status equals statuss.Id.ToString()
                          //join npf_chart in context.npf_Charts on loanRegister.BankID equals npf_chart.acctcode
                          where (loanRegister.FundTypeID == fundtypeid && loanRegister.PersonID == PersonID)
                          select new LoanRegisterViewModel
                          {
                              Id = loanRegister.Id,
                              PersonID = (int)loanRegister.PersonID,
                              firstName = pers.FirstName,
                              middleName = pers.MiddleName,
                              lastName = pers.LastName,
                              Amount = (decimal)loanRegister.Amount,
                              FundTypeID = (int)loanRegister.FundTypeID,
                              LoanTypeID = (int)loanRegister.LoanTypeID,
                              ApproveDate = loanRegister.ApproveDate,
                              BankID = loanRegister.BankID,
                             // BankName = npf_chart.description,
                              ChequeNo = loanRegister.ChequeNo,
                              FundTypeDesc = npffundcode.Description,
                              LoanTypeDesc = loantype.Description,
                              Tenure = loanRegister.Tenure,
                              loanAccount = loanRegister.loanAccount,
                              remarks = loanRegister.remarks,
                              Status = statuss.Description,
                              applicationNo = loanRegister.LoanAppNo,
                              StatusId = statuss.Id,
                              svcno = pers.SVC_NO,
                              StatusAndDate = loanRegister.StatusAndStatusDate
                          }).ToListAsync();
        }


        public async Task<List<LoanRegisterViewModel>> getListofLoanRegisterByStatus(int fundtypeid, string loanstatus)
        {
            return await (from loanRegister in context.pf_LoanRegisters
                          join pers in context.person on loanRegister.PersonID equals pers.PersonID
                          join npffundcode in context.npffundType on loanRegister.FundTypeID equals npffundcode.Id
                          join loantype in context.Pf_loanType on loanRegister.LoanTypeID equals loantype.Id
                          join statuss in context.loanStatus on loanRegister.Status equals statuss.Id.ToString()
                          where (loanRegister.FundTypeID == fundtypeid && loanRegister.Status == loanstatus)
                          select new LoanRegisterViewModel
                          {
                              Id = loanRegister.Id,
                              PersonID = (int)loanRegister.PersonID,
                              firstName = pers.FirstName,
                              middleName = pers.MiddleName,
                              lastName = pers.LastName,
                              Amount = (decimal)loanRegister.Amount,
                              FundTypeID = (int)loanRegister.FundTypeID,
                              LoanTypeID = (int)loanRegister.LoanTypeID,
                              ApproveDate = loanRegister.ApproveDate,
                              ChequeNo = loanRegister.ChequeNo,
                              FundTypeDesc = npffundcode.Description,
                              LoanTypeDesc = loantype.Description,
                              Tenure = loanRegister.Tenure,
                              remarks = loanRegister.remarks,
                              Status = statuss.Description,
                              applicationNo = loanRegister.LoanAppNo,
                              StatusId = statuss.Id,
                              svcno = pers.SVC_NO,
                              LoanAppNo = loanRegister.LoanAppNo,
                              StatusAndDate = loanRegister.StatusAndStatusDate
                          }).ToListAsync();
        }

        public async Task<List<LoanRegisterViewModel>> getListofLoanRegisterBTNByStatus(int fundtypeid, string loanstatus)
        {
            return await (from loanRegister in context.pf_LoanRegisters
                          join pers in context.person on loanRegister.PersonID equals pers.PersonID
                          join npffundcode in context.pf_Funds on loanRegister.FundTypeID equals npffundcode.Id
                          join loantype in context.Pf_loanType on loanRegister.LoanTypeID equals loantype.Id
                          join statuss in context.loanStatus on loanRegister.Status equals statuss.Id.ToString()
                          where (loanRegister.FundTypeID == fundtypeid && (Convert.ToInt32(loanRegister.Status) <= 2))
                          select new LoanRegisterViewModel
                          {
                              Id = loanRegister.Id,
                              PersonID = (int)loanRegister.PersonID,
                              firstName = pers.FirstName,
                              middleName = pers.MiddleName,
                              lastName = pers.LastName,
                              Amount = (decimal)loanRegister.Amount,
                              FundTypeID = (int)loanRegister.FundTypeID,
                              LoanTypeID = (int)loanRegister.LoanTypeID,
                              ApproveDate = loanRegister.ApproveDate,
                              ChequeNo = loanRegister.ChequeNo,
                              FundTypeDesc = npffundcode.Description,
                              LoanTypeDesc = loantype.Description,
                              Tenure = loanRegister.Tenure,
                              remarks = loanRegister.remarks,
                              applicationNo = loanRegister.LoanAppNo,
                              Status = statuss.Description,
                              StatusId = statuss.Id,
                              svcno = pers.SVC_NO,
                              StatusAndDate = loanRegister.StatusAndStatusDate
                          }).ToListAsync();
        }

        public async Task<List<LoanRegisterViewModel>> getListofLoanRegisterBTNByStatusP(int fundtypeid, string loanstatus)
        {
            return await (from loanRegister in context.pf_LoanRegisters
                          join pers in context.person on loanRegister.PersonID equals pers.PersonID
                          join npffundcode in context.npffundType on loanRegister.FundTypeID equals npffundcode.Id
                          join loantype in context.Pf_loanType on loanRegister.LoanTypeID equals loantype.Id
                          join statuss in context.loanStatus on loanRegister.Status equals statuss.Id.ToString()
                          where (loanRegister.FundTypeID == fundtypeid && (Convert.ToInt32(loanRegister.Status) != 8) && (Convert.ToInt32(loanRegister.Status) != 9)&&(Convert.ToInt32(loanRegister.Status) != 2))
                          select new LoanRegisterViewModel
                          {
                              Id = loanRegister.Id,
                              PersonID = (int)loanRegister.PersonID,
                              firstName = pers.FirstName,
                              middleName = pers.MiddleName,
                              lastName = pers.LastName,
                              Amount = (decimal)loanRegister.Amount,
                              FundTypeID = (int)loanRegister.FundTypeID,
                              LoanTypeID = (int)loanRegister.LoanTypeID,
                              ApproveDate = loanRegister.ApproveDate,
                        
                              applicationNo = loanRegister.LoanAppNo,
                              ChequeNo = loanRegister.ChequeNo,
                              FundTypeDesc = npffundcode.Description,
                              LoanTypeDesc = loantype.Description,
                              Tenure = loanRegister.Tenure,
                              remarks = loanRegister.remarks,
                              Status = statuss.Description,
                              StatusId = statuss.Id,
                              svcno = pers.SVC_NO,
                              StatusAndDate = loanRegister.StatusAndStatusDate
                          }).ToListAsync();
        }
        public async Task<List<LoanRegisterViewModel>> getListofLoanReport(int fundtypeid, string loanstatus, DateTime startdate, DateTime enddate)
        {
          
            return await (from loanRegister in context.pf_LoanRegisters
                          join pers in context.person on loanRegister.PersonID equals pers.PersonID
                          join npffundcode in context.pf_Funds on loanRegister.FundTypeID equals npffundcode.Id
                          join loantype in context.Pf_loanType on loanRegister.LoanTypeID equals loantype.Id
                          join statuss in context.loanStatus on loanRegister.Status equals statuss.Id.ToString()
                          where (loanRegister.FundTypeID == fundtypeid && loanRegister.Status == loanstatus &&
                               (loanRegister.ApproveDate.Date >= startdate.Date && loanRegister.ApproveDate.Date<= enddate.Date)) //&& (loanRegister.ApproveDate.Value.Year >= startdate.Year)) ||
                               //((loanRegister.ApproveDate.Value.Day <= enddate.Day) && (loanRegister.ApproveDate.Value.Month <= enddate.Month) && (loanRegister.ApproveDate.Value.Year <= enddate.Year))))
                           select new LoanRegisterViewModel
                          {
                              Id = loanRegister.Id,
                              PersonID = (int)loanRegister.PersonID,
                              firstName = pers.FirstName,
                              middleName = pers.MiddleName,
                              lastName = pers.LastName,
                              Amount = (decimal)loanRegister.Amount,
                              FundTypeID = (int)loanRegister.FundTypeID,
                              LoanTypeID = (int)loanRegister.LoanTypeID,
                              ApproveDate = loanRegister.ApproveDate,
                              ChequeNo = loanRegister.ChequeNo,
                              FundTypeDesc = npffundcode.Description,
                              LoanTypeDesc = loantype.Description,
                              Tenure = loanRegister.Tenure,
                              remarks = loanRegister.remarks,
                              Status = statuss.Description,
                              applicationNo = loanRegister.LoanAppNo,
                              StatusId = loanRegister.ApproveDate.Day,
                              svcno = pers.SVC_NO,
                              LoanAppNo = loanRegister.LoanAppNo,
                              StatusAndDate = loanRegister.StatusAndStatusDate
                          }).ToListAsync();
        }

        public async Task<List<LoanRegisterViewModel>> getListofLoanReportSummary(string batchno)
        {
   
            return await (from loanRegister in context.pf_LoanRegisters
                          join pers in context.person on loanRegister.PersonID equals pers.PersonID
                          join npffundcode in context.pf_Funds on loanRegister.FundTypeID equals npffundcode.Id
                          join loantype in context.Pf_loanType on loanRegister.LoanTypeID equals loantype.Id
                          join statuss in context.loanStatus on loanRegister.Status equals statuss.Id.ToString()
                          where (loanRegister.batchNo == batchno)                                                       
                          select new LoanRegisterViewModel
                          {
                              Id = loanRegister.Id,
                              PersonID = (int)loanRegister.PersonID,
                              firstName = pers.FirstName,
                              middleName = pers.MiddleName,
                              lastName = pers.LastName,
                              Amount = (decimal)loanRegister.Amount,
                              FundTypeID = (int)loanRegister.FundTypeID,
                              LoanTypeID = loanRegister.loancount,
                              ApproveDate = loanRegister.ApproveDate,
                              ChequeNo = loanRegister.ChequeNo,
                              FundTypeDesc = npffundcode.Description,
                              LoanTypeDesc = loantype.Description,
                              Tenure = loanRegister.Tenure,
                              remarks = loanRegister.remarks,
                              Status = statuss.Description,
                              applicationNo = loanRegister.LoanAppNo,
                              StatusId = loanRegister.ApproveDate.Day,
                              svcno = pers.SVC_NO,
                              LoanAppNo = loanRegister.LoanAppNo,
                              StatusAndDate = loanRegister.StatusAndStatusDate,
                              EffectiveDate=loanRegister.EffectiveDate
                          }).ToListAsync();
        }
       

        public async Task<List<LoanRegisterViewModel222>> getListofLoanRegisterByBatch(string batchs)
        {

            return await (from loanRegister in context.pf_LoanRegisters
                          join pers in context.person on loanRegister.PersonID equals pers.PersonID
                          join rnk in context.ranks on pers.rank equals rnk.Id
                          join npffundcode in context.pf_Funds on loanRegister.FundTypeID equals npffundcode.Id
                          join loantype in context.Pf_loanType on loanRegister.LoanTypeID equals loantype.Id
                          join statuss in context.loanStatus on loanRegister.Status equals statuss.Id.ToString()
                          where loanRegister.batchNo==batchs orderby rnk.Id descending
                          select new LoanRegisterViewModel222
                          {
                              rank=rnk.Description,
                              svcno = pers.SVC_NO,
                              Name = pers.FirstName + " " + pers.LastName,
                              LoanTypeDesc = loantype.Description,
                              Amount = (decimal)loanRegister.Amount,
                              Tenure = loanRegister.Tenure,
                              Status = statuss.Description,
                              batchNo = loanRegister.batchNo,
                              
                          }).ToListAsync();
        }
        public async Task<List<LoanRegisterViewModel22>> getListofLoanRegisterByBatch2(string batchs)
        {

            return await (from loanRegister in context.pf_LoanRegisters
                          join pers in context.person on loanRegister.PersonID equals pers.PersonID
                          join rnk in context.ranks on pers.rank equals rnk.Id
                          join npffundcode in context.pf_Funds on loanRegister.FundTypeID equals npffundcode.Id
                          join loantype in context.Pf_loanType on loanRegister.LoanTypeID equals loantype.Id
                          join statuss in context.loanStatus on loanRegister.Status equals statuss.Id.ToString()
                          where loanRegister.batchNo == batchs orderby rnk.Id descending
                          select new LoanRegisterViewModel22
                          {
                              
                              rank = rnk.Description,
                              svcno = pers.SVC_NO,
                              Name = pers.FirstName +" "+ pers.LastName, //+ pers.MiddleName,
                              LoanTypeDesc = loantype.Description,
                              Amount= (decimal)loanRegister.Amount,
                              AmountWithInterst = ((decimal)loanRegister.Amount * Convert.ToInt32(loantype.Interest))/100,
                              Tenure = loanRegister.Tenure,
                              Status = statuss.Description,
                              batchNo = loanRegister.batchNo,

                          }).ToListAsync();
        }

        public async Task<List<Pf_LoanRegister>> getLoanRegisterPending(int fundtypeid)
        {
            return await (from loanRegister in context.pf_LoanRegisters
                          //join pers in context.person on loanRegister.PersonID equals pers.PersonID
                          //join npffundcode in context.npffundType on loanRegister.FundTypeID equals npffundcode.Id
                          //join loantype in context.Pf_loanType on loanRegister.LoanTypeID equals loantype.Id
                          //join statuss in context.loanStatus on loanRegister.Status equals statuss.Id.ToString()

                          where (loanRegister.FundTypeID == fundtypeid && Convert.ToInt32(loanRegister.Status) < 8)
                          select new Pf_LoanRegister
                          {
                              Id = loanRegister.Id,
                              PersonID = (int)loanRegister.PersonID,
                              
                              Amount = (decimal)loanRegister.Amount,
                              FundTypeID = (int)loanRegister.FundTypeID,
                              LoanTypeID = (int)loanRegister.LoanTypeID,
                              ApproveDate = loanRegister.ApproveDate,
                              ChequeNo = loanRegister.ChequeNo,
                              
                              Tenure = loanRegister.Tenure,
                              remarks = loanRegister.remarks,
                             // Status = statuss.Description,
                              
                             
                          }).ToListAsync();
        }
        //public int getinterst(string lontype)
        //{
        //    var loanreview = context.npf_loantypereview.Where(x => x.LoanType == lontype);
        //    foreach(var r in loanreview)
        //    {
        //        int rdate = r.ReviewDate.Year;
        //    }
        //    int interst;
        //    return interest
        //}
        public async Task<List<LoanRegisterViewModelRecieveable>> getListofLoanRegisterRecieveable(int loantypeid,string batch,bool ckforoutst)
        {
            int interest = 0;
            var loantypeint = context.Pf_loanType.FirstOrDefault(x => x.Id == loantypeid);
            var loanr = context.pf_LoanRegisters.FirstOrDefault(x => x.batchNo == batch);
            var loanreview = context.npf_loantypereview.Where(x => x.LoanType == loantypeint.Code);
            foreach (var r in loanreview)
            {


                //int rdate = r.ReviewDate.Year;
                //int edate = loanr.EffectiveDate.Year;
                if (r.ReviewDate.Date > loanr.EffectiveDate.Date)
                {
                    continue;

                }
                else
                {
                    interest = r.Interestrate;
                }
            }
            if (ckforoutst == true)
            {
                return await (from loanRegister in context.pf_LoanRegisters
                              join pers in context.person on loanRegister.PersonID equals pers.PersonID
                              join loantype in context.Pf_loanType on loanRegister.LoanTypeID equals loantype.Id
                              join rank in context.ranks on pers.rank equals rank.Id
                              where (loanRegister.LoanTypeID == loantypeid) && loanRegister.batchNo == batch 
                                    && loanRegister.loancount < Convert.ToInt32(loanRegister.Tenure)
                              select new LoanRegisterViewModelRecieveable
                              {
                                  Name = pers.FirstName + " " + pers.LastName,
                                  Rank = rank.Description,
                                  Amount = (decimal)loanRegister.Amount,
                                  loancount = loanRegister.loancount,
                                  interstamt = getinterest(interest, (decimal)loanRegister.Amount),
                                  totalloantamt = (decimal)loanRegister.Amount + getinterest(interest, (decimal)loanRegister.Amount),
                                  monthlydeductions = ((decimal)loanRegister.Amount + getinterest(interest, (decimal)loanRegister.Amount)) / Convert.ToInt32(loanRegister.Tenure),
                                  totalloanpay = (((decimal)loanRegister.Amount + getinterest(interest, (decimal)loanRegister.Amount)) / Convert.ToInt32(loanRegister.Tenure)) * loanRegister.loancount,
                                  OutstandingPay = (decimal)loanRegister.Amount + getinterest(interest, (decimal)loanRegister.Amount) - ((((decimal)loanRegister.Amount + getinterest(interest, (decimal)loanRegister.Amount)) / Convert.ToInt32(loanRegister.Tenure)) * loanRegister.loancount),
                                  monthoutstanding = Convert.ToInt32(loanRegister.Tenure) - loanRegister.loancount,
                                  Tenure = loanRegister.Tenure,
                                  svcno = pers.SVC_NO,
                                  LoanTypeDesc = loantype.Description,
                                  rankid = rank.Id
                              }).OrderByDescending(x => x.rankid).ToListAsync();
            }

            else
            {
                return await (from loanRegister in context.pf_LoanRegisters
                              join pers in context.person on loanRegister.PersonID equals pers.PersonID
                              join loantype in context.Pf_loanType on loanRegister.LoanTypeID equals loantype.Id
                              join rank in context.ranks on pers.rank equals rank.Id
                              where (loanRegister.LoanTypeID == loantypeid) && loanRegister.batchNo == batch
                              select new LoanRegisterViewModelRecieveable
                              {
                                  Name = pers.FirstName + " " + pers.LastName,
                                  Rank = rank.Description,
                                  Amount = (decimal)loanRegister.Amount,
                                  loancount = loanRegister.loancount,
                                  interstamt = getinterest(interest, (decimal)loanRegister.Amount),
                                  totalloantamt = (decimal)loanRegister.Amount + getinterest(interest, (decimal)loanRegister.Amount),
                                  monthlydeductions = ((decimal)loanRegister.Amount + getinterest(interest, (decimal)loanRegister.Amount)) / Convert.ToInt32(loanRegister.Tenure),
                                  totalloanpay = (((decimal)loanRegister.Amount + getinterest(interest, (decimal)loanRegister.Amount)) / Convert.ToInt32(loanRegister.Tenure)) * loanRegister.loancount,
                                  OutstandingPay = (decimal)loanRegister.Amount + getinterest(interest, (decimal)loanRegister.Amount) - ((((decimal)loanRegister.Amount + getinterest(interest, (decimal)loanRegister.Amount)) / Convert.ToInt32(loanRegister.Tenure)) * loanRegister.loancount),
                                  monthoutstanding = Convert.ToInt32(loanRegister.Tenure) - loanRegister.loancount,
                                  Tenure = loanRegister.Tenure,
                                  svcno = pers.SVC_NO,
                                  LoanTypeDesc = loantype.Description,
                                  rankid = rank.Id
                              }).OrderByDescending(x => x.rankid).ToListAsync();
            }
        }
        public decimal getinterest(int interest, decimal amt)
        {
            
            decimal intamt = (amt * interest) / 100;
            return intamt;
        }


        public async Task<List<LoanRegisterViewModel>> getLoanRegisterApproved(int fundtypeid)
        {

            return await (from loanRegister in context.pf_LoanRegisters
                          where loanRegister.FundTypeID == fundtypeid && int.Parse(loanRegister.Status) == 8
                          select new LoanRegisterViewModel { Status = loanRegister.Status }).ToListAsync();
           
        }

        public async Task<List<Pf_LoanRegister>> GetLoanRegisterByApplication()
        {
          var ba=  await context.pf_LoanRegisters.Where(x => Convert.ToInt32(x.Status)<=8).ToListAsync();
            return ba.DistinctBy(x=>x.batchNo).ToList();
        }
        public async Task<List<Pf_LoanRegister>> getListofLoanRegisterByBatchDrp()
        {
            var ba = await context.pf_LoanRegisters.Where(x => x.Tenure!=x.loancount.ToString()).ToListAsync();
            return ba.DistinctBy(x => x.batchNo).ToList();
        }
        public async Task<List<Pf_LoanRegister>> getListofLoanRegisterBatchDrp()
        {
            var ba = await context.pf_LoanRegisters.ToListAsync();
            return ba.DistinctBy(x => x.batchNo).ToList();
        }
        public async Task<List<npf_loanstatus>> getListofLoanStatus()
        {
            var b=  await context.npf_loanstatus.ToListAsync();
            return b.ToList();
          
        }
        public async Task<List<npf_history>> getListofHistoryPeriod(string loanacct,string batchNo)
        {

            var b = await context.npf_Histories.Where(x=>x.acctcode==loanacct && x.batchNo==batchNo && x.remarks!= "LOAN REVERSAL").ToListAsync();
            return b.ToList();

        }
        public async Task<List<npf_history>> getListofHistoryPeriod2(string loanacct,string period)
        {

            var b = await context.npf_Histories.Where(x => x.acctcode == loanacct && x.docno==period).ToListAsync();
            return b.ToList();

        }

    }
}

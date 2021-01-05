using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Entities;
using NavyAccountCore.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NavyAccountWeb.Services
{
    public class TrialBalanceUpload
    {
        private readonly List<TrialBalanceCapture> balanceCaptures;
        private readonly IUnitOfWork unitOfWork;
        private readonly string fundCode ;
        private int fundid;
        private string user;
        private string year;
        private string month;
        public TrialBalanceUpload(List<TrialBalanceCapture> balanceCaptures, IUnitOfWork unitOfWork,string fundCode,int fundid,string _user,string month,string year)
        {
            this.balanceCaptures = balanceCaptures;
            this.unitOfWork = unitOfWork;
            this.fundCode = fundCode;
            this.fundid = fundid;
            user = _user;
            this.month = month;
            this.year = year;
        }

        public async Task<List<TrialBalanceCapture>> UploadHistoryInThread()
        {

            var res = new List<TrialBalanceCapture>();
         
            string year2 =year.Substring(2,2);
           
            foreach (var s in balanceCaptures)
            {

                if (unitOfWork.balance.checkAcctCode(s.ACCode))
                {
                    unitOfWork.history.Create(new npf_history
                    {
                        acctcode = s.ACCode,
                        fundcode = fundCode,
                        cramt = s.CREDIT,
                        dbamt = s.DEBIT,
                        netamt = (s.DEBIT - s.CREDIT),
                        docno = "Open" + fundCode + year2 + month,
                        docdate = DateTime.Now.Date,
                        period = year + month,
                        doctype = "JV",
                        batchNo=s.BatchNo,
                        remarks = "Open balance",
                        datecreated = DateTime.Now,
                        createdby = user

                    });
                }
                else
                {
                    res.Add(s);
                }
            }

            await unitOfWork.Done();

            return res;
        }

        public async Task TrialbalanceUploadInThread()
        {
            foreach(var s in balanceCaptures)
            {
                if (unitOfWork.balance.checkAcctCode(s.ACCode))
                {
                    unitOfWork.balance.Create(new npf_ledger
                    {
                        acctcode = s.ACCode,
                        fundcode = fundCode,
                        opbalance = 0M,
                        adbbalance = s.DEBIT,
                        crbalance = s.CREDIT,
                        balpl = 0M,
                        datecreated = DateTime.Now,
                        createdby = user
                    });
                }     
               
            }

            await unitOfWork.Done();
        }

      



        //public decimal getValue(decimal c,decimal d)
        //{

        //    if (c > d)
        //        return c - d;
        //    else
        //        return d - c;

        //}

        public string getDay(int day)
        {
            string result = "";
            if (day == 1)
                result = "01";
            else if (day == 2)
                result = "02";
            else if (day == 3)
                result = "03";
            else if (day == 4)
                result = "04";
            else if (day == 5)
                result = "05";
            else if (day == 6)
                result = "06";
            else if (day == 7)
                result = "07";
            else if (day == 8)
                result = "08";
            else if (day == 9)
                result = "09";
            else
                result = day.ToString();
           

            return result;


        }

        public string getMonth(int day)
        {
            string result = "";
            if (day == 1)
                result = "01";
            if (day == 2)
                result = "02";
            if (day == 3)
                result = "03";
            if (day == 4)
                result = "04";
            if (day == 5)
                result = "05";
            if (day == 6)
                result = "06";
            if (day == 7)
                result = "07";
            if (day == 8)
                result = "08";
            if (day == 9)
                result = "09";
            if (day == 10)
                result = "10";
            if (day == 11)
                result = "11";
            if (day == 12)
                result = "12";

            return result;


        }


    }
}

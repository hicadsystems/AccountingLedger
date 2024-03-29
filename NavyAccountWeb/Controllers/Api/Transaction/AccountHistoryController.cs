﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NavyAccountCore.Core.Entities;
using NavyAccountCore.Models;
using NavyAccountWeb.IServices;

namespace NavyAccountWeb.Controllers.Api.Transaction
{
    [Route("api/AccountHistory")]
    [ApiController]
    public class AccountHistoryController : ControllerBase
    {
        
       private readonly IAccountHistoryService accountHistoryService;
       public AccountHistoryController(IAccountHistoryService accountHistoryService)
       {
          this.accountHistoryService = accountHistoryService;
       }

        [Route("getAccountHistoryByRefNo/{refno}/{accountcode}/{svcno}")]
        [HttpGet]
        public async Task<IEnumerable<AccountHistoryViewModel>> getAllLoanRegisterByStatusAsync( string refno, string accountcode,string svcno)
        {
            if (svcno.Substring(0, 2) == "NN")
            {
                svcno = svcno.Substring(0, 2) + "/" + svcno.Substring(2, 4);
                refno = refno.Substring(0, 3)+"-"+ svcno;
            }
            if (svcno.Substring(1, 0) == "F" && svcno.Substring(0, 2) == "NN")
            {
                svcno = svcno.Substring(0, 2) + "/" + svcno.Substring(2, 5);
                refno = refno.Substring(0, 3) + "-" + svcno;
            }
            string acct = accountcode.Substring(0, 5) +  svcno;
            var result = await accountHistoryService.GetAccountHistory(refno, acct);
            
            return result.OrderBy(x => x.dateoftransaction);
        }

        [Route("getYearListForReport")]
        [HttpGet]
        public IEnumerable<int> getYearListForReport()
        {
            var result = accountHistoryService.getYearForReport();

            return result;
        }
    }
}
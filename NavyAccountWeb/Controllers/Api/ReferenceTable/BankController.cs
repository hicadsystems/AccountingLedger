using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NavyAccountCore.Core.Entities;
using NavyAccountWeb.IServices;

namespace NavyAccountWeb.Controllers.Api.ReferenceTable
{
    [Route("api/Bank")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private readonly IBankService bankService;
        public BankController(IBankService bankService)
        {
            this.bankService = bankService;
        }
        // GET: api/Banks/getAllBanks
        [Route("getAllBanks")]
        [HttpGet]
        public IEnumerable<py_bank> Get()
        {
            return bankService.GetBanks();
        }

       
    }
}

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NavyAccountCore.Core.Entities;
using NavyAccountCore.Models;
using NavyAccountWeb.IServices;

namespace NavyAccountWeb.Controllers.Api.ReferenceTable
{
    [Route("api/LoanStatus")]
    [ApiController]
    public class LoanStatusController : ControllerBase
    {
        private readonly ILoanStatusService lService;
        public LoanStatusController(ILoanStatusService lService)
        {
            this.lService = lService;
        }
        // GET: api/SubType
        [Route("getAllLoanStatus")]
        [HttpGet]
        public IEnumerable<LoanStatus> Get()
        {
            return lService.GetLoanStatus();
        }

        //[Route("getAllLoanStatus2/{id}")]
        //[HttpGet]
        //public List<LoanStatusVM> Get2(int id)
        //{

        //    return lService.GetLoanStatus2(id).Result;

        //}
    }
}

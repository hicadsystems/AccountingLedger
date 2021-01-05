using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NavyAccountCore.Core.Entities;
using NavyAccountWeb.IServices;

namespace NavyAccountWeb.Controllers.Api.ReferenceTable
{
    [Route("api/Rank")]
    [ApiController]
    public class RankController : ControllerBase
    {
        private readonly IRankService rankService;
        public RankController(IRankService rankService)
        {
            this.rankService = rankService;
        }
        // GET: api/SubType
        [Route("getAllRanks")]
        [HttpGet]
        public IEnumerable<Rank> Get()
        {
            return rankService.GetRanks();
        }
        [Route("getAllRanks2")]
        [HttpGet]
        public IEnumerable<Rank> Get2()
        {
            return rankService.GetRanks2();
        }


    }
}

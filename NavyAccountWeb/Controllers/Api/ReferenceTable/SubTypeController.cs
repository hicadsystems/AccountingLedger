using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NavyAccountCore.Core.Entities;
using NavyAccountWeb.IServices;

namespace NavyAccountWeb.Controllers.Api.ReferenceTable
{
    [Route("api/SubType")]
    [ApiController]
    public class SubTypeController : ControllerBase
    {
        private readonly ISubTypeService subTypeService;
        public SubTypeController(ISubTypeService subTypeService)
        {
            this.subTypeService = subTypeService;
        }
        // GET: api/SubType
        [Route("getAllSubTypes")]
        [HttpGet]
        public IEnumerable<sub_type> Get()
        {
            return subTypeService.GetSubTypes();
        }

       
    }
}

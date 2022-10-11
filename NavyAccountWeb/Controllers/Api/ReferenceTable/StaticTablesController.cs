using Microsoft.AspNetCore.Mvc;
using NavyAccountCore.Core.Data;
using NavyAccountCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NavyAccountWeb.Controllers.Api.ReferenceTable
{
    [Route("api/statictable")]
    [ApiController]
    public class StaticTablesController : ControllerBase
    {
        private readonly INavyAccountDbContext _context;
        public StaticTablesController(INavyAccountDbContext context)
        {
            this._context = context;
        }
        // GET: api/<StaticTablesController>
        [Route("getallstate")]
        [HttpGet]
        public IEnumerable<sr_state> GetAllState()
        {
            return _context.sr_state.ToList();
        }

        // GET api/<StaticTablesController>/5
        [Route("getalllga/{statecode}")]
        [HttpGet]
        public IEnumerable<sr_lga> Getlga(string statecode)
        {
            return _context.sr_lga.Where(s=>s.statecode== statecode).ToList();
        }
        [Route("getallclass")]
        [HttpGet]
        public IEnumerable<sr_ClassRecord> GetClass(string statecode)
        {
            return _context.sr_ClassRecord.ToList();
        }
        // POST api/<StaticTablesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<StaticTablesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<StaticTablesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

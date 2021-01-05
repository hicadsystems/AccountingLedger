using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NavyAccountCore.Core.Entities;
using NavyAccountCore.Models;
using NavyAccountWeb.IServices;

namespace NavyAccountWeb.Controllers.Api.Transaction
{
    [Route("api/Navip")]
    [ApiController]
    public class NavipController : Controller
    {
        private readonly IPersonService personService;
        private readonly INavipService navipservice;
        private readonly string _connectionstring;
        public NavipController(IPersonService personService, INavipService navipservice, IConfiguration configuration)
        {
            this.personService = personService;
            this.navipservice = navipservice;
            _connectionstring = configuration.GetConnectionString("DefaultConnection");
        }
        [Route("getPersonByID/{id}")]
        [HttpGet]
        public IActionResult getByPersonID(int id)
        {
            var getnavip = navipservice.checksvc(id).Result;
            if (getnavip != null)
            {
                var personlist = personService.getPersonById(id).Result;
                NavipVM3 pn = new NavipVM3();
                pn.SVC_NO = personlist.SVC_NO;
                pn.LastName = personlist.LastName + " " + personlist.FirstName;
                pn.Title = personlist.rank;
                pn.dateemployed = personlist.dateemployed;
                int rankid = personlist.rankid;
                pn.bank = getnavip.bank;
                pn.acctno = getnavip.acctno;
                pn.beneficiary = getnavip.beneficiary;
                pn.prmdate01 = getnavip.prmdate01; pn.prmdate02 = getnavip.prmdate02;
                pn.prmdate03 = getnavip.prmdate03; pn.prmdate04 = getnavip.prmdate04;
                pn.prmdate05 = getnavip.prmdate05; pn.prmdate06 = getnavip.prmdate06;
                pn.prmdate07 = getnavip.prmdate07; pn.prmdate08 = getnavip.prmdate08;
                pn.prmdate09 = getnavip.prmdate09; pn.prmdate10 = getnavip.prmdate10;
                pn.prmdate11 = getnavip.prmdate11; pn.prmdate12 = getnavip.prmdate12;
                pn.prmdate13 = getnavip.prmdate13; pn.prmdate14 = getnavip.prmdate14; 
                pn.prmdate15 = getnavip.prmdate15; pn.prmdate16 = getnavip.prmdate16; 
                pn.prmdate17 = getnavip.prmdate17; pn.title2 = getnavip.title;




                  return Ok(new { responseCode = 200, rankid, pn });
               // return Ok();
            }
            else
            {
                var personlist = personService.getPersonById(id).Result;
                Person pn = new Person();
                pn.SVC_NO = personlist.SVC_NO;
                pn.LastName = personlist.LastName + " " + personlist.FirstName;
                pn.Title = personlist.rank;
                pn.dateemployed = personlist.dateemployed;
                int rankid = personlist.rankid;

                return Ok(new { responseCode = 200, rankid, pn });
            }
        }
        [Route("createNavip")]
        [HttpPost]
        public IActionResult createNavip([FromBody] npf_NavipContribution value)
        {
            try
            {
                if (navipservice.checksvc(value.PersonID).Result != null)
                {
                    navipservice.updateNavip(value);
                    //var getnavip2 = navipservice.checksvc(value.PersonID).Result;

                    using (SqlConnection sqls = new SqlConnection(_connectionstring))
                    {
                        using (SqlCommand cmd = new SqlCommand("npf_calculate_navip", sqls))
                        {
                            cmd.CommandTimeout = 1200;
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@globaluser", User.Identity.Name));
                            cmd.Parameters.Add(new SqlParameter("@perid", value.PersonID));


                            sqls.Open();
                            cmd.ExecuteNonQuery();

                            return Ok(new { responseCode = 200, responseDescription = "Calculation Successfully" });
                        }

                    }
                    //return Ok(new { responseCode = 400, responseDescription = "Navip Claim Has Been Added" });
                }
                else
                {
                    navipservice.AddNavip(value);
                }
                var getnavip = navipservice.checksvc(value.PersonID).Result;
                if (getnavip != null)
                {
                
                    using (SqlConnection sqls = new SqlConnection(_connectionstring))
                    {
                        using (SqlCommand cmd = new SqlCommand("npf_calculate_navip", sqls))
                        {
                            cmd.CommandTimeout = 1200;
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@globaluser", User.Identity.Name));
                            cmd.Parameters.Add(new SqlParameter("@perid", getnavip.PersonID));


                            sqls.Open();
                            cmd.ExecuteNonQuery();

                            return Ok(new { responseCode = 200, responseDescription = "Calculation Successfully" });
                        }

                    }
                }
                else
                {
                    return Ok(new { responseCode = 400, responseDescription = "Navip Claim Has Not Been Added" });
                }   

            }
            catch (Exception ex)
            {
                return Ok(new { responseCode = 500, responseDescription = "Failed" });
            }
        }
    }
}
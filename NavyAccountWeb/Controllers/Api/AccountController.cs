using System;
using System.Collections.Generic;
using NavyAccountWeb.IServices;
using Microsoft.AspNetCore.Mvc;
using NavyAccountWeb.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NavyAccountWeb.Controllers.Api
{
    [Route("api/[controller]")]
    public class AccountController : BaseController
    {
        private readonly IAuthenticationService authenticationService;
        private readonly IUserService userService;

        public AccountController(IAuthenticationService authenticationService, IUserService userService) : base(userService)
        {
            this.userService = userService;
            this.authenticationService = authenticationService;
        }
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public  IActionResult Post([FromBody]LoginViewModel login)
        {
            var auth =  authenticationService.SignInUserAsync(login.EmailAddress, login.Password, "false");

            if (!auth.Result.Success)
            {
               // TempData["errorMessage"] = auth.Result.Message;
                return Ok(new { error = "error"});
            }
            return Ok(new { error = "Success" });
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

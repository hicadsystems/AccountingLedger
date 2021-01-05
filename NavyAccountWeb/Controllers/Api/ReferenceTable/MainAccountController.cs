using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NavyAccountCore.Core.Entities;
using NavyAccountCore.Models;
using NavyAccountWeb.IServices;

namespace NavyAccountWeb.Controllers.Api.ReferenceTable
{
    [Route("api/MainAccount")]
    [ApiController]
    public class MainAccountController : ControllerBase
    {
        private readonly IMainAccountService mainAccountService;
        public MainAccountController(IMainAccountService mainAccountService)
        {
            this.mainAccountService = mainAccountService;
        }
        // GET: api/MainAccount
        [Route("")]
        [HttpGet]
        public IActionResult getMainAcount(int? pageno)
        {
            int mainCount = 0;
            int iDisplayLength = 10;
            if (pageno == 0) pageno = 0;
            else pageno = pageno - 1;
            //pageno = (pageno == 0) ? 0 : pageno--;
            
            var mainact= mainAccountService.GetMainPerPage(((int)pageno * iDisplayLength), iDisplayLength,out mainCount);

            return Ok(new { responseCode = 200, mainactlist = mainact, total = mainCount });
        }

        [Route("getAllMainAccountsDesc")]
        [HttpGet]
        public IActionResult getMainAcountDesc()
        {
            var mainActs = mainAccountService.GetMainAccountDesc().Result;
            if (mainActs == null)
            {
                return Ok(new { responseCode = 200, responseDescription = "", Data = 0 });
            }
            return Ok(new { responseCode = 200, responseDescription = "Main Account Code Exist", Data = mainActs });
             
        }

        [Route("getLastMainAccount/{id}")]
        [HttpGet]
        public IActionResult getLastMainAcountByCode(string id)
        {

            var mainAct = mainAccountService.GetLastMainAccountByCode(id).Result;
            if (mainAct == null)
            {
                return Ok(new { responseCode = 200, responseDescription = "", Data = 0 });
            }
            return Ok(new { responseCode = 200, responseDescription = "Main Account Code Exist", Data = mainAct });
        }

        // GET: api/MainAccount/5
        [Route("getMainAccountById")]
        [HttpGet("{id}")]
        public string getMainAccountById(int id)
        {
            return "value";
        }

        // api/MainAccount/RemoveMainAct
        [Route("RemoveMainAct/{id:int}")]
        [HttpGet]
        public IActionResult Remove(int id)
        {
            var res =  mainAccountService.GetMainAccountById(id);
            if (res == null) return NotFound();

            mainAccountService.RemoveMainAct(res.Result);
            return Ok(new { responseCode = 200, responseDescription = "Deleted Successful" });
        }

        // POST: api/MainAccount
        [Route("createMainAccount")]
        [HttpPost]
        public IActionResult addNewMainAccount([FromBody] npf_mainact value)
        {
            try
            {
                if (mainAccountService.GetMainAccountByCode(value.maincode.Trim()).Result != null)
                {
                    return Ok(new { responseCode = 400, responseDescription = "Balance Sheet Code already Exist" });
                }
                value.datecreated = DateTime.Now;
                value.createdby = User.Identity.Name;
                mainAccountService.AddMainAccount(value);

                return Ok(new { responseCode = 200, responseDescription = "Created Successfully" });
            }
            catch (Exception ex) {
                return Ok(new { responseCode = 500, responseDescription = "Failed" });
            }
        }
        

        // PUT: api/MainAccount/5
        [Route("updateMainAccount")]
        [HttpPut]
        public IActionResult updateMainAccount( [FromBody] npf_mainact value)
        {
            try{
                if (String.IsNullOrEmpty(value.maincode))
                {
                    return Ok(new { responseCode = 500, responseDescription = "Kindly Supply Main Account Code" });
                }
                var getbal = mainAccountService.GetMainAccountByCode(value.maincode.Trim()).Result;
                        getbal.description = value.description;
                        getbal.subtype = value.subtype;
                        getbal.shortname = value.shortname;
                        getbal.npf_balsheet_bl_code = value.npf_balsheet_bl_code;
                        getbal.datecreated= value.datecreated = DateTime.Now;
                        getbal.createdby= value.createdby = User.Identity.Name;
                mainAccountService.UpdateMainAccount(getbal);
                    return Ok(new { responseCode = 200, responseDescription = "Updated Successfully" });
            }
            catch(Exception ex){
                return Ok(new { responseCode = 500, responseDescription = "Failed" });
            }
        }

    }
}

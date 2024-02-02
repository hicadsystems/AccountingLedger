using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NavyAccountCore.Core.Entities;
using NavyAccountCore.Models;
using NavyAccountWeb.IServices;

namespace NavyAccountWeb.Controllers.Api.ReferenceTable
{
    [Route("api/PfInvest")]
    [ApiController]
    public class InvestRegisterController : ControllerBase
    {

        private readonly IInvestmentRegisterServices services;

        public InvestRegisterController(IInvestmentRegisterServices services)
        {
            this.services = services;
        }

        // api/PfInvest/getAllRegister
        [Route("getAllRegister")]
        [HttpGet]
        public List<InvestmentView> getAllRegister()
        {
            return services.GetAllInvestRegister().ToList();
        }

        [Route("getAllRegister2")]
        [HttpGet]
        public List<InvestmentView> getAllRegister2()
        {
            return services.GetAllInvestRegister2().ToList();
        }


        ////api/PfInvest/GetAllBank
        //[Route("GetAllBank")]
        //[HttpGet]
        //public IEnumerable<BankView> GetAllBank()
        //{
        //    return services.GetBanKRecord().Result;
        //}

        //api/PfInvest/GetAllUser
        [Route("LoadAllPersonnel")]
        [HttpGet]
        public IEnumerable<PersonView> LoadAllPersonnel()
        {
            return services.GetAllCompany().Result;
        }


       




        // api/PfInvest/createInvestRegister
        [Route("createInvestRegister")]
        [HttpPost]
        public IActionResult createInvestRegister([FromBody] Pf_InvestRegister value)
        {
            try
            {
                if (value.IssuanceBankId== null)
                {
                    return Ok(new { responseCode = 400, responseDescription = "IssuanceBank can not be Null" });
                }
                value.InvestmentType = "Money Market";
                services.AddInvestRegister(value);

                return Ok(new { responseCode = 200, responseDescription = "Created Successfully" });
            }
            catch (Exception ex)
            {
                return Ok(new { responseCode = 500, responseDescription = "Failed" });
            }
        }

        // api/PfInvest/updateInvestRegister
        [Route("updateInvestRegister")]
        [HttpPut]
        public IActionResult updateInvestRegister([FromBody] Pf_InvestRegister value)
        {
            try
            {
                if (String.IsNullOrEmpty(value.IssuanceBankId.ToString()))
                {
                    return Ok(new { responseCode = 500, responseDescription = "Kindly enter the IssuanceBank" });
                }

                var getbal = services.GetinvestRegisterById(value.Id).Result;
                getbal.IssuanceBankId = value.IssuanceBankId;
                getbal.Date = value.Date;
                getbal.DueDate = value.DueDate;
                getbal.Amount = value.Amount;
                getbal.closecode = value.closecode;
                getbal.createdby = value.createdby;
                getbal.datecreated = value.datecreated;
                getbal.Description = value.Description;
                getbal.InvestmentType = value.InvestmentType;
                getbal.maturedamt = value.maturedamt;
                getbal.Voucher = value.Voucher;
                getbal.Tenure = value.Tenure;
                getbal.Maturingdate = value.Maturingdate;
                getbal.interest = value.interest;
                getbal.chequeno = value.chequeno;
                getbal.receivingBankId = value.receivingBankId;

                services.UpdateInvestRegister(getbal);
                return Ok(new { responseCode = 200, responseDescription = "Updated Successfully" });
            }
            catch (Exception ex)
            {
                return Ok(new { responseCode = 500, responseDescription = "Failed" });
            }
        }
        [Route("getAllCapital")]
        [HttpGet]
        public List<InvestmentView> getAllcapital()
        {
            return services.GetAllInvestRegister2().ToList();
        }
        [Route("getAllCapital/{startDate}/{endDate}")]
        [HttpGet]
        public List<InvestmentView> getAllcapital2(string startDate,string endDate)
        {
            return services.GetAllInvestRegister2()
                .Where(x=>x.Date==Convert.ToDateTime(startDate) && x.Date == Convert.ToDateTime(endDate))
                .ToList();
        }
        [Route("createInvestCapitalMarket")]
        [HttpPost]
        public IActionResult createInvestCapitalMarket([FromBody] Pf_InvestRegister value)
        {
            try
            {
                if (value.company== null)
                {
                    return Ok(new { responseCode = 400, responseDescription = "company already Exist" });
                }
                value.InvestmentType = "Capital Market";
                services.AddInvestRegister(value);

                return Ok(new { responseCode = 200, responseDescription = "Created Successfully" });
            }
            catch (Exception ex)
            {
                return Ok(new { responseCode = 500, responseDescription = "Failed" });
            }
        }

        // api/PfInvest/updateInvestRegister
        [Route("updateInvestCapitalMarket")]
        [HttpPut]
        public IActionResult updateInvestCapitalMarket([FromBody] Pf_InvestRegister value)
        {
            try
            {
                if (String.IsNullOrEmpty(value.company.ToString()))
                {
                    return Ok(new { responseCode = 500, responseDescription = "Kindly enter the company" });
                }

                var getbal = services.GetinvestRegisterById(value.Id).Result;
                getbal.company = value.company;
                getbal.IssuanceBankId = value.IssuanceBankId;
                getbal.Date = value.Date;
                getbal.Amount = value.Amount;
                getbal.closecode = value.closecode;
                getbal.createdby = value.createdby;
                getbal.datecreated = value.datecreated;
                getbal.Description = value.Description;
                getbal.InvestmentType = value.InvestmentType;
                getbal.Voucher = value.Voucher;
                getbal.chequeno = value.chequeno;
                getbal.unit = value.unit;
                getbal.StockId=value.StockId;
                getbal.TransactionType= value.TransactionType;

                services.UpdateInvestRegister(getbal);
                return Ok(new { responseCode = 200, responseDescription = "Updated Successfully" });
            }
            catch (Exception ex)
            {
                return Ok(new { responseCode = 500, responseDescription = "Failed" });
            }
        }
    }
}
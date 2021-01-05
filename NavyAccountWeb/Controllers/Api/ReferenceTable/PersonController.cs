using System;
using System.Collections.Generic;
using DocumentFormat.OpenXml.EMMA;
using Microsoft.AspNetCore.Mvc;
using NavyAccountCore.Core.Entities;
using NavyAccountCore.Models;
using NavyAccountWeb.IServices;

namespace NavyAccountWeb.Controllers.Api.ReferenceTable
{
    [Route("api/PersonAPI")]
    [ApiController]
    public class PersonAPIController : ControllerBase
    {
        private readonly IPersonService personService;
        private readonly IBeneficiaryService beneficiaryService;
        public PersonAPIController(IPersonService personService, IBeneficiaryService beneficiaryService)
        {
            this.personService = personService;
            this.beneficiaryService = beneficiaryService;
        }
        // GET: api/Person
        [Route("getAllPersons")]
        [HttpGet]
        public async System.Threading.Tasks.Task<IActionResult> Get(int? pageno)
        {
            int iDisplayLength = 10;
            pageno = pageno == null ? 0 : (pageno--);
            var _personlist = await personService.getPersonList(((int)pageno * iDisplayLength), iDisplayLength);
            var countall = await personService.getPersonListCount();
            return Ok(new { responseCode = 200, personlist = _personlist, total = countall });
        }
        [Route("getAllPersonsold")]
        [HttpGet]
        public async System.Threading.Tasks.Task<IActionResult> Getold(int? pageno)
        {
            int iDisplayLength = 10;
            pageno = pageno == null ? 0 : (pageno--);
            var _personlist = await personService.getPersonListold(((int)pageno * iDisplayLength), iDisplayLength);
            var countall = await personService.getPersonListCount();
            return Ok(new { responseCode = 200, personlist = _personlist, total = countall });
        }

        [Route("getAllInactivePersons")]
        [HttpGet]
        public List<PersonListViewModel> getAllInactivePersons()
        {
            return personService.getInactivePersonList().Result;
        }

        [Route("getAllPersonsByServiceNo/{serviceno}")]
        [HttpGet]
        public List<PersonListViewModel> getByServiceNo(string serviceno)
        {
            return personService.getPersonListBySvc_No(serviceno).Result;
        }

        [Route("getPersonByID/{id}")]
        [HttpGet]
        public PersonListViewModel getByPersonID(int id)
        {
           return personService.getPersonById(id).Result;
        }
        [Route("getPersonByID2/{id}")]
        [HttpGet]
        public IActionResult getByPersonID2(int id)
        {
          var per=  personService.getPersonById(id).Result;
            PersonListViewModel pn = new PersonListViewModel();
            pn.PersonID = per.PersonID;
            pn.SVC_NO = per.SVC_NO;
            pn.LastName = per.LastName;
            pn.FirstName = per.FirstName;
            pn.MiddleName = per.MiddleName;
            pn.rank = per.rank;
            pn.accountno = per.accountno;
            pn.Sex = per.Sex;
            

            return Ok(new { responseCode = 200, pn });
        }

        [Route("getAllPersonsByServiceNoLimited/{serviceno}")]
        [HttpGet]
        public List<PersonListID_Name> getByServiceNoLimited(string serviceno)
      {
            //string serviceno1 = serviceno.Substring(0, 2) + "/";
            //string serviceno2= serviceno.PadRight(4);
            //string serviceno3 = serviceno.Substring(0, 2);
            List<PersonListID_Name> pp = new List<PersonListID_Name>();
            var result = personService.getPersonListBySvc_No(serviceno).Result;
            foreach (var v in result)
            {
                pp.Add(new PersonListID_Name()
                {
                    Id = v.PersonID,
                    name = v.FirstName + " " + v.LastName + " " + v.MiddleName + "_" + v.SVC_NO
                }) ;
            }
            return pp;
        }


        // POST: api/Person
        [Route("createPerson")]
        [HttpPost]
        public IActionResult createPerson2([FromBody] PersonViewModel value)
        {
            try
            {
                if (String.IsNullOrEmpty(value.person.SVC_NO))
                {
                    return Ok(new { responseCode = 500, responseDescription = "Kindly Supply Service Number" });
                }
                if (personService.GetPersonBySvc_NO(value.person.SVC_NO.Trim()) != null)
                {
                    return Ok(new { responseCode = 400, responseDescription = "Service Number already Exist" });
                }
                value.person.CreatedDate = DateTime.Now;

                _ = personService.AddPerson(value.person).Result;

                //var getPostedPersonel = personService.GetPersonBySvc_NO(value.person.SVC_NO.Trim());
                //if (getPostedPersonel != null) { 
                
                //    if(!String.IsNullOrEmpty(value.bene.FirstName))
                //    {
                //        value.bene.PersonID = getPostedPersonel.PersonID;
                //        value.bene.IsActive = true;
                //        value.bene.CreatedBy = User.Identity.Name;
                //        value.bene.CreatedDate = System.DateTime.Now;
                //        value.bene.HomeNumber = null;
                //        value.bene.ModifiedBy = User.Identity.Name;
                //        value.bene.ModifiedDate = System.DateTime.Now;
                //        value.bene.NextofkinType = null;
                //        value.bene.PlaceOfWork = null;

                //        beneficiaryService.AddBeneficiary(value.bene);
                //    }

                //    if (!String.IsNullOrEmpty(value.bene2.FirstName))
                //    {
                //        value.bene2.PersonID = getPostedPersonel.PersonID;

                //        value.bene2.IsActive = true;
                //        value.bene2.CreatedBy = User.Identity.Name;
                //        value.bene2.CreatedDate = System.DateTime.Now;
                //        value.bene2.HomeNumber = null;
                //        value.bene2.ModifiedBy = User.Identity.Name;
                //        value.bene2.ModifiedDate = System.DateTime.Now;
                //        value.bene2.PlaceOfWork = null;
                //        value.bene2.NextofkinType = null;
                //        beneficiaryService.AddBeneficiary(value.bene2);
                //    }

                //    if (!String.IsNullOrEmpty(value.bene3.FirstName))
                //    {
                //        value.bene3.PersonID = getPostedPersonel.PersonID;
                //        value.bene3.IsActive = true;
                //        value.bene3.CreatedBy = User.Identity.Name;
                //        value.bene3.CreatedDate = System.DateTime.Now;
                //        value.bene3.HomeNumber = null;
                //        value.bene3.ModifiedBy = User.Identity.Name;
                //        value.bene3.ModifiedDate = System.DateTime.Now;
                //        value.bene3.PlaceOfWork = null;
                //        value.bene3.NextofkinType = null;
                //        beneficiaryService.AddBeneficiary(value.bene3);
                //    }
                //}

                return Ok(new { responseCode = 200, responseDescription = "Created Successfully" });
            }
            catch (Exception ex) {
                return Ok(new { responseCode = 500, responseDescription = "Failed" });
            }
        }


        // POST: api/Person
        [Route("updatePerson")]
        [HttpPost]
        public IActionResult updatePerson([FromBody] PersonViewModel value)
        {
            try
            {
                if (String.IsNullOrEmpty(value.person.SVC_NO))
                {
                    return Ok(new { responseCode = 500, responseDescription = "Kindly Supply Service Number" });
                }
                var gett = personService.GetPersonBySvc_NO(value.person.SVC_NO.Trim());
                if (gett == null)
                {
                    return Ok(new { responseCode = 400, responseDescription = "Service Number Not Available for Update" });
                }
                value.person.CreatedDate = DateTime.Now;
                gett.dateleft = value.person.dateleft;
                gett.FirstName = value.person.FirstName;
                gett.LastName = value.person.LastName;
                gett.MiddleName = value.person.MiddleName;
                gett.bank = value.person.bank;
                gett.dateemployed = value.person.dateemployed;
                gett.email = value.person.email;
                gett.Sex = value.person.Sex;
                gett.GSMNumber = value.person.GSMNumber;
                gett.homeaddress = value.person.homeaddress;
                
                _ = personService.UpdatePerson(gett).Result;

               

                //    if (!String.IsNullOrEmpty(value.bene.FirstName))
                //    {
                //        value.bene.PersonID = value.person.PersonID;
                //        value.bene.IsActive = true;
                //        //value.bene.CreatedBy = User.Identity.Name;
                //        //value.bene.CreatedDate = System.DateTime.Now;
                //        value.bene.HomeNumber = null;
                //        value.bene.ModifiedBy = User.Identity.Name;
                //        value.bene.ModifiedDate = System.DateTime.Now;
                //        value.bene.NextofkinType = null;
                //        value.bene.PlaceOfWork = null;

                //    if (value.bene.Id != 0)
                //    {
                //        var gettbene = beneficiaryService.GetBeneficiaryByID(value.bene.Id).Result;
                //        gettbene.FirstName = value.bene.FirstName;
                //        gettbene.LastName = value.bene.LastName;
                //        gettbene.FullAddress = value.bene.FullAddress;
                //        gettbene.EmailAddress = value.bene.EmailAddress;
                //        gettbene.MobileNumber = value.bene.MobileNumber;
                //        gettbene.PlaceOfWork = value.bene.PlaceOfWork;
                //        gettbene.RelationshipId = value.bene.RelationshipId;
                //        gettbene.Proportion = value.bene.Proportion;
                //        beneficiaryService.UpdateBeneficiary(gettbene);
                //    }
                //    else
                //    {
                //        beneficiaryService.AddBeneficiary(value.bene);
                //    }
                //}

                //    if (!String.IsNullOrEmpty(value.bene2.FirstName))
                //    {
                //        value.bene2.PersonID = value.person.PersonID;
                        
                //        value.bene2.IsActive = true;
                //        //value.bene2.CreatedBy = User.Identity.Name;
                //        //value.bene2.CreatedDate = System.DateTime.Now;
                //        value.bene2.HomeNumber = null;
                //        value.bene2.ModifiedBy = User.Identity.Name;
                //        value.bene2.ModifiedDate = System.DateTime.Now;
                //        value.bene2.PlaceOfWork = null;
                //        value.bene2.NextofkinType = null;
                //    if (value.bene2.Id != 0)
                //    {
                //        var gettbene2 = beneficiaryService.GetBeneficiaryByID(value.bene2.Id).Result;
                //        gettbene2.FirstName = value.bene2.FirstName;
                //        gettbene2.LastName = value.bene2.LastName;
                //        gettbene2.FullAddress = value.bene2.FullAddress;
                //        gettbene2.EmailAddress = value.bene2.EmailAddress;
                //        gettbene2.MobileNumber = value.bene2.MobileNumber;
                //        gettbene2.PlaceOfWork = value.bene2.PlaceOfWork;
                //        gettbene2.RelationshipId = value.bene2.RelationshipId;
                //        gettbene2.Proportion = value.bene2.Proportion;
                //        beneficiaryService.UpdateBeneficiary(gettbene2);
                //    }
                //    else
                //    {
                //        beneficiaryService.AddBeneficiary(value.bene2);
                //    }
                //}

                //    if (!String.IsNullOrEmpty(value.bene3.FirstName))
                //    {
                //        value.bene3.PersonID = value.person.PersonID;
                //        value.bene3.IsActive = true;
                //        //value.bene3.CreatedBy = User.Identity.Name;
                //        //value.bene3.CreatedDate = System.DateTime.Now;
                //        value.bene3.HomeNumber = null;
                //        value.bene3.ModifiedBy = User.Identity.Name;
                //        value.bene3.ModifiedDate = System.DateTime.Now;
                //        value.bene3.PlaceOfWork = null;
                //        value.bene3.NextofkinType = null;
                //    if (value.bene3.Id != 0)
                //    {
                //        var gettbene3 = beneficiaryService.GetBeneficiaryByID(value.bene3.Id).Result;
                //        gettbene3.FirstName = value.bene3.FirstName;
                //        gettbene3.LastName = value.bene3.LastName;
                //        gettbene3.FullAddress = value.bene3.FullAddress;
                //        gettbene3.EmailAddress = value.bene3.EmailAddress;
                //        gettbene3.MobileNumber = value.bene3.MobileNumber;
                //        gettbene3.PlaceOfWork = value.bene3.PlaceOfWork;
                //        gettbene3.RelationshipId = value.bene3.RelationshipId;
                //        gettbene3.Proportion = value.bene3.Proportion;
                //        beneficiaryService.UpdateBeneficiary(value.bene3);
                //    }
                //    else
                //    {
                //        beneficiaryService.AddBeneficiary(value.bene3);
                //    }
                //}
                

                return Ok(new { responseCode = 200, responseDescription = "Created Successfully" });
            }
            catch (Exception ex)
            {
                return Ok(new { responseCode = 500, responseDescription = "Failed" });
            }
        }



    }
}

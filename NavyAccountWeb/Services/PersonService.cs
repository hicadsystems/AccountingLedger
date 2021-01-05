using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Entities;
using NavyAccountCore.Models;
using NavyAccountWeb.IServices;

namespace NavyAccountWeb.Services
{
    public class PersonService : IPersonService
    {
        private readonly IUnitOfWork unitOfWork;
        public PersonService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;

        }
        public async Task<bool> AddPerson(Person person)
        {
            unitOfWork.person.Create(person);
            return await unitOfWork.Done();
        }

        public async Task<bool> UpdatePerson(Person person)
        {
            unitOfWork.person.Update(person);
            return await unitOfWork.Done();
        }

        public Person GetPersonBySvc_NO(string person)
        {
            return unitOfWork.person.GetPersonBySVC_No(x=>x.SVC_NO == person);
        }

        public async Task<PersonListViewModel> getSinglePerson(string person)
        {
            return await unitOfWork.person.getSinglePerson(person);
        }

        public async Task<PersonListViewModel> getPersonById(int personid)
        {
            return await unitOfWork.person.getPersonById(personid);
        }
        public async Task<List<PersonListViewModel>> getPersonList(int iDisplayStart, int iDisplayLength)
        {
            return await unitOfWork.person.getPersonList( iDisplayStart, iDisplayLength);
        }
        public async Task<List<PersonListViewModel>> getPersonListold(int iDisplayStart, int iDisplayLength)
        {
            return await unitOfWork.person.getPersonListold(iDisplayStart, iDisplayLength);
        }

        public async Task<int> getPersonListCount()
        {
            return await unitOfWork.person.getPersonListCount();
        }

        public async Task<List<PersonListViewModel>> getInactivePersonList()
        {
            return await unitOfWork.person.getPersonInactiveList();
        }

        public async Task<List<PersonListViewModel>> getPersonListBySvc_No(string svc_no)
        {
            return await unitOfWork.person.getPersonListBySvc_No(svc_no);
        }
        public IEnumerable<Person> GetPersons()
        {
            return unitOfWork.person.All();
        }

    }
}

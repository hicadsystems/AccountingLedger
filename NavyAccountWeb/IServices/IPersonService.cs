using System;
using System.Collections.Generic;
using System.Linq;
using NavyAccountCore.Core.Entities;
using System.Threading.Tasks;
using NavyAccountCore.Models;

namespace NavyAccountWeb.IServices
{
    public interface IPersonService
    {
        IEnumerable<Person> GetPersons();
        Task<bool> AddPerson(Person bl_sheet);
        Task<bool> UpdatePerson(Person bl_sheet);
        Person GetPersonBySvc_NO(string person);
        Task<List<PersonListViewModel>> getPersonList(int iDisplayStart, int iDisplayLength);
        Task<int> getPersonListCount();
        Task<List<PersonListViewModel>> getInactivePersonList();
        Task<List<PersonListViewModel>> getPersonListBySvc_No(string svc_no);
        Task<PersonListViewModel> getPersonById(int id);
        Task<PersonListViewModel> getSinglePerson(string person);
        Task<List<PersonListViewModel>> getPersonListold(int iDisplayStart, int iDisplayLength);
    }
}

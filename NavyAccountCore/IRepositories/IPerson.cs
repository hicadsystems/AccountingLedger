using Microsoft.EntityFrameworkCore;
using NavyAccountCore.Core.Entities;
using NavyAccountCore.Core.IRepositories;
using NavyAccountCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NavyAccountCore.Core.IRepositories
{
    public interface IPerson: IRepository<Person>
    {
        Person GetPersonBySVC_No(Expression<Func<Person, bool>> predicate);

        Task<PersonListViewModel> getSinglePerson(string svcno);
        Task<PersonListViewModel> getPersonById(int id);

        Task<List<PersonListViewModel>> getPersonList(int iDisplayStart, int iDisplayLength);
        Task<List<PersonListViewModel>> getPersonListold(int iDisplayStart, int iDisplayLength);
        Task<int> getPersonListCount();
        Task<List<PersonListViewModel>> getPersonInactiveList();
        Task<List<PersonListViewModel>> getPersonListBySvc_No(string svc_no);

    }
}

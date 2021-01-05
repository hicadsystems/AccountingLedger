using Microsoft.EntityFrameworkCore;
using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Entities;
using NavyAccountCore.Core.IRepositories;
using NavyAccountCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace NavyAccountCore.Core.Repositories
{
    public class PersonRepo : Repository<Person>, IPerson
    {
        private readonly INavyAccountDbContext context;
        public PersonRepo(INavyAccountDbContext context) : base(context)
        {
            this.context = context;
        }

        public Person GetPersonBySVC_No(Expression<Func<Person, bool>> predicate)
        {
            //if (!CheckContext.IsDisposed(context.Instance)) {
            //    var mme = "Memem";
            //}
            return context.person.FirstOrDefault(predicate);
        }

        public async Task<List<PersonListViewModel>> getPersonList(int iDisplayStart, int iDisplayLength)
        {
            return await (from pers in context.person
                          join npfranks in context.ranks on pers.rank equals npfranks.Id
                          where pers.dateleft == null
                          select new PersonListViewModel {
                              PersonID = pers.PersonID,
                              accountno = pers.accountno,
                              //bank = npfbank.bankname,
                              //BirthDate = (DateTime)pers.BirthDate,
                              dateemployed =  pers.dateemployed,
                              email = pers.email,
                              FirstName = pers.FirstName,
                              homeaddress = pers.homeaddress,
                              Initials = pers.Initials,
                              LastName = pers.LastName,
                              MiddleName = pers.MiddleName,
                              rank = npfranks.Description,
                              SVC_NO = pers.SVC_NO,
                              Sex = pers.Sex,
                              Title = pers.Title,
                              rankid = npfranks.Id

                          }).Skip(iDisplayStart).Take(iDisplayLength).ToListAsync();
        }
        public async Task<List<PersonListViewModel>> getPersonListold(int iDisplayStart, int iDisplayLength)
        {
            return await (from pers in context.person
                          join npfranks in context.ranks on pers.rank equals npfranks.Id
                          where pers.dateleft != null
                          select new PersonListViewModel
                          {
                              PersonID = pers.PersonID,
                              accountno = pers.accountno,
                              //bank = npfbank.bankname,
                              //BirthDate = (DateTime)pers.BirthDate,
                              dateemployed = pers.dateemployed,
                              email = pers.email,
                              FirstName = pers.FirstName,
                              homeaddress = pers.homeaddress,
                              Initials = pers.Initials,
                              LastName = pers.LastName,
                              MiddleName = pers.MiddleName,
                              rank = npfranks.Description,
                              SVC_NO = pers.SVC_NO,
                              Sex = pers.Sex,
                              Title = pers.Title,
                              rankid = npfranks.Id

                          }).Skip(iDisplayStart).Take(iDisplayLength).ToListAsync();
        }

        public async Task<int> getPersonListCount()
        {
            return await (from pers in context.person
                          join npfranks in context.ranks on pers.rank equals npfranks.Id
                          where pers.dateleft == null
                          select new PersonListViewModel
                          {
                              PersonID = pers.PersonID,
                              accountno = pers.accountno,
                              //bank = npfbank.bankname,
                              //BirthDate = (DateTime)pers.BirthDate,
                              dateemployed =  pers.dateemployed,
                              email = pers.email,
                              FirstName = pers.FirstName,
                              homeaddress = pers.homeaddress,
                              Initials = pers.Initials,
                              LastName = pers.LastName,
                              MiddleName = pers.MiddleName,
                              rank = npfranks.Description,
                              SVC_NO = pers.SVC_NO,
                              Sex = pers.Sex,
                              Title = pers.Title,
                              rankid = npfranks.Id

                          }).CountAsync();
        }

        public async Task<List<PersonListViewModel>> getPersonInactiveList()
        {
            return await (from pers in context.person
                          join npfranks in context.ranks on pers.rank equals npfranks.Id
                          where pers.dateleft != null
                          select new PersonListViewModel
                          {
                              PersonID = pers.PersonID,
                              accountno = pers.accountno,
                              //bank = npfbank.bankname,
                              //BirthDate = (DateTime)pers.BirthDate,
                              dateemployed =  pers.dateemployed,
                              email = pers.email,
                              FirstName = pers.FirstName,
                              homeaddress = pers.homeaddress,
                              Initials = pers.Initials,
                              LastName = pers.LastName,
                              MiddleName = pers.MiddleName,
                              rank = npfranks.Description,
                              SVC_NO = pers.SVC_NO,
                              Sex = pers.Sex,
                              Title = pers.Title,
                              rankid = npfranks.Id

                          }).ToListAsync();
        }

        public async Task<List<PersonListViewModel>> getPersonListBySvc_No(string svc_no)
        {
            return await (from pers in context.person
                          join npfranks in context.ranks on pers.rank equals npfranks.Id
                          //join npfbank in context.py_Banks on pers.bank equals npfbank.Id line.Replace(@"\", "");
                          where pers.SVC_NO.Replace(@"/","").Contains(svc_no)
                          select new PersonListViewModel
                          {
                              PersonID = pers.PersonID,
                              accountno = pers.accountno,
                              //bank = npfbank.bankname,
                             // BirthDate = (DateTime)pers.BirthDate,
                            //  dateemployed = "" + pers.dateemployed,
                              email = pers.email,
                              FirstName = pers.FirstName,
                              homeaddress = pers.homeaddress,
                              Initials = pers.Initials,
                              LastName = pers.LastName,
                              MiddleName = pers.MiddleName,
                              rank = npfranks.Description,
                              SVC_NO = pers.SVC_NO,
                              Sex = pers.Sex,
                              Title = pers.Title,
                              rankid = npfranks.Id

                          }).ToListAsync();
        }


        public async Task<PersonListViewModel> getPersonById(int id)
        {
            return await (from pers in context.person
                          join npfranks in context.ranks on pers.rank equals npfranks.Id
                          //join npfbank in context.py_Banks on pers.bank equals npfbank.Id
                          where pers.PersonID == id
                          select new PersonListViewModel
                          {
                              PersonID = pers.PersonID,
                              accountno = pers.accountno,
                              //bankid= npfbank.Id,
                              BirthDate = (DateTime)pers.BirthDate,
                              dateemployed = pers.dateemployed,
                              dateleft = pers.dateleft,
                              email = pers.email,
                              FirstName = pers.FirstName,
                              homeaddress = pers.homeaddress,
                              Initials = pers.Initials,
                              LastName = pers.LastName,
                              MiddleName = pers.MiddleName,
                              rank = npfranks.Description,
                              SVC_NO = pers.SVC_NO,
                              Sex = pers.Sex,
                              Title = pers.Title,
                              rankid = npfranks.Id

                          }).FirstOrDefaultAsync();
        }

        public async Task<PersonListViewModel> getSinglePerson(string svcno)
        {
            return await (from pers in context.person
                          join npfranks in context.ranks on pers.rank equals npfranks.Id
                          join npfbank in context.py_Banks on pers.bank equals npfbank.Id
                          where pers.SVC_NO == svcno
                          select new PersonListViewModel
                          {
                              PersonID = pers.PersonID,
                              accountno = pers.accountno,
                              bank = npfbank.bankname,
                              BirthDate = (DateTime)pers.BirthDate,
                              dateemployed = pers.dateemployed,
                              email = pers.email,
                              FirstName = pers.FirstName,
                              homeaddress = pers.homeaddress,
                              Initials = pers.Initials,
                              LastName = pers.LastName,
                              MiddleName = pers.MiddleName,
                              rank = npfranks.Description,
                              SVC_NO = pers.SVC_NO,
                              Sex = pers.Sex,
                              Title = pers.Title,
                              rankid = npfranks.Id

                          }).FirstOrDefaultAsync();
        }


    }


    public static class CheckContext{

        public static bool IsDisposed(this DbContext context)
        {
            var result = true;

            var typeDbContext = typeof(DbContext);
            var typeInternalContext = typeDbContext.Assembly.GetType("System.Data.Entity.Internal.InternalContext");
            try
            {
                var fi_InternalContext = typeDbContext.GetField("_internalContext", BindingFlags.NonPublic | BindingFlags.Instance);
                var pi_IsDisposed = typeInternalContext.GetProperty("IsDisposed");

                var ic = fi_InternalContext.GetValue(context);

                if (ic != null)
                {
                    result = (bool)pi_IsDisposed.GetValue(ic);
                }
            }
            catch (Exception ex) {
                result = false;
            }
            return result;
        }
    }
}
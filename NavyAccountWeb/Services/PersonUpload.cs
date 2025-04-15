using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Entities;
using NavyAccountCore.Models;
using NavyAccountWeb.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.Services
{
    public class PersonUpload
    {
        private readonly List<PersonUploadModel> PersonCaptures;
        private readonly IUnitOfWork unitOfWork;
        private string user;
        public PersonUpload(List<PersonUploadModel> loanCaptures, IUnitOfWork unitOfWork,string user)
        {

            this.PersonCaptures = loanCaptures;
            this.unitOfWork = unitOfWork;
            this.user = user;

        }

        public async Task processUploadInThread()
        {

            foreach (var s in PersonCaptures)
            {
                var getPersonId = unitOfWork.person.GetPersonBySVC_No(x => x.SVC_NO == s.SVC_NO);
                if (getPersonId == null)
                {
                    var getrank = unitOfWork.rank.GetRankbyName(x => x.Description == s.Rank);
                    unitOfWork.person.Create(new Person()
                    {
                        rank = getrank.Id,
                        SVC_NO = s.SVC_NO,
                        LastName = s.LastName,
                        FirstName = s.FirstName,
                        CreatedBy = user,
                        CreatedDate = System.DateTime.Now,
                    });
                   await unitOfWork.Done();
                }
            }

        }
    }
}
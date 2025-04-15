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
    public class ChangeUpload
    {
        private readonly List<UploadChangeModel> PersonCaptures;
        private readonly IUnitOfWork unitOfWork;
        private string user;
        public ChangeUpload(List<UploadChangeModel> loanCaptures, IUnitOfWork unitOfWork,string user)
        {

            this.PersonCaptures = loanCaptures;
            this.unitOfWork = unitOfWork;
            this.user = user;

        }

        public async Task processUploadInThread2()
        {

            foreach (var s in PersonCaptures)
            {
                var getPersonId = unitOfWork.person.GetPersonBySVC_No(x => x.SVC_NO == s.OLDSVC_NO);
                if (getPersonId == null)
                {
                    var getrank = unitOfWork.rank.GetRankbyName(x => x.Description == s.RANK);
                    getPersonId.SVC_NO = s.NEWSVC_NO;
                    getPersonId.rank = getrank.Id;

                    unitOfWork.person.Update(getPersonId);
                    await unitOfWork.Done();
                }
            }

        }
    }
}
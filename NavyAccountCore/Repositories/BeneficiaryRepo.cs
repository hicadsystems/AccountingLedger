using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Entities;
using NavyAccountCore.Core.IRepositories;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NavyAccountCore.Core.Repositories
{
    public class BeneficiaryRepo : Repository<Beneficiary>, IBeneficiary
    {
        private readonly INavyAccountDbContext context;

        public BeneficiaryRepo(INavyAccountDbContext context) : base(context)
        {
            this.context = context;

        }
        public async Task<List<Beneficiary>> GetBeneficiaryByPersonID(int personID)
        {
            return await (from bene in context.Beneficiaries
                         where bene.PersonID == personID
                         select new Beneficiary {
                             Id = bene.Id,
                             PersonID = bene.PersonID,
                             FirstName = bene.FirstName,
                             RelationshipId = bene.RelationshipId,
                             dateofbirth = bene.dateofbirth,
                             FullAddress = bene.FullAddress,
                             MobileNumber = bene.MobileNumber,
                             HomeNumber = bene.HomeNumber,
                             EmailAddress = bene.EmailAddress,
                             PlaceOfWork = bene.PlaceOfWork,
                             NextofkinType = bene.NextofkinType,
                             IsActive = bene.IsActive,
                             CreatedDate = bene.CreatedDate,
                             CreatedBy = bene.CreatedBy,
                             ModifiedBy = bene.ModifiedBy,
                             ModifiedDate = bene.ModifiedDate

                         }).ToListAsync();
    }

        public async Task<Beneficiary> GetBeneficiaryByID(int beneID)
        {
            return await (from bene in context.Beneficiaries
                          where bene.Id == beneID
                          select new Beneficiary
                          {
                              Id = bene.Id,
                              PersonID = bene.PersonID,
                              FirstName = bene.FirstName,
                              RelationshipId = bene.RelationshipId,
                              dateofbirth = bene.dateofbirth,
                              FullAddress = bene.FullAddress,
                              MobileNumber = bene.MobileNumber,
                              HomeNumber = bene.HomeNumber,
                              EmailAddress = bene.EmailAddress,
                              PlaceOfWork = bene.PlaceOfWork,
                              NextofkinType = bene.NextofkinType,
                              IsActive = bene.IsActive,
                              CreatedDate = bene.CreatedDate,
                              CreatedBy = bene.CreatedBy,
                              ModifiedBy = bene.ModifiedBy,
                              ModifiedDate = bene.ModifiedDate

                          }).FirstOrDefaultAsync();
        }
    }
}

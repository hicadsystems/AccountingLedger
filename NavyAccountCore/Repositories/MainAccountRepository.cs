
using Microsoft.EntityFrameworkCore;
using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Entities;
using NavyAccountCore.Core.IRepositories;
using NavyAccountCore.Core.Repositories;
using System;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;
using NavyAccountCore.Models;
using System.Collections.Generic;

namespace NavyAccountCore.Core.Repositories
{
    public class MainAccountRepository :Repository<npf_mainact>, IMainAccount
    {
        private readonly INavyAccountDbContext context;
        public MainAccountRepository(INavyAccountDbContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<npf_mainact> GetMainAccountByCode(Expression<Func<npf_mainact, bool>> predicate)
        {
            return await context.mainacts.FirstOrDefaultAsync(predicate);
        }

        public async Task<npf_mainact> GetLastMainAccountByCode(string maincode)
        {
            return await context.mainacts.Where(x => x.maincode.StartsWith(maincode)).OrderByDescending(y => y.maincode).FirstOrDefaultAsync();
        }


        public async Task<List<MainAccountView>> GetMainAccountDesc()
        {
            return await(from npfmain in context.mainacts
                         join npfbal in context.npf_Balsheets on npfmain.npf_balsheet_bl_code equals npfbal.bl_code
                         join npfsubtype in context.sub_Types on npfmain.subtype equals npfsubtype.subtype
                         orderby npfmain.maincode
                         select new MainAccountView
                         {
                             Id = npfmain.Id,
                             maincode = npfmain.maincode,
                             description = npfmain.description,
                             subtype = npfmain.subtype,
                             shortname = npfmain.shortname,
                             subtypeDesc = npfsubtype.subtypedesc,
                             datecreated = npfmain.datecreated,
                             createdby = npfmain.createdby,
                             npf_balsheet_bl_code = npfmain.npf_balsheet_bl_code,
                             balSheetdesc = npfbal.bl_desc
                         }).ToListAsync();
        }

        public IEnumerable<MainAccountView> GetMainPerPage(int iDisplayStart, int iDisplayLength,out int totalcount)
        {
            totalcount = context.mainacts.ToList().Count();
            return  (from npfmain in context.mainacts
                                 join npfbal in context.npf_Balsheets on npfmain.npf_balsheet_bl_code equals npfbal.bl_code
                                 join npfsubtype in context.sub_Types on npfmain.subtype equals npfsubtype.subtype
                                 orderby npfmain.maincode
                                 select new MainAccountView
                                 {
                                     Id = npfmain.Id,
                                     maincode = npfmain.maincode,
                                     description = npfmain.description,
                                     subtype = npfmain.subtype,
                                     shortname = npfmain.shortname,
                                     subtypeDesc = npfsubtype.subtypedesc,
                                     datecreated = npfmain.datecreated,
                                     createdby = npfmain.createdby,
                                     npf_balsheet_bl_code = npfmain.npf_balsheet_bl_code,
                                     balSheetdesc = npfbal.bl_desc
                                 }).Skip(iDisplayStart).Take(iDisplayLength).ToList();
        }
    }
}
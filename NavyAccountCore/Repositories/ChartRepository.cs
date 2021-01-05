using NavyAccountCore.Core.Entities;
using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.IRepositories;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using NavyAccountCore.Models;
using System.Collections.Generic;

namespace NavyAccountCore.Core.Repositories
{
    public class ChartRepository :Repository<npf_chart>, IAccountChart
    {
        private readonly INavyAccountDbContext context;
        public ChartRepository(INavyAccountDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<int> getChartofAccountListCount()
        {
            return await (from npfCharts in context.npf_Charts
                          join npfmain in context.mainacts on npfCharts.mainAccountCode equals npfmain.maincode
                          join npfbal in context.npf_Balsheets on npfCharts.balSheetCode equals npfbal.bl_code
                          join npfsubtype in context.sub_Types on npfCharts.subtype equals npfsubtype.subtype
                          orderby npfCharts.acctcode
                          select new ChartofAccountView
                          {
                              Id = npfCharts.Id,
                              acctcode = npfCharts.acctcode,
                              description = npfCharts.description,
                              mainAccountCode = npfCharts.mainAccountCode,
                              subtype = npfCharts.subtype,
                              balSheetCode = npfCharts.balSheetCode,
                              datecreated = npfCharts.datecreated,
                              createdby = npfCharts.createdby,
                              mainAccountCode_desc = npfmain.description,
                              balSheetCode_desc = npfbal.bl_desc,
                              subtype_desc = npfsubtype.subtypedesc
                          }).CountAsync();


        }
        public async Task<IEnumerable<ChartofAccountView>> getAllChartofAccountsCH()
        {

            return await (from npfCharts in context.npf_Charts
                          join npfmain in context.mainacts on npfCharts.mainAccountCode equals npfmain.maincode
                          join npfbal in context.npf_Balsheets on npfCharts.balSheetCode equals npfbal.bl_code
                          join npfsubtype in context.sub_Types on npfCharts.subtype equals npfsubtype.subtype
                          where npfCharts.subtype!="0" && npfCharts.balSheetCode!="0" orderby npfCharts.acctcode
                          select new ChartofAccountView
                          {
                              Id = npfCharts.Id,
                              acctcode = npfCharts.acctcode,
                              description = npfCharts.description,
                              mainAccountCode = npfCharts.mainAccountCode,
                              subtype = npfCharts.subtype,
                              balSheetCode = npfCharts.balSheetCode,
                              datecreated = npfCharts.datecreated,
                              createdby = npfCharts.createdby,
                              mainAccountCode_desc = npfmain.description,
                              balSheetCode_desc = npfbal.bl_desc,
                              subtype_desc = npfsubtype.subtypedesc
                          }).ToListAsync();


        }

        public async Task<List<ChartofAccountView>> getAllChartofAccounts_Desc() {

            return await (from npfCharts in context.npf_Charts
                         join npfmain in context.mainacts on npfCharts.mainAccountCode equals npfmain.maincode
                         join npfbal in context.npf_Balsheets on npfCharts.balSheetCode equals npfbal.bl_code
                         join npfsubtype in context.sub_Types on npfCharts.subtype equals npfsubtype.subtype
                          orderby npfCharts.acctcode
                          select new ChartofAccountView {
                             Id = npfCharts.Id,
                             acctcode = npfCharts.acctcode,
                             description = npfCharts.description,
                             mainAccountCode = npfCharts.mainAccountCode,
                             subtype = npfCharts.subtype,
                             balSheetCode = npfCharts.balSheetCode,
                             datecreated = npfCharts.datecreated,
                             createdby = npfCharts.createdby,
                             mainAccountCode_desc = npfmain.description,
                             balSheetCode_desc = npfbal.bl_desc,
                             subtype_desc = npfsubtype.subtypedesc
                         }).ToListAsync();
             

        }

        public async Task<List<ChartofAccountView2>> getAllChartofAccounts_Desc2(int iDisplayStart, int iDisplayLength)
        {

            return await (from npfCharts in context.npf_Charts
                         join npfmain in context.mainacts on npfCharts.mainAccountCode equals npfmain.maincode
                         join npfbal in context.npf_Balsheets on npfCharts.balSheetCode equals npfbal.bl_code
                         join npfsubtype in context.sub_Types on npfCharts.subtype equals npfsubtype.subtype
                         orderby npfCharts.acctcode
                         select new ChartofAccountView2
                         {
                             Id = npfCharts.Id,
                             acctcode = npfCharts.acctcode,
                             description = npfCharts.description,
                             mainAccountCode = npfCharts.mainAccountCode,
                             subtype = npfCharts.subtype,
                             balSheetCode = npfCharts.balSheetCode,
                             mainAccountCode_desc = npfmain.description,
                             balSheetCode_desc = npfbal.bl_desc,
                             subtype_desc = npfsubtype.subtypedesc
                         }).Skip(iDisplayStart).Take(iDisplayLength).ToListAsync();


        }

        public async Task<List<ChartofAccountView>> getListChartofAccounts_DescBymainAccount(string actCode)
        {

            return await (from npfCharts in context.npf_Charts
                          join npfmain in context.mainacts on npfCharts.mainAccountCode equals npfmain.maincode
                          join npfbal in context.npf_Balsheets on npfmain.npf_balsheet_bl_code equals npfbal.bl_code
                          join npfsubtype in context.sub_Types on npfmain.subtype equals npfsubtype.subtypedesc
                          where npfmain.maincode == actCode
                          orderby npfCharts.acctcode
                          select new ChartofAccountView
                          {
                              Id = npfCharts.Id,
                              acctcode = npfCharts.acctcode,
                              description = npfCharts.description,
                              mainAccountCode = npfCharts.mainAccountCode,
                              subtype = npfCharts.subtype,
                              balSheetCode = npfCharts.balSheetCode,
                              datecreated = npfCharts.datecreated,
                              createdby = npfCharts.createdby,
                              mainAccountCode_desc = npfmain.description,
                              balSheetCode_desc = npfbal.bl_desc,
                              subtype_desc = npfsubtype.subtypedesc
                          }).ToListAsync();


        }


        public async Task<ChartofAccountView> getSingleChartofAccounts_Desc(string actcode)
        {

            return await (from npfCharts in context.npf_Charts
                          join npfmain in context.mainacts on npfCharts.mainAccountCode equals npfmain.maincode
                          join npfbal in context.npf_Balsheets on npfmain.npf_balsheet_bl_code equals npfbal.bl_code
                          join npfsubtype in context.sub_Types on npfmain.subtype equals npfsubtype.subtypedesc
                          where npfCharts.acctcode == actcode
                          orderby npfCharts.acctcode
                          select new ChartofAccountView
                          {
                              Id = npfCharts.Id,
                              acctcode = npfCharts.acctcode,
                              description = npfCharts.description,
                              mainAccountCode = npfCharts.mainAccountCode,
                              subtype = npfCharts.subtype,
                              balSheetCode = npfCharts.balSheetCode,
                              datecreated = npfCharts.datecreated,
                              createdby = npfCharts.createdby,
                              mainAccountCode_desc = npfmain.description,
                              balSheetCode_desc = npfbal.bl_desc,
                              subtype_desc = npfsubtype.subtypedesc
                          }).FirstOrDefaultAsync();


        }

        public async Task<npf_chart> GetChartofAccountByCode(Expression<Func<npf_chart, bool>> predicate)
        {
            return await context.npf_Charts.FirstOrDefaultAsync(predicate);
        }
        public async Task<List<ChartofAccountView>> GetChartofAccountByCode1(string maincode)
        {
            return await(from npfCharts in context.npf_Charts                         
                         where npfCharts.acctcode.Substring(0,1) == "1" || npfCharts.acctcode.Substring(0, 1) == "2"
                         orderby npfCharts.acctcode
                         select new ChartofAccountView
                         {
                             Id = npfCharts.Id,
                             acctcode = npfCharts.acctcode,
                             description = npfCharts.description,
                           
                         }).ToListAsync();
        }
        public async Task<List<ChartofAccountView>> GetChartofAccountByCode2(string maincode)
        {
            return await (from npfCharts in context.npf_Charts
                          where npfCharts.acctcode.Substring(0, 1)=="1"|| npfCharts.acctcode.Substring(0, 1)=="2"
                          orderby npfCharts.acctcode
                          select new ChartofAccountView
                          {
                              Id = npfCharts.Id,
                              acctcode = npfCharts.acctcode,
                              description = npfCharts.description,

                          }).ToListAsync();
        }
        public async Task<List<ChartofAccountView>> GetChartofAccountByCode3(string maincode)
        {
            return await (from npfCharts in context.npf_Charts
                          where npfCharts.acctcode.Substring(0, 1) == "1" || npfCharts.acctcode.Substring(0, 1) == "2"
                          orderby npfCharts.acctcode
                          select new ChartofAccountView
                          {
                              Id = npfCharts.Id,
                              acctcode = npfCharts.acctcode,
                              description = npfCharts.description,

                          }).ToListAsync();
        }
        public async Task<List<ChartofAccountView>> GetChartofAccountByCode5(string maincode)
        {
            return await (from npfCharts in context.npf_Charts
                          where npfCharts.acctcode.Substring(0, 1) == "4"
                          orderby npfCharts.acctcode
                          select new ChartofAccountView
                          {
                              Id = npfCharts.Id,
                              acctcode = npfCharts.acctcode,
                              description = npfCharts.description,

                          }).ToListAsync();
        }
        public async Task<List<ChartofAccountView>> GetChartofAccountByCode4(string maincode)
        {
            return await (from npfCharts in context.npf_Charts
                          where npfCharts.subtype== maincode
                          orderby npfCharts.acctcode
                          select new ChartofAccountView
                          {
                              Id = npfCharts.Id,
                              acctcode = npfCharts.acctcode,
                              description = npfCharts.description,

                          }).ToListAsync();
        }


        public async Task<npf_chart> GetLasChartofAccountByCode(string maincode)
        {
            return await context.npf_Charts.Where(x => x.acctcode.StartsWith(maincode) && x.ispersonel == false).OrderByDescending(y => y.acctcode).FirstOrDefaultAsync();
        }

        
    }
}
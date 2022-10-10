using Dapper;
using NavyAccountWeb.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.Services
{
    public class SessionService:ISessionService
    {
        private readonly IDapper dapper;
        public SessionService(IDapper dapper)
        {
            this.dapper = dapper;
        }
        public async Task<int> MigrateToNewSession(string users)
        {
            var param = new DynamicParameters();
            param.Add("@scCreatedBy", users);

            var result = dapper.Execute("sr_MigrateToNewSession", param, commandType: System.Data.CommandType.StoredProcedure);

            return result;
        }
        public void MigratetoNewterm()
        {
            var param = new DynamicParameters();
            dapper.Execute("sr_MigrateToNewTerm", param, commandType: System.Data.CommandType.StoredProcedure);
        }
    }

}

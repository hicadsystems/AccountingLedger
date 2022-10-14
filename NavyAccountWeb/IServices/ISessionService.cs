using NavyAccountCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.IServices
{
    public interface ISessionService
    {
        Task<int> MigrateToNewSession(string users);
        void MigratetoNewterm();
        List<sr_SchoolRecordControl> GetCurrentSession();
    }
}

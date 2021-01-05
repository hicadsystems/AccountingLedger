using System;
using System.Collections.Generic;
using System.Linq;
using NavyAccountCore.Core.Entities;
using System.Threading.Tasks;

namespace NavyAccountWeb.IServices
{
    public interface ISubTypeService
    {
        IEnumerable<sub_type> GetSubTypes();
        Task<sub_type> GetSubTypeById(int id);
        Task<bool> AddSubType(sub_type subtype);
        Task<bool> UpdateSubType(sub_type subtype);
    }
}

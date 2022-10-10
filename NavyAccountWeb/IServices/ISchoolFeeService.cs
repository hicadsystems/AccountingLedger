using NavyAccountCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.IServices
{
    public interface ISchoolFeeService
    {
        Task<bool> AddSchoolFee(sr_SchoolFeeTB value);
        Task<bool> UpdateSchoolFee(sr_SchoolFeeTB value);
        Task<sr_SchoolFeeTB> GetAllSchoolFeeByCode(string code);
        Task<List<sr_SchoolFeeTB>> GetSchoolFeeByName(string schoolName);
        Task<List<SchoolFeeVM>> GetAllSchoolFee();
        Task<sr_SchoolFeeTB> GetSchoolFeeByid(int id);
        void DeleteSchoolFee(sr_SchoolFeeTB value);
    }
}

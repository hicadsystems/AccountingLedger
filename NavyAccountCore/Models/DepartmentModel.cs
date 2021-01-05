using System;
using System.Collections.Generic;
using System.Text;

namespace NavyAccountCore.Core.Models
{
    public class DepartmentModel
    {
        public string Name { get; set; }

        public IEnumerable<EmployeeModel> Employee { get; set; }
    }
}

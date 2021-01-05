using System;
using System.Collections.Generic;
using System.Text;

namespace NavyAccountCore.Core.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; }

        public int Verifications { get; set; }

        public int VerifiedDocuments { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace NavyAccountCore.Entities
{
    public class COA_MainAccount
    {
        public string accountcode { get; set; }
        public string description { get; set; }

        public DateTime Createdby { get; set; }

        public DateTime dateCreated { get; set; }

        public string type { get; set; }

        public string subType { get; set; }
    }
}

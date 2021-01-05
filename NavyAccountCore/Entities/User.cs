using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace NavyAccountCore.Core.Entities
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ResetPasswordCode { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }

        public List<UserRole> UserRoles { get; set; }

    }
}

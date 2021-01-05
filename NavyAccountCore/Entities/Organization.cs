using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NavyAccountCore.Core.Entities
{
    public class Organization
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Key]
        public string Code { get; set; }

    }
}
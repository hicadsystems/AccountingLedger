using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NavyAccountCore.Core.Entities
{
    public class Title
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Key]
        public string Code { get; set; }
    }
}

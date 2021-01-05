using System.ComponentModel.DataAnnotations;

namespace NavyAccountCore.Entities
{
    public class COA_Type
    {
        [Key]
        public int id { get; set; }

        public string typeCode { get; set; }

        public string typeDescription { get; set; }
    }
}

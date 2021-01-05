using NavyAccountCore.Core.Entities;
using NavyAccountWeb.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NavyAccountWeb.ViewModels
{
    public class FinancialViewModel
    {
        public int Id { get; set; }


        public string bl_code { get; set; }

        [Required]
        [StringLength(70)]
        public string bl_desc { get; set; }

        [Required]
        public int bl_group { get; set; }

        public IEnumerable<ac_accounttypes> GetAct { get; set; }
    }

    public class MainViewModel
    {
        public int Id { get; set; }

        public string accode { get; set; }

        [Required]
        [StringLength(70)]
        public string acdesc { get; set; }

        [Required]
        public int trade { get; set; }

        public IEnumerable<npf_balsheet> GetBal { get; set; }
    }

}
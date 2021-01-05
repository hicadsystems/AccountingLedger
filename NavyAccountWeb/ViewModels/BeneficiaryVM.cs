using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NavyAccountWeb.ViewModels
{
    public class BeneficiaryVM
    {
        public int Id { get; set; }
        public int PersonID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int RelationshipId { get; set; }
        public string FullAddress { get; set; }
        public string MobileNumber { get; set; }
        public string HomeNumber { get; set; }
        public string EmailAddress { get; set; }
        public string PlaceOfWork { get; set; }
        public string NextofkinType { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime dateofbirth { get; set; }
        public string Proportion { get; set; }
    }
}
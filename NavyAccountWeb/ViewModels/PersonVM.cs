using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NavyAccountWeb.ViewModels
{
    public class PersonVM
    {
        public int PersonID { get; set; }
        public string SVC_NO { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int TitleTypeId { get; set; }
        public int SexId { get; set; }
        public string email { get; set; }
        public DateTime dateemployed { get; set; }
        public int bank { get; set; }
        public string accountno { get; set; }
        public string bankbranch { get; set; }
        public string Factory { get; set; }
        public int Payrollclass { get; set; }
        public DateTime BirthDate { get; set; }
        public int ExitTypeid { get; set; }
        public DateTime dateleft { get; set; }
        public int branchid { get; set; }
        public int staffbranchid { get; set; }
        public int Departmentid { get; set; }
        public string Initials { get; set; }
        public string MaidenName { get; set; }
        public int Commandid { get; set; }
        public string Nationality { get; set; }
        public int MaritalStatusId { get; set; }
        public string PhotoPath { get; set; }
        public string FileName { get; set; }
        public string Beneficiary1 { get; set; }
        public string Proportion1 { get; set; }
        public int Relationship1 { get; set; }
        public string Phone1 { get; set; }
        public string Beneficiary2 { get; set; }
        public string Proportion2 { get; set; }
        public int Relationship2 { get; set; }
        public string Phone2 { get; set; }
        public string Beneficiary3 { get; set; }
        public string Proportion3 { get; set; }
        public int Relationship3 { get; set; }
        public string Phone3 { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool LiveStatus { get; set; }
        public int IdentificationId { get; set; }
        public int countryid { get; set; }
        public int LocalGovernmentareaId { get; set; }
        public int geozoneid { get; set; }
        public byte[] FingerPrint { get; set; }
        public string IdentityType { get; set; }
        public string IdentifyNumber { get; set; }
        public DateTime IdentityExpiryDate { get; set; }
        public int ReligionId { get; set; }
        public string GSMNumber { get; set; }
        public int tribeId { get; set; }
        public string homeaddress { get; set; }
        public string Birthplace { get; set; }
        public int religiondenominationid { get; set; }
        public int BloodGroupid { get; set; }
        public int entrymodeid { get; set; }
        public int commissiontypeid { get; set; }
        public int commissionid { get; set; }
        public DateTime commissiondate { get; set; }
        public DateTime senioritydate { get; set; }
        public string ExitReason { get; set; }
        public bool IsActive { get; set; }
        public int StateId { get; set; }
        public int SpecialisationAreaId { get; set; }
        public string hubbies { get; set; }
        public string NOKFileName { get; set; }
        public int SortId { get; set; }
        public string rank { get; set; }
    }
}
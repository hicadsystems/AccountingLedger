using System;
using System.Collections.Generic;
using System.Text;

namespace NavyAccountCore.Models
{
    public class StudentRecordVM
    {

        public int id { get; set; }
        public string Reg_Number { set; get; }
        public string Surname { set; get; }
        public string FirstName { set; get; }
        public string MiddleName { set; get; }
        public string Sex { set; get; }
        public int Age { get; set; }
        public string ClassCategory { get; set; }
        public string ParentalStatus { get; set; }
        public string SchoolCode { set; get; }
        public string Schoolname { set; get; }
        public int SchoolId { set; get; }
        public int ClassId { set; get; }
        public string CommencementDate { set; get; }
        public string ClassName { set; get; }
        public string PhoneNumber { set; get; }
        public string Email { set; get; }
        public string Status { set; get; }
        public string ExitDate { set; get; }
        public string ExitReason { set; get; }
        public string ParentName { set; get; }
        public string GuardianName { set; get; }
        public decimal? ClaimAmount { get; set; }
        public DateTime? ClaimDate { get; set; }

    }
    public class StudentReport
    {

        public int studentid { get; set; }
        public string Reg_Number { set; get; }
        public string StudentName { set; get; }
        public string ClassName { set; get; }
        public int? ClassId { get; set; }
        public string SchoolName { set; get; }
        public int? SchoolId { get; set; }
        public string Sex { set; get; }
        public int? Age { get; set; }
        public string ClassCategory { get; set; }
        public string ParentalStatus { get; set; }
        public string SchoolCode { set; get; }
        public string CommencementDate { set; get; }
        public string PhoneNumber { set; get; }
        public string Email { set; get; }
        public string Status { set; get; }
        public string ExitReason { set; get; }
        public string ParentName { set; get; }
        public string GuardianName { set; get; }
        public string Session { set; get; }
        public string Term { set; get; }


    }
}

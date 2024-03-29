﻿using System;
using System.Collections.Generic;

namespace NavyAccountWeb.Models
{
    public class ClaimPaymentReport
    {
        public string Reg_Number { get; set; }
        public string Surname { get; set; }
        public string Sex { get; set; }
        public int? Age { get; set; }
        public string ParentName { get; set; }
        public string ClassName { get; set; }
        public string Schoolname { get; set; }
        public string SchoolType { get; set; }
        public decimal? ClaimAmount { get; set; } = 0M;
        public decimal? FeeAmount { get; set; } = 0M;
        public string Period { get; set; }
    }

    public class ClaimPaymentReportExcel
    {
        public string Reg_Number { get; set; }
        public string Surname { get; set; }
        public string Sex { get; set; }
        public int? Age { get; set; }
        public string ParentName { get; set; }
        public string ClassName { get; set; }
        public string Schoolname { get; set; }
        public string SchoolType { get; set; }
        public decimal? ClaimAmount { get; set; } = 0M;
        public decimal? FeeAmount { get; set; } = 0M;
        public string Period { get; set; }
    }

    public class ClaimPaymentRecordReport
    {
        public List<ClaimPaymentReport> distintrecord { get; set; }
        public List<ClaimPaymentReport> allRecord { get; set; }
    }
    public class ClaimReport
    {
        public string Reg_Number { get; set; }
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Sex { get; set; }
        public int? Age { get; set; }
        public string ParentName { get; set; }
        public string Schoolname { get; set; }
        public string SchoolType { get; set; }
        public string ClassName { get; set; }
        public decimal? ClaimAmount { get; set; } = 0M;
        public decimal? FeeAmount { get; set; } = 0M;
        public DateTime? Transdate { get; set; }
        public string Period { get; set; }
    }
}

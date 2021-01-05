﻿using NavyAccountCore.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NavyAccountCore.Models
{
    public class ContrDiscVM
    {
        public int Id { get; set; }
        public string fundcode { get; set; }
        public decimal amountpay { get; set; }
        public decimal prvamt { get; set; }
        public decimal extamt { get; set; }
        public decimal amtvar { get; set; }
        public decimal amtdiscr { get; set; }
        public string contract { get; set; }
        public string period { get; set; }
        public string intract { get; set; }
        public string trusteeact { get; set; }
        public string incomeact { get; set; }
        public string liabilityact { get; set; }
        public string personname { get; set; }
        
        public string groupcode { get; set; }
        public DateTime datecreated { get; set; }
        public string createdby { get; set; }
       

    }
    
    }

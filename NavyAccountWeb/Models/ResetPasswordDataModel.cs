﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.Models
{
    public class ResetPasswordDataModel
    {
            [Required]
            public string UserId { get; set; }

            [Required]
            public string Code { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
            public string NewPassword { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Compare("NewPassword", ErrorMessage = "Passwords do not match.")]
            public string ConfirmPassword { get; set; }

    }
}

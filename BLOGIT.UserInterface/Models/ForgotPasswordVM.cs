﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BLOGIT.UserInterface.Models
{
    public class ForgotPasswordVM
    {
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
    }
}

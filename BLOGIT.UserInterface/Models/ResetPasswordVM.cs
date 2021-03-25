using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BLOGIT.UserInterface.Models
{
    public class ResetPasswordVM
    {
        [Required]
        public string Token { get; set; }

        [Required]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}

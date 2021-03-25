using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLOGIT.Models.UserServicesViewModels
{
    public class CreateUserRoleViewModel
    {
        [Required]
        public string RoleName { get; set; }
    }
    
}

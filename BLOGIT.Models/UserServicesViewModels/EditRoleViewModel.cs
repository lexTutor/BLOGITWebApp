﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLOGIT.Models.UserServicesViewModels
{
    public class EditRoleViewModel
    {
        public EditRoleViewModel()
            {
                Users = new List<string>();
            }
            public string Id { get; set; }
            [Required]
            public string RoleName { get; set; }

            public List<string> Users { get; set; }
        
    }
}

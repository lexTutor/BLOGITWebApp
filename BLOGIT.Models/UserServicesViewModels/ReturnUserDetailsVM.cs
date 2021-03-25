using System;
using System.Collections.Generic;
using System.Text;

namespace BLOGIT.Models.UserServicesViewModels
{
    public class ReturnUserDetailsVM
    {
        public string Id { get; set; }
        public string ImagePath { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}

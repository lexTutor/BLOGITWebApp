using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLOGIT.Models.ViewModels;
using Microsoft.AspNetCore.Http;

namespace BLOGIT.Models.UserServicesViewModels
{
    public class BlogUserViewModel: IWithPhotoUpload
    {
        public string Id { get; set; }
        public string FirstName { get;set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string ImagePath { get; set; }

        public IFormFile Image { get; set; }

        public string Bio { get; set; }

    }
}

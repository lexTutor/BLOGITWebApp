using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BLOGIT.Models
{
    public class BlogUser :IdentityUser
    {
       
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Bio { get; set; }

        public string ProfilePhoto { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public virtual ICollection<UserPostActivity> UserActivity { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

    }
}

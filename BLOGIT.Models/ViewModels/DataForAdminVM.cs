using BLOGIT.Models.UserServicesViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLOGIT.Models.ViewModels
{
    public class DataForAdminVM
    {
        public IEnumerable<PostsVM> AllSinglePosts { get; set; }

        public IEnumerable<BlogUserViewModel> AllUsers { get; set; }
        public int PostCount { get; set; }

        public int UsersCount { get; set; }

        public BlogUserViewModel UserDetails { get; set; }
    }
}

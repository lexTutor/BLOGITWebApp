using BLOGIT.Models.UserServicesViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLOGIT.Models.ViewModels
{
    public class AllEditorDataVM
    {
        public IEnumerable<PostsVM> AllSinglePosts { get; set; }
        public int PostCount { get; set; }

        public BlogUserViewModel UserDetails { get; set; }
    }
}

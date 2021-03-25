using System;

namespace BLOGIT.Models.ViewModels
{
    public class RecentPostsVM
    {
        public string PostId { get; set; }
        public DateTime CreationDate { get; set; }

        public string PostTitle { get; set; }

        public string PostDetails { get; set; }

        public string ImagePath { get; set; }

    }
}
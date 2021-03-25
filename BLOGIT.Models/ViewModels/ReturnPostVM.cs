using System;
using System.Collections.Generic;
using System.Text;

namespace BLOGIT.Models.ViewModels
{
    public class ReturnPostVM
    { 
        public string PostId { get; set; }

        public string PostCreator { get; set; }

        public string PostCategories { get; set; } //IEnumerable<string>
        public  IEnumerable<ReturnCommentsVm> commentsForThisPost { get; set; }
        public int Likes { get; set; }

        public int CommentCount { get; set; }

        public string PostTitle { get; set; }

        public string PostDetails { get; set; }

        public string ImagePath { get; set; }
        public AddCommentVM addComment { get; set; }
        public List<RecentPostsVM> MostRecentPosts { get; set; }

        public DateTime CreationDate { get; set; }
    }
}

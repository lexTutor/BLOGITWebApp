using System;
using System.Collections.Generic;
using System.Text;

namespace BLOGIT.Models.ViewModels
{
    public class OtherPostActivitiesPostVM
    {
        public string PostId { get; set; }
        public string PostCreatorName { get; set; }

        public string PostCategories { get; set; } 
        public int Likes { get; set; }

        public int CommentCount { get; set; }

        public string PostTitle { get; set; }

        public bool ApprovalState { get; set; }

        public string PostDetails { get; set; }

        public string ImagePath { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace BLOGIT.Models.ViewModels
{
    public class PostsVM
    {
        public string PostCreatorName { get; set; }
        public string PostId { get; set; }
        public string PostTitle { get; set; }
        public string ImagePath { get; set; }
        public string PostCategories { get; set; }
        public bool ApprovalState { get; set; }
        public DateTime CreationDate { get; set; }
    }
}

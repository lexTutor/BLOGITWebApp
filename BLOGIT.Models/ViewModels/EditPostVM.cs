using BLOGIT.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLOGIT.Models.ViewModels
{
    public class EditPostVM
    {
        public string PostCreatorName { get; set; }

        public List<PostCategories> PostCategories { get; set; }

        public int CommentCount { get; set; }

        public int LikesCount { get; set; }

        [Required]
        public string PostTitle { get; set; }

        [Required]
        [ValidatePostContentLength(30, ErrorMessage = "A post must have at least 30 words.")]
        [VaLidateDataContent(ErrorMessage = "Your post contains unaccecptable words")] public string PostDetails { get; set; }

        public bool ApprovalState { get; set; }

        public string ImagePath { get; set; }

        public DateTime CreationDate { get; set; }
    }
}

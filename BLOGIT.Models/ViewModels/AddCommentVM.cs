using BLOGIT.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLOGIT.Models.ViewModels
{
    public class AddCommentVM
    {
        public Post Post { get; set; }

        public DateTime CommentTime { get; set; } = DateTime.Now;

        public string UserFullName { get; set; }

        public string ImagePath { get; set; }

        [Required]
        [VaLidateDataContent(ErrorMessage = "Your comment contains unaccecptable words")]
        public string Comment { get; set; }
    }
}

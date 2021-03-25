using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLOGIT.Models
{
    public class Comments
    {
        public Comments()
        {
            this.CommentId = Guid.NewGuid().ToString().Substring(1, 6);

        }
        [Key]
        public string CommentId { get; set; }

        [Required]
        public Post Post { get; set; }

        public DateTime CommentTime { get; set; }

        [Required]
        public string UserFullName { get; set; }

        public string UserImage { get; set; }

        [Required]
        public string Comment { get; set; }
    }
}

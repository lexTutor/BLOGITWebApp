using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLOGIT.Models
{
    public class Post
    {
        public Post()
        {
            this.PostId = Guid.NewGuid().ToString().Substring(2, 8);

        }
        public string PostId { get; set; }

        public BlogUser PostCreator { get; set; }

        public virtual ICollection<PostCategories> PostCategories { get; set; }
        public virtual ICollection<Comments> Comments { get; set; }
        public virtual ICollection<Likes> Likes { get; set; }

        [Required]

        public string PostTitle { get; set; }

        [Required]

        public string PostDetails { get; set; }

        public bool ApprovalState { get; set; }

        public string ImagePath { get; set; }

        public DateTime CreationDate { get; set; }


    }
}

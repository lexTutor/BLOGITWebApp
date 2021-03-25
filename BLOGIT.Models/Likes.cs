using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLOGIT.Models
{
    public class Likes
    {
        public Likes()
        {
            this.LikesId = Guid.NewGuid().ToString().Substring(3, 6);

        }
        [Required]
        public string LikesId { get; set; }

        public Post LikedPost { get; set; }

        public BlogUser UserWhoLiked { get; set; }
    }
}

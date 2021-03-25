using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLOGIT.Models
{
    public class UserPostActivity
    {
        public UserPostActivity()
        {
            this.UserPostActivityId = Guid.NewGuid().ToString().Substring(3, 6);
        }
        [Key]
        public string UserPostActivityId { get; set; }

        [Required]
        public BlogUser BlogUser { get; set; }

        [Required]
        public UserActivityType ActivityType { get; set; }

        [Required]
        public Post Post { get; set; }

        public DateTime ActivityTime { get; set; } = DateTime.Now;

    }
}

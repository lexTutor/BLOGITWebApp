using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLOGIT.Models
{
    public class UserDataActivity
    {
        public UserDataActivity()
        {
            this.UserDataActivityId = Guid.NewGuid().ToString().Substring(4, 6);

        }
        [Key]
        public string UserDataActivityId { get; set; }

        [Required]
        public BlogUser BlogUser { get; set; }

        [Required]
        public UserActivityType ActivityType { get; set; }

        public DateTime ActivityTime { get; set; } = DateTime.Now;
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLOGIT.Models
{
    public class UserActivityType
    {
        public UserActivityType()
        {
            this.UserActivityTypeId = Guid.NewGuid().ToString().Substring(2, 5);
        }
        public string UserActivityTypeId { get; set; }

        [Required]
        public string ActivityTypeName { get; set; }

        public virtual ICollection<UserPostActivity> UserPostActivities { get; set; }
        public virtual ICollection<UserDataActivity> UserDataActivities { get; set; }

    }
}

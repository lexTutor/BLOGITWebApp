using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLOGIT.Models
{
    public class Category
    {
        public Category()
        {
            this.CategoryId = Guid.NewGuid().ToString().Substring(1, 9);

        }
        public string CategoryName { get; set; }

        [Key]
        public string CategoryId { get; set; }

        public virtual ICollection<PostCategories> PostCategories { get; set; }

    }
}

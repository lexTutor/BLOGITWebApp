using System;
using System.Collections.Generic;
using System.Text;

namespace BLOGIT.Models
{
    public class PostCategories
    {
        public Category Category { get; set; }

        public string CategoryId { get; set; }

        public Post Post { get; set; }

        public string PostId { get; set; }
    }
}

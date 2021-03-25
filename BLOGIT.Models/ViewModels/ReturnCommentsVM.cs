using System;
using System.Collections.Generic;
using System.Text;

namespace BLOGIT.Models.ViewModels
{
    public class ReturnCommentsVm
    {

        public string Comment { get; set; }

        public DateTime CommentTime { get; set; }

        public string UserFullName { get; set; }

        public string UserImage { get; set; }
    }
}

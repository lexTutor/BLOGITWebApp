using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLOGIT.Models.UserServicesViewModels
{
    public class AddRemoveUserFromRoleViewModel
    {
        public string UserId { get; set; }
        public bool IsChecked { get; set; }
        public string Username { get; set; }
    }
}

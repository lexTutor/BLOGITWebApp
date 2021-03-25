using BLOGIT.Models.UserServicesViewModels;
using BLOGIT.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLOGIT.Models.ObjectRelationalMappers
{
    public static class UserMapper
    {
        public static BlogUserViewModel MapToBlogUserVM(BlogUser user)
        {
            var blogVm = new BlogUserViewModel()
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                ImagePath = user.ProfilePhoto,
                Bio = user.Bio
            };
            return blogVm;
        }

        public static List<BlogUserViewModel> BlogUserViewModels(IQueryable<BlogUser> blogUsers)
        {
            var listUsersViewModel = new List<BlogUserViewModel>();

            foreach (var item in blogUsers)
            {
                listUsersViewModel.Add(new BlogUserViewModel()
                {
                    Id = item.Id,
                    UserName = item.UserName,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    PhoneNumber = item.PhoneNumber,
                });
            }

            return listUsersViewModel;
        }

        public static DataForAdminVM ReturnAdminDataRequirements(IEnumerable<PostsVM> allPosts, IEnumerable<BlogUserViewModel> allUsers)
        {
            DataForAdminVM Alldata = new DataForAdminVM
            {
                AllSinglePosts = allPosts,
                AllUsers = allUsers,
                PostCount = allPosts.Count(),
                UsersCount = allUsers.Count()
            };
            return Alldata;
        }
    }
}

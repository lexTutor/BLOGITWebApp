using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BLOGIT.Commons;
using BLOGIT.Models;
using BLOGIT.Models.UserServicesViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using BLOGIT.Models.ObjectRelationalMappers;
using BLOGIT.Commons.ObjectRelationalMappers;
using BLOGIT.Repository;
using BLOGIT.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace BLOGIT.UserInterface.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<BlogUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IPostService _postService;

        public AdminController(RoleManager<IdentityRole> roleManager, IWebHostEnvironment webHostEnvironment, IPostService postService,
            UserManager<BlogUser> userManager)
        {
            _roleManager = roleManager;
            _webHostEnvironment = webHostEnvironment;
            _postService = postService;
            _userManager = userManager;
        }


        /// <summary>
        /// Gets the Admin dashboard
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var CurrentUser = await _userManager.GetUserAsync(HttpContext.User);
            var users = _userManager.Users;
            var listUsersViewModel = UserMapper.BlogUserViewModels(users);
            var allPostsMain = await _postService.GetAllPosts();
            var allPostsVM = PostMappers.ReturnAllPosts(allPostsMain);
            var allData = UserMapper.ReturnAdminDataRequirements(allPostsVM.AllSinglePosts, listUsersViewModel);
            allData.UserDetails = UserMapper.MapToBlogUserVM(CurrentUser);
            return View(allData);
        }

        /// <summary>
        /// Gets the delete post View
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> DeletePost(string postId)
        {
            var post = await _postService.GetPost(postId);
            var deletePostVM = PostMappers.ConvertToOtherPostVM(post);
            return View(deletePostVM);
        }

        /// <summary>
        /// Allows the Admin user to delete a post from the database
        /// </summary>
        /// <param name="postToDelete"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Delete(OtherPostActivitiesPostVM postToDelete)
        {
            var post = await _postService.GetPost(postToDelete.PostId);
            var delete = await _postService.DeletePost(post);
            return RedirectToAction("Index", "Admin");
        }

        /// <summary>
        /// Gets the approve post view
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Approve(string postId)
        {
            var post = await _postService.GetPost(postId);
            var approvePostVM = PostMappers.ConvertToOtherPostVM(post);
            return View(approvePostVM);
        }

        /// <summary>
        /// Allows the admin to approve a post before it can be visible on the home page
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Approved(string postId)
        {
            var post = await _postService.GetPost(postId);
            var approve = await _postService.ApprovePost(post);
            return RedirectToAction("Index", "Admin");
        }

        public async Task<IActionResult> DetailsUser(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            var blogVm = new BlogUserViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber
            };

            return View(blogVm);
        }


        // GET: AdminController/Delete/
        // this method gets the details of the user to be deleted
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _userManager.FindByIdAsync(id);
            //var postVm = _mapper.Map<PostViewModel>(post);
            var blogVm = UserMapper.MapToBlogUserVM(user);
            return View(blogVm);
        }

        // POST: this method deletes a user /Delete/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
           var user = await _userManager.FindByIdAsync(id);
           await _userManager.DeleteAsync(user);
           //Thread.Sleep(2000);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        //this method creates a role
        public async Task<IActionResult> CreateRole(CreateUserRoleViewModel roleVm)
        {
            if (ModelState.IsValid)
            {
                var role = new IdentityRole()
                {
                    Name = roleVm.RoleName
                };
                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListOfRoles", "Admin");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(roleVm);
        }

        // GET: this method gets the list of roles 
        public IActionResult ListOfRoles()
        {
            var listOfRoles = _roleManager.Roles;
            return View(listOfRoles);
        }


        // GET: this method gets each role by their Id and users under this role
        [HttpGet]
        public async Task<IActionResult> EditUserRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.ErrorMessage($"the role with id : {id} could not be found");
                return View("GeneralError");
            }

            var editVm = new EditRoleViewModel()
            {
                Id = role.Id,
                RoleName = role.Name,
            };

            foreach (var user in _userManager.Users)
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    editVm.Users.Add(user.UserName);
                }

            }

            return View(editVm);
        }



        [HttpPost]
        // POST: this method then updates the role
        public async Task<IActionResult> EditUserRole(EditRoleViewModel editVm)
        {
            var role = await _roleManager.FindByIdAsync(editVm.Id);
            if (role == null)
            {
                ViewBag.ErrorMessage($"the role with id : {editVm.Id} could not be found");
                return View("GeneralError");
            }
            else
            {
                role.Name = editVm.RoleName;
                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListOfRoles");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(editVm);
            }

        }


        [HttpGet]
        // GET: this method gets the all users whether under a role or not under a role
        public async Task<IActionResult> EditUsersInRole(string id)
        {
            ViewBag.RoleId = id;

            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"role with the id {id} cannot be found";
                return NotFound();
            }

            var model = new List<AddRemoveUserFromRoleViewModel>();

            foreach (var user in _userManager.Users)
            {
                var userRoleViewModel = new AddRemoveUserFromRoleViewModel
                {
                    Username = user.UserName,
                    UserId = user.Id
                };

                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsChecked = true;
                }
                else
                {
                    userRoleViewModel.IsChecked = false;
                }

                model.Add(userRoleViewModel);
            }

            return View(model);
        }



        /// <summary>
        // POST: this method update or assign users to an existing role
        /// </summary>
        /// <param name="userRoleVm"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<AddRemoveUserFromRoleViewModel> userRoleVm, string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"role with the id {id} cannot be found";
                return NotFound();
            }

            for (int i = 0; i < userRoleVm.Count; i++)
            {
                var user = await _userManager.FindByIdAsync(userRoleVm[i].UserId);
                IdentityResult result;
                if (userRoleVm[i].IsChecked && !(await _userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await _userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!userRoleVm[i].IsChecked && (await _userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else if (!userRoleVm[i].IsChecked && !(await _userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await _userManager.AddToRoleAsync(user, "Member");
                }
                else
                {
                    continue;
                }

                if (result.Succeeded)
                {
                    if (i < (userRoleVm.Count - 1))
                    {
                        continue;
                    }
                    else
                    {
                        return RedirectToAction("EditUserRole", new { id = role.Id });
                    }
                }

            }
            return RedirectToAction("EditUserRole", new { id = role.Id });
        }


    }
}

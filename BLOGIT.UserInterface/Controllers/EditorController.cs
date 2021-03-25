using BLOGIT.Commons.ObjectRelationalMappers;
using BLOGIT.Models;
using BLOGIT.Models.ObjectRelationalMappers;
using BLOGIT.Models.ViewModels;
using BLOGIT.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLOGIT.UserInterface.Controllers
{
    [Authorize(Roles ="Editor, ProEditor")]
    public class EditorController: Controller
    {
        private UserManager<BlogUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IPostService _postService;

        public EditorController(RoleManager<IdentityRole> roleManager, IWebHostEnvironment webHostEnvironment, IPostService postService,
            UserManager<BlogUser> userManager)
        {
            _roleManager = roleManager;
            _webHostEnvironment = webHostEnvironment;
            _postService = postService;
            _userManager = userManager;
        }

        /// <summary>
        /// Gets the editors dashboard
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Editor()
        {
            var CurrentUser = await _userManager.GetUserAsync(HttpContext.User);
            var allPostsMain = await _postService.GetAllPosts();
            var editorPsts = allPostsMain.Where(post => post.PostCreator == CurrentUser);
            var allPostsVM = PostMappers.ReturnAllPosts(editorPsts);
            allPostsVM.UserDetails = UserMapper.MapToBlogUserVM(CurrentUser);
            return View("Editor", allPostsVM);
        }


        /// <summary>
        /// Gets the edot posts view
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Edit(string postId)
        {
            var post = await _postService.GetPost(postId);
            var editedPostVM = PostMappers.ConvertToOtherPostVM(post);
            return View("EditPost", editedPostVM);
        }

        /// <summary>
        /// Allows an editor to edit the details of his post
        /// </summary>
        /// <param name="postToUpdate"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(OtherPostActivitiesPostVM postToUpdate)
        {
            var post = await _postService.GetPost(postToUpdate.PostId);
            var assignEditedData = PostMappers.MapToEditedPostView(post, postToUpdate);
            var edit = await _postService.EditPost(post);
            return RedirectToAction( "Editor", "Editor");
        }
    }
}

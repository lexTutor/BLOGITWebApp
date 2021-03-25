using BLOGIT.Commons.ObjectRelationalMappers;
using BLOGIT.Models;
using BLOGIT.Models.ObjectRelationalMappers;
using BLOGIT.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLOGIT.Models.UserServicesViewModels;

namespace BLOGIT.UserInterface.Controllers
{
    /// <summary>
    /// Handles the Http Get requests for the dashboards
    /// </summary>
    public class MembersController : Controller
    {
        private readonly ILogger<MembersController> _logger;
        private readonly IPostService _postService;
        private readonly UserManager<BlogUser> _userManager;
        private readonly IImageProcessorService _imageProcessor;

        public MembersController(ILogger<MembersController> logger, IPostService postService, UserManager<BlogUser> userManager, IImageProcessorService imageProcessor)
        {
            _logger = logger;
            _postService = postService;
            _userManager = userManager;
            _imageProcessor = imageProcessor;
        }

        [HttpGet]
        public async Task<IActionResult> Member()
        {
            var CurrentUser = await _userManager.GetUserAsync(HttpContext.User);
            var userDetails = UserMapper.MapToBlogUserVM(CurrentUser);
            return View(userDetails);
        }

        [HttpGet]
        public async Task<IActionResult> MemberDetails(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var userDetails = UserMapper.MapToBlogUserVM(user);

            return View(userDetails);
        }

        [HttpPost]
        public async Task<IActionResult> MemberDetails(BlogUserViewModel model)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var imagePath = _imageProcessor.ImageUpload(model);
            user.UserName = model.UserName;
            user.Email = model.Email;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Bio = model.Bio;
            user.ProfilePhoto = imagePath;
            user.PhoneNumber = model.PhoneNumber;
            //user = UserMapper.MapBackToBlogUser(model);
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Member");
            }

            return View(model);
        }
    }
}

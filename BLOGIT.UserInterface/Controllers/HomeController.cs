using BLOGIT.Repository;
using BLOGIT.UserInterface.Models;
using BLOGIT.Commons.ObjectRelationalMappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BLOGIT.Models;
using Microsoft.AspNetCore.Authorization;

namespace BLOGIT.UserInterface.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPostService _postService;

        public HomeController(ILogger<HomeController> logger, IPostService postService)
        {
            _logger = logger;
            this._postService = postService;
        }

        /// <summary>
        /// Gets all the approved posts from the database and displays it on the home page
        /// </summary>
        /// <returns></returns>
       
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
          var allPostsMain = await _postService.GetAllPosts();
            var approvedPosts = allPostsMain.Where(post => post.ApprovalState == true);
            var allPostsVM = PostMappers.ReturnAllPosts(approvedPosts);
            return View(allPostsVM);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [AllowAnonymous]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

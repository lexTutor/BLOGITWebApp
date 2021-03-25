using BLOGIT.Commons.ObjectRelationalMappers;
using BLOGIT.Models;
using BLOGIT.Models.ViewModels;
using BLOGIT.Repository;
using BLOGIT.Repository.CategoryServices;
using BLOGIT.Repository.LikeServices;
using BLOGIT.Repository.PostServices.CommentServices;
using BLOGIT.UserInterface.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BLOGIT.UserInterface.Controllers
{
    public class PostController : Controller
    {
        private readonly ILogger<PostController> _logger;
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;
        private readonly IImageProcessorService _imageProcessorService;
        private readonly UserManager<BlogUser> _userManager;
        private readonly ILikeService _likeService;

        public ICategoryService _Categoryservices { get; }

        public PostController(ILogger<PostController> logger, IPostService postService, ILikeService likeService, ICommentService commentService,
            IImageProcessorService imageProcessorService, ICategoryService categoryservices, UserManager<BlogUser> userManager)
        {
            _logger = logger;
            _postService = postService;
            _commentService = commentService;
            _imageProcessorService = imageProcessorService;
            _Categoryservices = categoryservices;
            _userManager = userManager;
            _likeService = likeService;
        }

        /// <summary>
        /// Helper method to create the post details model
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public async Task<ReturnPostVM> CreatePostVM(string postId)
        {
            var post = await _postService.GetPost(postId);
            var recentPosts = await _postService.GetMostRecentPost();
            var returnPost = PostMappers.ReturnPosts(post, recentPosts);
            return returnPost;
        }

        /// <summary>
        /// Helper method to generate the selectite, for the category dropdown box
        /// </summary>
        /// <returns></returns>
        public async Task<AddPostVM> CreateAddPostVM()
        {
            var post = new AddPostVM();
            var tags = await _Categoryservices.GetPostCategory();
            var selsectList = new List<SelectListItem>();
            foreach (var item in tags)
            {
                selsectList.Add(new SelectListItem { Value = item.CategoryId, Text = item.CategoryName });
            }
            post.Categories = selsectList;
            return post;
        }


        /// <summary>
        /// Gets the postDetails View
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns> 
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index(string postId)
        {
            var post = await CreatePostVM(postId);
            return View(post);
        }

      
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// Gets the add post details view
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles ="Editor, ProEditor")]
        public async Task<IActionResult> AddPost()
        {
            var post = await CreateAddPostVM();
            return View(post);
        }

        /// <summary>
        /// Posts the new post instance to the database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Editor, ProEditor")]
        public async Task<IActionResult> AddPost(AddPostVM model)
        {
            if (!ModelState.IsValid)
            {
                var post = await CreateAddPostVM();
                return View(post);
            }
            else
            {

                var user = await _userManager.GetUserAsync(HttpContext.User);
                var postToAdd = PostMappers.AddPostToVM(model);
                var imagepath = _imageProcessorService.ImageUpload(model);
                postToAdd.ImagePath = imagepath;
                postToAdd.PostCreator = user;
                await _postService.AddPost(postToAdd);

                return RedirectToAction("Index", new { postToAdd.PostId });
            }

        }

        /// <summary>
        /// Posts the comment to the database
        /// </summary>
        /// <param name="forComment"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CommentOnPost(ReturnPostVM forComment)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    var returnedPost = await CreatePostVM(forComment.PostId);
                    return RedirectToAction("Index", new { returnedPost.PostId });
                }
               
                var post = await _postService.GetPost(forComment.PostId);
                forComment.addComment.UserFullName = user.FirstName + " " + user.LastName;
                forComment.addComment.Post = post;
                forComment.addComment.ImagePath = user.ProfilePhoto;
                var comment = PostMappers.MapCommentVMToComment(forComment.addComment);
                await _commentService.AddComment(comment);

                var returnPost = await CreatePostVM(forComment.PostId);
                return RedirectToAction("Index", new { returnPost.PostId });
            }
        }

        /// <summary>
        /// Allows the user to like a post
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> LikePost(ReturnPostVM postToLike)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    var returnedPost = await CreatePostVM(postToLike.PostId);
                    return RedirectToAction("Index", new { returnedPost.PostId });
                }
  
                var allLikes = await _likeService.GetAllLikes();
                var checkNonRepeat = allLikes.Any(like=> like.LikedPost.PostId == postToLike.PostId && like.UserWhoLiked == user );
                if (checkNonRepeat)
                {
                    var returnPost = await CreatePostVM(postToLike.PostId);
                    return View("Index", returnPost);
                }
                else
                {
                    var post = await _postService.GetPost(postToLike.PostId);
                    var Like = PostMappers.AddLike(post, user); //Change to getuser method and remove Blogit.models namespace.
                    await _likeService.LikePost(Like);

                    var returnPost = await CreatePostVM(postToLike.PostId);
                    return RedirectToAction("Index", new { returnPost.PostId });
                }
            }
        }


        [HttpGet]
        public IActionResult Advert()
        {
            return View();
        }
    }
}


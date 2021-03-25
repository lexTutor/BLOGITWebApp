using BLOGIT.Models;
using BLOGIT.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLOGIT.Commons.ObjectRelationalMappers
{
    /// <summary>
    /// Helper class to convert map views to view models and vice-versa
    /// </summary>
    public static class PostMappers
    {
        /// <summary>
        /// Maps a post entity to an OtherPostActivitiesPost view model.
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        public static OtherPostActivitiesPostVM ConvertToOtherPostVM(Post post)
        {
            OtherPostActivitiesPostVM viewModelPost = new OtherPostActivitiesPostVM
            {
                PostId = post.PostId,
                CommentCount = post.Comments.Count,
                PostCategories = string.Join(", ", ReturnCategories(post.PostCategories)),
                ImagePath = post.ImagePath,
                Likes = post.Likes.Count,
                PostCreatorName = post.PostCreator.FirstName + " " + post.PostCreator.LastName,
                PostDetails = post.PostDetails,
                PostTitle = post.PostTitle,
                ApprovalState = post.ApprovalState
            };
            return viewModelPost;
        }

        /// <summary>
        /// Maps an AddPost view model to a Post entity
        /// </summary>
        /// <param name="postToAdd"></param>
        /// <returns></returns>
        public static Post AddPostToVM(AddPostVM postToAdd)
        {
            Post post = new Post
            {
                PostCreator = postToAdd.PostCreator,
                PostDetails = postToAdd.PostDetails,
                PostTitle = postToAdd.PostTitle,
                CreationDate = postToAdd.Time,
            };
            post.PostCategories = ReturnPostCategories(postToAdd.CategoriesId, post.PostId);
            return post;
        }
        
        /// <summary>
        /// Creates an instance of a Like object.
        /// </summary>
        /// <param name="post"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static Likes AddLike(Post post, BlogUser user)
        {
            Likes like = new Likes
            {
                LikedPost = post,
                UserWhoLiked = user
            };
            return like;
        }

        /// <summary>
        /// Creates a list of postcategories from the categoriesId and postId
        /// </summary>
        /// <param name="categoriesId"></param>
        /// <param name="postId"></param>
        /// <returns></returns>
        public static List<PostCategories> ReturnPostCategories(string[] categoriesId, string postId)
        {
            List<PostCategories> postCategories = new List<PostCategories>();
            for(var i =0; i < categoriesId.Length; i++)
            {
                postCategories.Add(new PostCategories { PostId = postId, CategoryId = categoriesId[i] });
            }
            return postCategories;
        }

        /// <summary>
        /// Creates a comment from the comments view model
        /// </summary>
        /// <param name="commentToAdd"></param>
        /// <returns></returns>
        public static Comments MapCommentVMToComment(AddCommentVM commentToAdd)
        {
            Comments comment = new Comments
            {
                Post = commentToAdd.Post,
                CommentTime = commentToAdd.CommentTime,
                Comment = commentToAdd.Comment,
                UserFullName = commentToAdd.UserFullName,
                UserImage = commentToAdd.ImagePath
            };

            return comment;
        }

        /// <summary>
        /// Maps an IEnumerable of all posts to an all posts view model
        /// </summary>
        /// <param name="posts"></param>
        /// <returns></returns>
        public static AllEditorDataVM ReturnAllPosts(IEnumerable<Post> posts)
        {
            AllEditorDataVM allPosts = new AllEditorDataVM();
            List<PostsVM> allSinglePosts = new List<PostsVM>();
            foreach (var post in posts)
            {
                PostsVM PostsVM = new PostsVM
                {
                    PostCreatorName = post.PostCreator.FirstName + " " + post.PostCreator.LastName,
                    PostId = post.PostId,
                    PostTitle = post.PostTitle,
                    ImagePath = post.ImagePath,
                    CreationDate = post.CreationDate,
                    PostCategories = string.Join(", ", ReturnCategories(post.PostCategories)),
                    ApprovalState = post.ApprovalState,
                    
                };

                allSinglePosts.Add(PostsVM);
            }
            allPosts.AllSinglePosts = allSinglePosts;
            allPosts.PostCount = posts.Count();
            return allPosts;
        }
        
        /// <summary>
        /// Maps an IEnumerable of comments into a returnComments view model
        /// </summary>
        /// <param name="comments"></param>
        /// <returns></returns>
        private static IEnumerable<ReturnCommentsVm> ReturnComments(IEnumerable<Comments> comments)
        {
            if (comments == null)
            {
                return null;
            }
            List<ReturnCommentsVm> ListOfComments = new List<ReturnCommentsVm>();
            foreach (var comment in comments)
            {
                ReturnCommentsVm returnComment = new ReturnCommentsVm
                {
                    Comment = comment.Comment,
                    CommentTime = comment.CommentTime,
                    UserFullName = comment.UserFullName,
                    UserImage = comment.UserImage
                };
                ListOfComments.Add(returnComment);
            }
            return ListOfComments;
        }

        /// <summary>
        /// Returns the list of categories as an IEnunerable  of strings
        /// </summary>
        /// <param name="postCategories"></param>
        /// <returns></returns>
        private static IEnumerable<string> ReturnCategories(IEnumerable<PostCategories> postCategories)
        {
            List<string> Categories = new List<string>();
            if (postCategories != null)
            {
                foreach (var category in postCategories)
                {
                    Categories.Add(category.Category.CategoryName);
                }
            }
            return Categories;
        }

        /// <summary>
        /// Creates a returnPost view model from a single post entity and a list of posts
        /// </summary>
        /// <param name="post"></param>
        /// <param name="recentPosts"></param>
        /// <returns></returns>
        public static ReturnPostVM ReturnPosts(Post post, List<Post> recentPosts)
        {
            ReturnPostVM returnPost = new ReturnPostVM
            {
                PostTitle = post.PostTitle,
                PostCategories = string.Join(" ," , ReturnCategories(post.PostCategories)),
                PostCreator = post.PostCreator.FirstName + " " + post.PostCreator.LastName,
                PostDetails = post.PostDetails,
                ImagePath = post.ImagePath,
                commentsForThisPost = ReturnComments(post.Comments),
                CommentCount = post.Comments.Count,
                Likes = post.Likes.Count,
                PostId = post.PostId,
                MostRecentPosts = ReturnRecentPosts(recentPosts),
                CreationDate = post.CreationDate
            };
            return returnPost;
        }

        /// <summary>
        /// Returns a list of recent posts view model from a list of post entities
        /// </summary>
        /// <param name="recentPosts"></param>
        /// <returns></returns>
        private static List<RecentPostsVM> ReturnRecentPosts(List<Post> recentPosts)
        {
            List<RecentPostsVM> recentPostsVMs = new List<RecentPostsVM>();
            foreach (var post in recentPosts)
            {
            RecentPostsVM recent =   new RecentPostsVM 
                {
                    PostDetails = post.PostDetails,
                    PostId = post.PostId,
                    PostTitle = post.PostTitle,
                    CreationDate = post.CreationDate,
                    ImagePath = post.ImagePath
                };
                recentPostsVMs.Add(recent);
            }
            return recentPostsVMs;
        }

        /// <summary>
        /// Edits the post Details and title of a post and returns the post
        /// </summary>
        /// <param name="post"></param>
        /// <param name="editedPost"></param>
        /// <returns></returns>
        public static Post MapToEditedPostView(Post post, OtherPostActivitiesPostVM editedPost)
        {
            post.PostDetails = editedPost.PostDetails;
            post.PostTitle = editedPost.PostTitle;
            return post;
        }  

    }
}

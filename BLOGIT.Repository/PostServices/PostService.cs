using BLOGIT.DataAccess;
using BLOGIT.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLOGIT.Repository
{
    public class PostService : IPostService
    {
        private DataAccessContext _ctx { get; }
        public PostService(DataAccessContext ctx)
        {
            _ctx = ctx;
        }
        /// <summary>
        /// Sends an instance of a new post to the database
        /// </summary>
        /// <param name="post"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<bool> AddPost(Post post)
        {
            _ctx.Post.Add(post);
            return await _ctx.SaveChangesAsync() > 0;

        }

        /// <summary>
        /// Gets all the posts in the database
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Post>> GetAllPosts()
        {
            var allPosts = await _ctx.Post.Include(user => user.PostCreator).
                Include(postcatergory => postcatergory.PostCategories).
                ThenInclude(postCategory => postCategory.Category) .ToListAsync();
            return allPosts;
        }

        /// <summary>
        /// Gets a particular post in the database by the posts Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<Post> GetPost(string Id)
        {
            var post = await _ctx.Post.Where(Post => Post.PostId == Id).Include(post => post.Comments).OrderByDescending(postComment => postComment.CreationDate).Include(post => post.Likes).
                Include(user => user.PostCreator).Include(postcatergory => postcatergory.PostCategories).
                ThenInclude(postCategory => postCategory.Category).ToListAsync();
            
            return  post.First();
        }

     
        /// <summary>
        /// Gets the most recents posts from the database by ordering by the date of upload.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Post>> GetMostRecentPost()
        {
            var result = await _ctx.Post.Where(post=> post.ApprovalState == true).OrderByDescending(post => post.CreationDate).ToListAsync();
            if (result.Count >= 2) { return result.Take(2).ToList(); }
            else { return result; }
        }

        /// <summary>
        /// Changes the approval state of a post.
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        public async Task<bool> ApprovePost(Post post)
        {
            post.ApprovalState = post.ApprovalState != true;
            _ctx.Post.Update(post);
            return await _ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Removes an instance of a post from the database
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        public async Task<bool> DeletePost(Post post)
        {
            _ctx.Post.Remove(post);
            return await _ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Updates an instance of a post in the database
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        public async Task<bool> EditPost(Post post)
        {
            _ctx.Post.Update(post);
            return await _ctx.SaveChangesAsync() > 0;
        }
    }
}

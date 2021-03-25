using BLOGIT.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLOGIT.Repository
{
    public interface IPostService
    {
        Task<bool> AddPost(Post post);
        Task<bool> ApprovePost(Post post);
        Task<bool> DeletePost(Post post);
        Task<bool> EditPost(Post post);
        Task<IEnumerable<Post>> GetAllPosts();
        Task<Post> GetPost(string Id);
        Task<List<Post>> GetMostRecentPost();
    }
}
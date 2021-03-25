using BLOGIT.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLOGIT.Repository.LikeServices
{
    public interface ILikeService
    {
        Task<bool> LikePost(Likes like);
        Task<List<Likes>> GetAllLikes();
    }
}
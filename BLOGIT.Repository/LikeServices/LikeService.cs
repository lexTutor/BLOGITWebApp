using BLOGIT.DataAccess;
using BLOGIT.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLOGIT.Repository.LikeServices
{
    public class LikeService : ILikeService
    {
        private readonly DataAccessContext _ctx;
        public LikeService(DataAccessContext ctx)
        {
            _ctx = ctx;
        }

        /// <summary>
        /// Gets all the Likes inthe database
        /// </summary>
        /// <returns></returns>
        public async Task<List<Likes>> GetAllLikes()
        {
            return  await _ctx.Likes.Include(like=> like.LikedPost).Include(like=> like.LikedPost).ToListAsync();
        }

        /// <summary>
        /// Sends an instance of a liked post to the database
        /// </summary>
        /// <param name="like"></param>
        /// <returns></returns>
        public async Task<bool> LikePost(Likes like)
        {
            await _ctx.AddAsync(like);

            return await _ctx.SaveChangesAsync() > 0;
        }
    }
}

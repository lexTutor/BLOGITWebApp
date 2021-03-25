using BLOGIT.DataAccess;
using BLOGIT.Models;
using BLOGIT.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLOGIT.Repository.PostServices.CommentServices
{
    public class CommentService : ICommentService
    {
        private readonly DataAccessContext _ctx;
        public CommentService(DataAccessContext ctx)
        {
            _ctx = ctx;
        }

        /// <summary>
        /// sends an instance of a comment on a post to the database
        /// </summary>
        /// <param name="commment"></param>
        /// <returns></returns>
        public async Task<bool> AddComment(Comments commment)
        {
            _ctx.Comments.Add(commment);
            return await _ctx.SaveChangesAsync() > 0;

        }

        /// <summary>
        /// Deletes a comment from the comments table in the database
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        public async Task<bool> Deletecomment(Comments comment)
        {
            _ctx.Comments.Remove(comment);
            return await _ctx.SaveChangesAsync() > 0;
        }
    }
}

using BLOGIT.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLOGIT.Repository.PostServices.CommentServices
{
    public interface ICommentService
    {
        Task<bool> AddComment(Comments commment);
        Task<bool> Deletecomment(Comments comment);
    }
}
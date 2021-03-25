using BLOGIT.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLOGIT.Repository.CategoryServices
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetPostCategory();
        Task<string[]> GetGeneralCategory();
    }
}
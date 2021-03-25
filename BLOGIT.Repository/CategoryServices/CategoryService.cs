using BLOGIT.DataAccess;
using BLOGIT.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLOGIT.Repository.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private DataAccessContext _ctx { get; }
        public CategoryService(DataAccessContext ctx)
        {
            _ctx = ctx;
        }


        /// <summary>
        /// Gets all the categories that a post can belong to from the database
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Category>> GetPostCategory()
        {
            return await _ctx.Categories.ToListAsync();
        }

        /// <summary>
        /// Gets the general category for default category settings
        /// </summary>
        /// <returns></returns>
        public async Task<string[]> GetGeneralCategory()
        {
            var category = await _ctx.Categories.FirstOrDefaultAsync(category => category.CategoryName == "General");
            return new string[] { category.CategoryId };
        }
    }
}

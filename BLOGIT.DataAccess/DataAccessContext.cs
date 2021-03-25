using BLOGIT.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace BLOGIT.DataAccess
{
    /// <summary>
    /// DataAccesssContext class that implements the Identity DbContext class to enable the use of the Identity User in Entity Framework
    /// </summary>
    public class DataAccessContext: IdentityDbContext<BlogUser>
    {
        public DataAccessContext(DbContextOptions<DataAccessContext> options) : base(options)
        {
           
        }

        public DbSet<BlogUser> BlogUser { get; set; }

        public DbSet<Post> Post { get; set; }

        public DbSet<UserPostActivity> UserActivities { get; set; }

        public DbSet<UserActivityType> UserActivityTypes { get; set; }

        public DbSet<Comments> Comments { get; set; }

        public DbSet<Likes> Likes { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<PostCategories> PostCategories { get; set; }

        public DbSet<UserDataActivity> UserDataActivity { get; set; }

        // <summary>
        // Overrides the onMoelcreating to configure the many to many relationship between Category object and Post object
        //</summary>
        // <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<PostCategories>().HasKey(key => new { key.CategoryId, key.PostId });
            builder.Entity<PostCategories>().HasOne(postCartegory => postCartegory.Post).WithMany(postCategory => postCategory.PostCategories);
            builder.Entity<PostCategories>().HasOne(postCategory => postCategory.Category).WithMany(postCategory => postCategory.PostCategories);
        }

    }
}

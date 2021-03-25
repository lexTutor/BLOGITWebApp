using BLOGIT.DataAccess;
using BLOGIT.DataAccess.DataSeeding;
using BLOGIT.Models;
using BLOGIT.Repository;
using BLOGIT.Repository.CategoryServices;
using BLOGIT.Repository.LikeServices;
using BLOGIT.Repository.PostServices.CommentServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLOGIT.UserInterface
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<DataAccessContext>(options => options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<BlogUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;

            }

            ).AddEntityFrameworkStores<DataAccessContext>().AddDefaultTokenProviders();

            services.AddControllersWithViews(options=> {
              var policy =  new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                options.Filters.Add(new AuthorizeFilter(policy));
                }).AddRazorRuntimeCompilation();

            services.AddTransient<IPostService, PostService>();
            services.AddTransient<ILikeService, LikeService>();
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<IImageProcessorService, ImageProcessorService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
            services.AddTransient<IMailService, MailService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            DataAccessContext context, RoleManager<IdentityRole> roleManager, UserManager<BlogUser> userManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
                app.UseExceptionHandler("/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            DataSeeder.Preseeder(context, roleManager, userManager).Wait();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

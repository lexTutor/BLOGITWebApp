using BLOGIT.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLOGIT.DataAccess.DataSeeding
{
    public class DataSeeder
    {
        /// <summary>
        /// Creates the database if it does not exist
        /// Seeds data into the database if the database the database does not contain required data for startup
        /// The data is seeded from a json file and converted to the C# objectusing the newton.Json nuget package
        /// </summary>
        /// <param name="context"></param>
        /// <param name="roleManager"></param>
        /// <param name="userManager"></param>

        /// <returns></returns>

        public static async Task Preseeder( DataAccessContext context, RoleManager<IdentityRole> roleManager, UserManager<BlogUser> userManager)
        {
            try
            {
                List<string> list = new List<string>();

                context.Database.EnsureCreated();

                //Checks if there is data in the roles table and seeds data into it, if empty.
                if (!roleManager.Roles.Any())
                {
                    List<string> roles = new List<string> { "Admin", "ProEditor", "Editor", "Member" };
                    foreach (var item in roles)
                    {
                        var role = new IdentityRole(item);
                        await roleManager.CreateAsync(role);
                    }
                }

                //Checks if there is data in the BlogUser table and seeds data into it, if empty.
                if (!userManager.Users.Any())
                {
                    int count = 0;
                    string DataConvertedToString = File.ReadAllText("../BLOGIT.DataAccess/DataSeeding/UserSeed.json");
                    var blogUsers = JsonConvert.DeserializeObject<List<BlogUser>>(DataConvertedToString);
                    foreach (var user in blogUsers)
                    {
                        if (count == 0)
                        {
                            var result = await userManager.CreateAsync(user, "admin");
                            if (!result.Succeeded)
                            {
                                throw new Exception("Error");
                            }
                            await userManager.AddToRoleAsync(user, "Admin");
                             list.Add(user.Id);
                        }
                        if (count == 1)
                        {
                            var result = await userManager.CreateAsync(user, "proeditor");
                            if (!result.Succeeded)
                            {
                                throw new Exception("Error");
                            }
                            await userManager.AddToRoleAsync(user, "ProEditor");
                            list.Add(user.Id);
                        }
                        if (count == 2)
                        {
                            var result = await userManager.CreateAsync(user, "member");
                            if (!result.Succeeded)
                            {
                                throw new Exception("Error");
                            }
                            await userManager.AddToRoleAsync(user, "Member");
                            list.Add(user.Id);
                        }
                        if (count == 3)
                        {
                            var result = await userManager.CreateAsync(user, "proeditor");
                            if (!result.Succeeded)
                            {
                                throw new Exception("Error");
                            }
                            await userManager.AddToRoleAsync(user, "ProEditor");
                        }
                        count++;
                    }
                }


                //Checks if there is data in the Categories table and seeds data into it, If empty.
                if (!context.Categories.Any())
                {
                    string DataConvertedToString = File.ReadAllText("../BLOGIT.DataAccess/DataSeeding/Category.json");
                    var categories = JsonConvert.DeserializeObject<List<Category>>(DataConvertedToString);
                    foreach (var item in categories)
                    {
                        context.Categories.Add(item);
                    }
                    await context.SaveChangesAsync();
                }

                //Checks if there is data in the Post table
                if (!context.Post.Any())
                {
                    List<string> catergories = new List<string> { "Religion", "General", "LifeStyle", "Technology", "Architecture", "Adventure" };
                    int count = 0;
                    int count2 = 0;
                    string DataConvertedToString = File.ReadAllText("../BLOGIT.DataAccess/DataSeeding/PostSeed.json");
                    var subPosts = JsonConvert.DeserializeObject<List<Post>>(DataConvertedToString);
                    foreach (var item in subPosts)
                    {
                        Post post = new Post
                        {
                            PostTitle = item.PostTitle,
                            PostDetails = item.PostDetails,
                            PostCreator = context.BlogUser.FirstOrDefault(user => user.Id == list[count]),
                            ApprovalState = true
                        };
                        post.PostCategories = new List<PostCategories>
                        {
                            new PostCategories { Category = context.Categories.FirstOrDefault(Category => Category.CategoryName == catergories[count2]),
                                CategoryId= context.Categories.FirstOrDefault(Category => Category.CategoryName == catergories[count2]).CategoryId, PostId= post.PostId, Post = post },
                            new PostCategories { Category = context.Categories.FirstOrDefault(Category => Category.CategoryName == catergories[count2 + 1]), 
                                CategoryId= context.Categories.FirstOrDefault(Category => Category.CategoryName == catergories[count2 + 1]).CategoryId, PostId= post.PostId, Post = post }
                        };
                        count++;
                        count2 += 2;

                        context.Post.Add(post);
                    }
                    await context.SaveChangesAsync();
                }

            }
            catch (Exception )
            {
            }
        }
    }
}

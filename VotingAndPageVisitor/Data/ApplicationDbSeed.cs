using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using VotingAndPageVisitor.Models;

namespace VotingAndPageVisitor.Data
{
    public class ApplicationDbSeed
    {
        public static async Task SeedAsync(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user1 = new ApplicationUser { UserName = "User1", Email = "user1@gmail.com" };
                await userManager.CreateAsync(user1, "myuser1");

                var user2 = new ApplicationUser { UserName = "User2", Email = "user2@gmail.com" };
                await userManager.CreateAsync(user2, "myuser1");

                var user3 = new ApplicationUser { UserName = "User3", Email = "user3@gmail.com" };
                await userManager.CreateAsync(user3, "myuser3");

                if (!context.Posts.Any())
                {
                    var post1 = new Post()
                    {
                        AuthorId = user1.Id,
                        Title = "Post 1",
                        Body = "This is post 1"
                    };
                    var post2 = new Post()
                    {
                        AuthorId = user2.Id,
                        Title = "Post 2",
                        Body = "This is post 2"
                    };
                    var post3 = new Post()
                    {
                        AuthorId = user3.Id,
                        Title = "Post 3",
                        Body = "This is post 3"
                    };

                    await context.Posts.AddAsync(post1);
                    await context.Posts.AddAsync(post2);
                    await context.Posts.AddAsync(post3);

                    await context.SaveChangesAsync();
                }
            }

            
        }

        
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingAndPageVisitor.Data;
using VotingAndPageVisitor.Extensions;
using VotingAndPageVisitor.Models;

namespace VotingAndPageVisitor.api
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]/{id?}")]
    public class PostController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PostController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> GetPost(int id)
        {
            var post = _context.Posts
                .Include(p => p.Views)
                .FirstOrDefault(p => p.Id == id);

            if (post == null)
                return NotFound();

            var session = HttpContext.Session;
            if (!HttpContext.Session.Keys.Contains("History"))
            {
                HttpContext.Session.SetObjectAsJson("History", new List<int>());
            }
            List<int> visited = HttpContext.Session.GetObjectFromJson<List<int>>("History");
            if (!visited.Contains(id))
            {
                var view = new PostView()
                {
                    PostId = post.Id,
                    Date = DateTime.Now
                };

                await _context.PostViews.AddAsync(view);
                await _context.SaveChangesAsync();
                visited.Add(id);
                HttpContext.Session.SetObjectAsJson("History", visited);
            }
           
            return Ok(post);
        }
    }
}

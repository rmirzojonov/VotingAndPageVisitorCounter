using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VotingAndPageVisitor.Data;
using VotingAndPageVisitor.Models;
using VotingAndPageVisitor.Extensions;

namespace VotingAndPageVisitor.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var posts = _context.Posts.ToList();
            return View(posts);
        }

        public async Task<IActionResult> GetPost(int id)
        {
            var post = _context.Posts.Include(p => p.Author)
                .Include(p => p.Views)
                .Include(p => p.Votes)
                .FirstOrDefault(p => p.Id == id);
            
            if (post == null)
                return RedirectToAction(nameof(Index));

            var session = HttpContext.Session;
            if(!HttpContext.Session.Keys.Contains("History"))
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
            return View(post);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

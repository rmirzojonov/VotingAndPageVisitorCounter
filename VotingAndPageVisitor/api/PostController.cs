using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(HttpContext.User.Claims.Select(x => $"{x.Type} : {x.Value}").ToArray());
        }

        //public async Task<IActionResult> GetPost(int id)
        //{
        //    var post = _context.Posts
        //        .Include(p => p.Views)
        //        .FirstOrDefault(p => p.Id == id);

        //    if (post == null)
        //        return NotFound();
        //    var viewerId = User.FindFirst(ClaimTypes.Anonymous).Value;
        //    if(!post.Views.Any(p => p.ViewerId == viewerId))
        //    {
        //        var view = new PostView()
        //        {
        //            PostId = post.Id,
        //            ViewerId = viewerId,
        //            Date = DateTime.Now
        //        };

        //        await _context.PostViews.AddAsync(view);
        //        await _context.SaveChangesAsync();
        //    }
        //    //if (!HttpContext.Session.Keys.Contains("History"))
        //    //{
        //    //    HttpContext.Session.SetObjectAsJson("History", new List<int>());
        //    //}
        //    //List<int> visited = HttpContext.Session.GetObjectFromJson<List<int>>("History");
        //    //if (!visited.Contains(id))
        //    //{
        //    //    var view = new PostView()
        //    //    {
        //    //        PostId = post.Id,
        //    //        Date = DateTime.Now
        //    //    };

        //    //    await _context.PostViews.AddAsync(view);
        //    //    await _context.SaveChangesAsync();
        //    //    visited.Add(id);
        //    //    HttpContext.Session.SetObjectAsJson("History", visited);
        //    //}
           
        //    return Ok(post);
        //}
    }
}

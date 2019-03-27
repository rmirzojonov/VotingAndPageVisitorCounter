using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VotingAndPageVisitor.Data;
using VotingAndPageVisitor.Models;

namespace VotingAndPageVisitor.api
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]/{id?}")]
    public class VoteController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public VoteController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Get(int id)
        {
            var vote = await _context.Votes.FirstOrDefaultAsync(v => v.Id == id);

            if (vote == null)
            {
                return NotFound();
            }

            return Ok(vote);
        }

        [HttpPost]
        public async Task<IActionResult> Upvote(int id)
        {
            //if(!User.Identity.IsAuthenticated)
            //{
            //    return Unauthorized();
            //}
            var userId = _context.Users.Last().Id;

            var post = _context.Posts.FirstOrDefault(p => p.Id == id);
            if (post == null)
                return BadRequest(ModelState);
            var vote = await _context.Votes.FirstOrDefaultAsync(v => v.PostId == id && v.VoterId == userId);
            if (vote == null)
            {
                vote = new Vote()
                {
                    PostId = post.Id,
                    Value = 1,
                    VoterId = userId //User.FindFirst(ClaimTypes.NameIdentifier).Value
                };
                await _context.Votes.AddAsync(vote);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(Get),
                    new { id = vote.Id }, vote);
            }
            else
            {
                vote.Value = 1;
                _context.Update(vote);

                return Ok(vote);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Downvote(int id)
        {
            //if(!User.Identity.IsAuthenticated)
            //{
            //    return Unauthorized();
            //}
            var userId = _context.Users.Last().Id;

            var post = _context.Posts.FirstOrDefault(p => p.Id == id);
            if (post == null)
                return BadRequest(ModelState);
            var vote = await _context.Votes.FirstOrDefaultAsync(v => v.PostId == id && v.VoterId == userId);
            if (vote == null)
            {
                vote = new Vote()
                {
                    PostId = post.Id,
                    Value = -1,
                    VoterId = userId //User.FindFirst(ClaimTypes.NameIdentifier).Value
                };
                await _context.Votes.AddAsync(vote);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(Get),
                    new { id = vote.Id }, vote);
            }
            else
            {
                vote.Value = -1;
                _context.Update(vote);

                return Ok(vote);
            }
        }
    }
}

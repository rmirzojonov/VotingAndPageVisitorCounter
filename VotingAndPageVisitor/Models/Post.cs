using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using VotingAndPageVisitor.Models;

namespace VotingAndPageVisitor.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string AuthorId { get; set; }

        public string Title { get; set; }
        public string Body { get; set; }

        public ApplicationUser Author { get; set; }
        public ICollection<Vote> Votes { get; set; } 
        public ICollection<PostView> Views { get; set; }
    }
}

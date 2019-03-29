using Microsoft.AspNetCore.Identity;
using System;
using VotingAndPageVisitor.Models;

namespace VotingAndPageVisitor.Models
{
    public class PostView
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string ViewerId { get; set; }
        public DateTime Date { get; set; }

        public Post Post { get; set; }
    }
}

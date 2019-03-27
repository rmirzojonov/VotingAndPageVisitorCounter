using Microsoft.AspNetCore.Identity;
using VotingAndPageVisitor.Models;

namespace VotingAndPageVisitor.Models
{
    public class Vote
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string VoterId { get; set; }

        // Value can be 1 (upvote) or -1 (downvote)
        public int Value { get; set; }

        public Post Post { get; set; }
        public ApplicationUser Voter { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VotingAndPageVisitor.Models;

namespace VotingAndPageVisitor.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<PostView> PostViews { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Post>(ConfigurePosts);
            builder.Entity<Vote>(ConfigureVotes);
            builder.Entity<PostView>(ConfigureViews);

            base.OnModelCreating(builder);
        }

        private void ConfigurePosts(EntityTypeBuilder<Post> builder)
        {
            builder.HasOne(p => p.Author)
                .WithMany(a => a.Posts)
                .HasForeignKey(p => p.AuthorId);
        }

        private void ConfigureVotes(EntityTypeBuilder<Vote> builder)
        {
            builder.HasOne(p => p.Voter)
                .WithMany(a => a.Votes)
                .HasForeignKey(p => p.VoterId);

            builder.HasOne(p => p.Post)
                .WithMany(a => a.Votes)
                .HasForeignKey(p => p.PostId);
        }

        private void ConfigureViews(EntityTypeBuilder<PostView> builder)
        {
            builder.HasOne(p => p.Post)
                .WithMany(a => a.Views)
                .HasForeignKey(p => p.PostId);
        }
    }
}

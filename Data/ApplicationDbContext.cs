using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TwinMIME.Models;

namespace TwinMIME.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<BlogComment> BlogComments { get; set; }
        public DbSet<ArticleReaction> ArticleReactions { get; set; }
    }
}
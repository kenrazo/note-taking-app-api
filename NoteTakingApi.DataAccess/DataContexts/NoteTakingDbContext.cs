using Microsoft.EntityFrameworkCore;
using NoteTakingApi.DataAccess.Entities;

namespace NoteTakingApi.DataAccess.DataContexts
{
    public class NoteTakingDbContext : DbContext
    {
        public NoteTakingDbContext(DbContextOptions<NoteTakingDbContext> options) : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Note> Note { get; set; }
    }
}

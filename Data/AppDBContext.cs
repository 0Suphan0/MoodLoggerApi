using Microsoft.EntityFrameworkCore;
using MoodLoggerApi.Models;

namespace MoodLoggerApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<MoodLog> MoodLogs => Set<MoodLog>();

    }
}

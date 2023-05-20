using GlobalHandlerException.Models;
using Microsoft.EntityFrameworkCore;

namespace GlobalHandlerException.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}
        public DbSet<User> Users { get; set; }
    }
}

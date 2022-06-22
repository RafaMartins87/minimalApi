using Microsoft.EntityFrameworkCore;
using MiniApi.Model;

namespace MiniApi.Db
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Cliente>? Clientes { get; set; }
    }
}

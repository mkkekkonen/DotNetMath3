using DotNetMath3.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetMath3.API.DbContexts
{
    public class MathDataContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DbSet<Category> Categories { get; set; }

        public DbSet<Page> Pages { get; set; }

        public MathDataContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
    }
}
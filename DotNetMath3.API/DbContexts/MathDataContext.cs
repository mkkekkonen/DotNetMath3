using DotNetMath3.Shared.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DotNetMath3.API.DbContexts
{
    public class MathDataContext : IdentityUserContext<IdentityUser>
    {
        private readonly IConfiguration _configuration;

        public DbSet<Category> Categories { get; set; }

        public DbSet<Page> Pages { get; set; }

        public MathDataContext(DbContextOptions<MathDataContext> options, IConfiguration configuration) : base(options)
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
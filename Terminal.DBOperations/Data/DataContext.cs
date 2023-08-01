using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Terminal.Models;

namespace Terminal.DBOperations.Data
{
    public class DataContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public DataContext(DbContextOptions<DataContext> options,IConfiguration configuration) : base(options)
        {
           _configuration  = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_configuration.GetSection("ConnectionStrings:ConnectionString").Value, x => x.MigrationsAssembly("Terminal.DBOperations")); 
        }
        public DbSet<User> Users => Set<User>();
    }
}

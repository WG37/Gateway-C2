using Microsoft.EntityFrameworkCore;
using TeamServer.Domain.Entities.Agents;
using TeamServer.Domain.Entities.Listeners.HttpListeners;

namespace TeamServer.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<HttpListenerEntity> HttpListeners { get; set; }
        public DbSet<Agent> Agents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ModelConfig.ConfigureModel(modelBuilder);
        }
    }
}

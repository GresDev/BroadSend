using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.Extensions.Configuration;

namespace BroadSend.Server.Models
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration) : base(options)
        {
            if (bool.TryParse(configuration.GetSection("Deployment")?.Value, out bool deployment) && deployment)
            {
                Database.Migrate();
            }
        }

        public Exception DbContextException { get; }

        public DbSet<Director> Directors { get; set; }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Language> Languages { get; set; }

        public DbSet<Presenter> Presenters { get; set; }

        public DbSet<PresenterAlias> PresenterAliases { get; set; }

        public DbSet<Title> Titles { get; set; }

        public DbSet<TitleAlias> TitleAliases { get; set; }

        public DbSet<Composer> Composers { get; set; }

        public DbSet<Vendor> Vendors { get; set; }

        public DbSet<Broadcast> Broadcasts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Director>().HasIndex(d => d.Name).IsUnique();
            modelBuilder.Entity<Director>().HasIndex(d => d.Alias).IsUnique();
            modelBuilder.Entity<Country>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<Language>().HasIndex(l => l.Name).IsUnique();
            modelBuilder.Entity<Presenter>().HasIndex(p => p.Name).IsUnique();
            modelBuilder.Entity<PresenterAlias>().HasIndex(p => p.Alias).IsUnique();
            modelBuilder.Entity<Title>().HasIndex(t => t.Name).IsUnique();
            modelBuilder.Entity<TitleAlias>().HasIndex(t => t.Alias).IsUnique();
            modelBuilder.Entity<Composer>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<Vendor>().HasIndex(v => v.Name).IsUnique();
        }
    }
}
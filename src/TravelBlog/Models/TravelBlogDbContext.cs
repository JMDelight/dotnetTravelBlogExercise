using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TravelBlog.Models
{ 
    public class TravelBlogDbContext : DbContext
    {
        public DbSet<Location> Locations { get; set; }

        public DbSet<Person> People { get; set; }

        public DbSet<Experience> Experiences { get; set; }

        public DbSet<ExperiencePerson> ExperiencesPeople { get; set; }

        public TravelBlogDbContext(DbContextOptions<TravelBlogDbContext> options)
            : base(options)
        {
        }

        public TravelBlogDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=TravelBlog;integrated security=True");
        }
    }
}

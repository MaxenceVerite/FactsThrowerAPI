using FactsThrowingAPI.DAL.DAO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FactsThrowingAPI.DAL
{
    public class FactsContext : DbContext
    {

        public FactsContext(DbContextOptions<FactsContext> options) : base(options)
        {   
           
        }

        public DbSet<FactDAO> Facts { get; set; }

        public DbSet<Fact_TagDAO> Facts_Tags { get; set; }
        public DbSet<TagDAO> Tags { get; set; }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FactDAO>().HasKey(c => c.Id);
            modelBuilder.Entity<TagDAO>().HasKey(c => c.Id);
            modelBuilder.Entity<Fact_TagDAO>().HasKey(c => new { c.IdFact, c.IdTag });

            modelBuilder.Entity<FactDAO>().HasMany(c => c.Facts_Tags)
                                          .WithOne(c => c.Fact)
                                          .HasForeignKey(c => c.IdFact)
                                          .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<TagDAO>().HasMany(c => c.Tags_Fact)
                                          .WithOne(c => c.Tag)
                                          .HasForeignKey(c => c.IdTag)
                                          .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FactDAO>().HasData(
                new FactDAO()
                {
                    Id = Guid.NewGuid(),
                    Title = "tutu",
                    Content = "tata"
                });
            

        }
    }

}

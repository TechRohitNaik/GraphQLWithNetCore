using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQLAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphQLAPI.DBContext
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions option):base(option)
        {

        }
        public DbSet<Maker> Makers { get; set; }
        public DbSet<Car> Cars { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Maker>()
                .HasMany(p => p.Cars)
                .WithOne(p => p.Maker!)
                .HasForeignKey(p => p.MakerId);

            modelBuilder
                .Entity<Car>()
                .HasOne(p => p.Maker)
                .WithMany(p => p.Cars)
                .HasForeignKey(p => p.MakerId);
        }
    }
}

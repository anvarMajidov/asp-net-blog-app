using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingSystem.Models;

namespace VotingSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { }
        public DbSet<User> Users { get; set; }
        public DbSet<Article> Articles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(user => user.Role).HasDefaultValue("Member");
        }
    }
}

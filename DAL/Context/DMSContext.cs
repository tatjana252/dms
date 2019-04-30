using DAL.Configuration;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Context
{
    public class DMSContext : DbContext
    {
        public DbSet<Document> Documents{ get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
         // optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB; Database=Dmsms; Trusted_Connection=true;");
            optionsBuilder.UseInMemoryDatabase("miname");
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DocumentConfiguration());
            
        }
    }
}

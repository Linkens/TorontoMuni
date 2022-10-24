using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertCities.Data.Entities
{
    public class ECContext : DbContext
    {
        public DbSet<Building> Buildings { get; set; }
        public DbSet<ChoiceList> Choices { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseInMemoryDatabase(databaseName: "InMemoryDb");

            //optionsBuilder.UseSqlServer(ConnectionString);
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<SchemValue>().HasKey(s => new { s.Parameter_ID, s.Schema_ID });
            base.OnModelCreating(builder);
        }
    }
}

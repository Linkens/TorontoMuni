using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertCities.Data
{
    public class ECContext : IdentityDbContext<Workforce,Role, int>
    {
        public DbSet<Asset> Buildings { get; set; }
        public DbSet<ChoiceList> Choices { get; set; }
        public DbSet<Work> Works { get; set; }
        public DbSet<HostedFile> HostedFiles { get; set; }
        public DbSet<Workforce> Workforce { get; set; }
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

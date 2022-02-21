using EmirhanAvci.WebApi.Authentication;
using EmirhanAvci.WebApi.Models.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmirhanAvci.WebApi.Models
{
    public class Context: IdentityDbContext<User>
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
            //This Injection useful because we can write DefaultConnection to AppSettings.json so we can use the DefaultConnection on Startup.cs
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=DbWebApi;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
        public DbSet<Coin> Coins { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Network> Networks { get; set; }
    }
}

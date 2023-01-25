
using LexNews_Charlie.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using LexNews_Charlie.Models.ViewModels;

namespace LexNews_Charlie.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Email> Email { get; set; }
        public DbSet<Subscription> Subscription { get; set; }
        public DbSet<SubscriptionType> SubscriptionType { get; set; }
        public DbSet<LexNews_Charlie.Models.ViewModels.DisplayScoresVM> DisplayScoresVM { get; set; }
        //public DbSet<WeatherForecast> WeatherForecast { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarAgency.Models
{
    public class context : IdentityDbContext<applicationUser, IdentityRole<int>, int>

    {
        public context() : base()
        {
        }

        public context(DbContextOptions options) : base(options)
        {

        }

        public DbSet<applicationUser> applicationUsers { get; set; }
        public DbSet<cars> cars { get; set; }
        public DbSet<review> reviews { get; set; }
        public DbSet<classOfCar> classOfCars { get; set; }
        public DbSet<purchase> purchases { get; set; }  






    }
}

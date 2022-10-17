using Microsoft.EntityFrameworkCore;
using RealEstateAPI.Models.Domain;

namespace RealEstateAPI.Data
{
    public class RealEstateDataContext : DbContext
    {
        public RealEstateDataContext(DbContextOptions<RealEstateDataContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User_Role>()
                .HasOne(x => x.Role)
                .WithMany(y => y.UserRoles)
                .HasForeignKey(x => x.RoleId);

            modelBuilder.Entity<User_Role>()
                .HasOne(x => x.User)
                .WithMany(y => y.UserRoles)
                .HasForeignKey(x => x.UserId);
        }

        public DbSet<Region> Regions { get; set; }
        public DbSet<House> Houses { get; set; }
        public DbSet<Landscape> Landscapes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User_Role> Users_Roles { get; set; }
    }
}

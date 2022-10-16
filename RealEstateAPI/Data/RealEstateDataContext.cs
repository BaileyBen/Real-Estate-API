using Microsoft.EntityFrameworkCore;
using RealEstateAPI.Models.Domain;

namespace RealEstateAPI.Data
{
    public class RealEstateDataContext : DbContext
    {
        public RealEstateDataContext(DbContextOptions<RealEstateDataContext> options) : base(options)
        {

        }
        public DbSet<Region> Regions { get; set; }
        public DbSet<House> Houses { get; set; }
        public DbSet<Landscape> Landscapes { get; set; }
    }
}

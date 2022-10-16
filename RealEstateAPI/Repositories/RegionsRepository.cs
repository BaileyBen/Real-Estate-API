using Microsoft.EntityFrameworkCore;
using RealEstateAPI.Data;
using RealEstateAPI.Models.Domain;

namespace RealEstateAPI.Repositories
{
    public class RegionsRepository : IRegionsRepository
    {
        private readonly RealEstateDataContext _context;

        public RegionsRepository(RealEstateDataContext context)
        {
            _context = context;
        }

        public async Task<Region> AddAsync(Region region)
        {
            region.Id = Guid.NewGuid();
            await _context.AddAsync(region);

            _context.SaveChanges();

            return region;
        }

        public async Task<Region> DeleteAsync(Guid id)
        {
            var region = await _context.Regions.FirstOrDefaultAsync(x => x.Id == id);


            if (region == null)
            {
                return null;
            }

            _context.Regions.Remove(region);
            _context.SaveChangesAsync();

            return region;
        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await _context.Regions.ToListAsync();
        }

        public async Task<Region> GetAsync(Guid id)
        {
            return await _context.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region> UpdateASync(Guid id, Region region)
        {
            var existingRegion = await _context.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (existingRegion == null)
            {
                return null;
            }

            existingRegion.State = region.State;
            existingRegion.Code = region.Code;
            existingRegion.City = region.City;
            existingRegion.Population = region.Population;

            await _context.SaveChangesAsync();

            return existingRegion;
        }
    }
}

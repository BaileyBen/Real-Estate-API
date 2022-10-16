using Microsoft.EntityFrameworkCore;
using RealEstateAPI.Data;
using RealEstateAPI.Models.Domain;

namespace RealEstateAPI.Repositories
{
    public class LandscapeRepository : ILandscapesRepository
    {
        private readonly RealEstateDataContext _context;

        public LandscapeRepository(RealEstateDataContext context)
        {
            _context = context;
        }

        public async Task<Landscape> AddAsync(Landscape landscape)
        {
            landscape.Id = Guid.NewGuid();
            await _context.Landscapes.AddAsync(landscape);
            await _context.SaveChangesAsync();

            return landscape;
        }

        public async Task<Landscape> DeleteAsync(Guid id)
        {
            var existingLandscape = await _context.Landscapes.FindAsync(id);

            if (existingLandscape != null)
            {
                _context.Landscapes.Remove(existingLandscape);
                _context.SaveChangesAsync();
                return existingLandscape;
            }
            return null;
        }

        public async Task<IEnumerable<Landscape>> GetAllAsync()
        {
            return await _context.Landscapes.ToListAsync();
        }

        public async Task<Landscape> GetAsync(Guid id)
        {
            return await _context.Landscapes.FirstOrDefaultAsync();
        }

        public async Task<Landscape> UpdateAsync(Guid id, Landscape landscape)
        {
            var existingLandscape = await _context.Landscapes.FindAsync(id);

            if (existingLandscape == null)
            {
                return null;
            }

            existingLandscape.Type = landscape.Type;

            await _context.SaveChangesAsync();
            return existingLandscape;
        }
    }
}

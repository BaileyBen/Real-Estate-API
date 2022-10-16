using Microsoft.EntityFrameworkCore;
using RealEstateAPI.Data;
using RealEstateAPI.Models.Domain;

namespace RealEstateAPI.Repositories
{
    public class HousesRepository : IHousesRepository
    {
        private readonly RealEstateDataContext _context;

        public HousesRepository(RealEstateDataContext context)
        {
            _context = context;
        }

        public async Task<House> AddAsync(House house)
        {
            house.Id = Guid.NewGuid();

            await _context.Houses.AddAsync(house);
            await _context.SaveChangesAsync();

            return house;
        }

        public async Task<House> DeleteAsync(Guid id)
        {
            var existingHouse = await _context.Houses.FindAsync(id);

            if (existingHouse == null)
            {
                return null;
            }

            _context.Houses.Remove(existingHouse);
            _context.SaveChangesAsync();

            return existingHouse;
        }

        public async Task<IEnumerable<House>> GetAllAsync()
        {
            return await _context.Houses
                .Include(x => x.Region)
                .Include(x => x.Landscape)
                .ToListAsync();
        }

        public async Task<House> GetAsync(Guid id)
        {
            return await _context.Houses
                .Include(x => x.Region)
                .Include(x => x.Landscape)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<House> UpdateAsync(Guid id, House house)
        {
            var existingHouse = await _context.Houses.FindAsync(id);

            if (existingHouse != null)
            {
                existingHouse.Price = house.Price;
                existingHouse.Status = house.Status;
                existingHouse.Suburb = house.Suburb;
                existingHouse.Postcode = house.Postcode;
                existingHouse.Bedroom = house.Bedroom;
                existingHouse.Bathroom = house.Bathroom;
                existingHouse.Room = house.Room;
                existingHouse.Shed = house.Shed;
                existingHouse.SqMeter = house.SqMeter;
                existingHouse.Text = house.Text;
                existingHouse.RegionId = house.RegionId;
                existingHouse.LandscapeId = house.LandscapeId;

                return existingHouse;
            }

            return null;
        }
    }
}

using RealEstateAPI.Models.Domain;

namespace RealEstateAPI.Repositories
{
    public interface IHousesRepository
    {
        Task<IEnumerable<House>> GetAllAsync();
        Task<House> GetAsync(Guid id);
        Task<House> AddAsync(House house);
        Task<House> UpdateAsync(Guid id, House house);
        Task<House> DeleteAsync(Guid id);
    }
}

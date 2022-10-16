using RealEstateAPI.Models.Domain;

namespace RealEstateAPI.Repositories
{
    public interface IRegionsRepository
    {
        Task<IEnumerable<Region>> GetAllAsync();
        Task<Region> GetAsync(Guid id);
        Task<Region> AddAsync(Region region);
        Task<Region> DeleteAsync(Guid id);
        Task<Region> UpdateASync(Guid id, Region region);   
    }
}

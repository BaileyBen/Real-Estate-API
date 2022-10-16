using RealEstateAPI.Models.Domain;

namespace RealEstateAPI.Repositories
{
    public interface ILandscapesRepository
    {
        Task<IEnumerable<Landscape>> GetAllAsync();
        Task<Landscape> GetAsync(Guid id);
        Task<Landscape> AddAsync(Landscape landscape);
        Task<Landscape> UpdateAsync(Guid id, Landscape landscape);
        Task<Landscape> DeleteAsync(Guid id);
    }
}

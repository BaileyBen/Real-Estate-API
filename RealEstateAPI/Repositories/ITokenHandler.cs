using RealEstateAPI.Models.Domain;

namespace RealEstateAPI.Repositories
{
    public interface ITokenHandler
    {
        Task<string> CreateTokenAsync(User user);
    }
}

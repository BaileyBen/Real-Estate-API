using Microsoft.IdentityModel.Tokens;
using RealEstateAPI.Models.Domain;

namespace RealEstateAPI.Repositories
{
    public interface IUserRepository
    {
        Task<User> AuthenticateAsync(string username, string password);
    }
}

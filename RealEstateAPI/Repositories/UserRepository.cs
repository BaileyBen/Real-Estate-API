using Microsoft.EntityFrameworkCore;
using RealEstateAPI.Data;
using RealEstateAPI.Models.Domain;

namespace RealEstateAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly RealEstateDataContext _context;

        public UserRepository(RealEstateDataContext context)
        {
            _context = context;
        }
        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Username.ToLower() == username.ToLower() && x.Password == password);
        
            if (user == null)
            {
                return null;
            }

            var userRoles = await _context.Users_Roles.Where(x => x.UserId == user.Id).ToListAsync();

            if (userRoles.Any())
            {
                user.Roles = new List<string>();
                foreach (var userRole in userRoles)
                {
                    var role = await _context.Users_Roles.FirstOrDefaultAsync(x => x.Id == userRole.RoleId);
                    if (role != null)
                    {
                        user.Roles.Add(role.Name);
                    }
                }
            }
            user.Password = null;
            return user;
        }
    }
}

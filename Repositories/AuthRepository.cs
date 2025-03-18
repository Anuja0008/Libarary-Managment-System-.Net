using Microsoft.EntityFrameworkCore;
using YourProject.Data;
using YourProject.Models;

namespace YourProject.Repositories
{
    public class AuthRepository
    {
        private readonly AppDbContext _context;

        public AuthRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> Register(User user, string password)
        {
            // Store the plain-text password directly
            user.Password = password; // No hashing
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> Login(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == username);
            if (user == null || user.Password != password) // Compare plain text password
                return null;

            return user;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using YourProject.Data;
using YourProject.Models;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace YourProject.Repositories
{
    public class MemberRepository
    {
        private readonly AppDbContext _context;

        public MemberRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Member> RegisterMember(Member member, string password)
        {
            // Hash the password before saving it
            member.Password = HashPassword(password);

            // Set Registration Date to now
            member.RegistrationDate = DateTime.Now;

            // Save the member to the database
            _context.Members.Add(member);
            await _context.SaveChangesAsync();
            return member;
        }

        private string HashPassword(string password)
        {
            using var hmac = new HMACSHA256();
            return Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
        }
    }
}

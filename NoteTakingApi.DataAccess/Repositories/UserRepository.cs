using Microsoft.EntityFrameworkCore;
using NoteTakingApi.DataAccess.DataContexts;
using NoteTakingApi.DataAccess.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace NoteTakingApi.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly NoteTakingDbContext _context;

        public UserRepository(NoteTakingDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetByUsername(string username)
        {
            return await _context.User
                .Where(m => m.Username == username)
                .FirstOrDefaultAsync();
        }

        public async Task<User> Insert(User user)
        {
            await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

    }
}

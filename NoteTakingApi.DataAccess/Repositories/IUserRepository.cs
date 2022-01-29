using NoteTakingApi.DataAccess.Entities;
using System.Threading.Tasks;

namespace NoteTakingApi.DataAccess.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByUsername(string username);
        Task<User> Insert(User user);
    }
}

using System.Threading.Tasks;
using TransactionsManager.DAL.Models;

namespace TransactionsManager.Services
{
    public interface IAuthService
    {
        public Task<bool> Register(User person);
        public string GenerateToken(string login);
        public Task<User> GetUser(User user);
    }

}

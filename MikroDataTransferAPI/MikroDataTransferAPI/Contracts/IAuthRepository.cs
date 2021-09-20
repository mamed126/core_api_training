using MikroDataTransferAPI.Model;
using System.Threading.Tasks;

namespace MikroDataTransferAPI.Contracts
{
    public interface IAuthRepository
    {
        User Login(string userName, string password);
        Task<User> Register(User user, string password);
        Task<bool> UserExists(string userName);
    }
}

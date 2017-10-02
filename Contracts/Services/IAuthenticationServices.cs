using System.Threading.Tasks;
using Models.Auth;
using Models.Entities;
using Models.Enums;

namespace Contracts.Services
{
    public interface IAuthenticationServices
    {
        Task<RegistrationStatus> RegisterUser(string email, string password);

        Task<User> LoginAttempt(LoginModel model);
    }
}
using System.Threading.Tasks;
using Models.Enums;

namespace Contracts.Services
{
    public interface IAuthenticationServices
    {
        Task<RegistrationStatus> RegisterUser(string email, string password);
    }
}
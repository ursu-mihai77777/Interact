using Microsoft.AspNetCore.Identity;
using ThirdDelivery.Models;

namespace ThirdDelivery.Services
{
    public interface IAuthService
    {
        Task<SignInResult> LoginAsync(string email, string password);
        Task<IdentityResult> RegisterAsync(User user, string password);
    }

}

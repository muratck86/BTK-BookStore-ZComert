using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Identity;

namespace Services.Contracts
{
    public interface IAuthenticationService
    {
        Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistrationDto);
        Task<bool> ValidateUser(UserForAuthenticaionDto userForAuthenticaionDto);
        Task<string> CreateToken();
    }
}

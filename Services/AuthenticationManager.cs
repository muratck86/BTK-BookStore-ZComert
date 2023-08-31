using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services
{
    public class AuthenticationManager : IAuthenticationService
    {
        private readonly ILoggerService _loggerService;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private User? _user;

        public AuthenticationManager(
            ILoggerService loggerService,
            IMapper mapper,
            UserManager<User> userManager,
            IConfiguration configuration)
        {
            _loggerService = loggerService;
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistrationDto)
        {
            var user = _mapper.Map<User>(userForRegistrationDto);

            var result = await _userManager
                .CreateAsync(user, userForRegistrationDto.Password);
            if(result.Succeeded)
            {
                await _userManager.AddToRolesAsync(user, userForRegistrationDto.Roles);
            }
            return result;
        }

        public async Task<bool> ValidateUser(UserForAuthenticaionDto userForAuthenticaionDto)
        {
            _user = await _userManager.FindByNameAsync(userForAuthenticaionDto.UserName);
            var result = (_user is not null 
                && await _userManager.CheckPasswordAsync(_user, userForAuthenticaionDto.Password));

            if(!result)
            {
                _loggerService.LogWarning($"{nameof(ValidateUser)} : Authentication falied. Wrong User Name or Password");
            }
            return result;
        }

        public async Task<string> CreateToken()
        {
            var SignInCredentials = GetSignInCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(SignInCredentials, claims);
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        private SigningCredentials GetSignInCredentials()
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = Encoding.UTF8.GetBytes(jwtSettings["secretKey"]);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims()
        {
            var claim = new Claim(ClaimTypes.Name, _user.UserName);
            var claims = new List<Claim> { claim };

            var roles = await _userManager
                .GetRolesAsync(_user);

            foreach ( var role in roles )
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signInCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var tokenOptions = new JwtSecurityToken(
                issuer: jwtSettings["validIssuer"],
                audience: jwtSettings["validAudience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
                signingCredentials: signInCredentials);

            return tokenOptions;
        }
    }
}

using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/authentication")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IServiceManager _services;

        public AuthenticationController(IServiceManager services)
        {
            _services = services;
        }

        [HttpPost("register")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistrationDto)
        {
            var service = _services.AuthenticationService;
            var result = await service.RegisterUser(userForRegistrationDto);
            if(!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }

            return StatusCode(201);
        }

        [HttpPost("login")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticaionDto userDto)
        {
            if(!await _services.AuthenticationService.ValidateUser(userDto))
                return Unauthorized();

            var tokenDto = await _services.AuthenticationService.CreateToken(populateExp: true);
            return Ok(tokenDto);
        }

        [HttpPost("refresh")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Refresh([FromBody] TokenDto tokenDto)
        {
            var tokenDtoToReturn = await _services
                .AuthenticationService
                .RefreshToken(tokenDto);

            return Ok(tokenDtoToReturn);
        }
    }
}

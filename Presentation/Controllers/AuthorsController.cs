using Entities.DataTransferObjects;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;
using System.Text.Json;

namespace Presentation.Controllers
{
    [ApiExplorerSettings(GroupName = "v1")]
    [ServiceFilter(typeof(LogFilterAttribute))]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IServiceManager _services;

        public AuthorsController(IServiceManager services)
        {
            _services = services;
        }

        [HttpGet]
        [HttpHead]
        [ValidateMediaType]
        public async Task<IActionResult> GetAllAuthorsAsync([FromQuery] AuthorParameters authorParameters)
        {
            var linkParameters = new AuthorLinkParameters
            {
                AuthorParameters = authorParameters,
                HttpContext = HttpContext
            };

            var result = await _services.AuthorService.GetAllAuthorsAsync(linkParameters);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(result.metaData));

            return result.linkResponse.HasLinks
                ? Ok(result.linkResponse.LinkedEntities)
                : Ok(result.linkResponse.ShapedEntities);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOneAuthorAsync([FromRoute(Name ="id")] int id)
        {
            var author = await _services.AuthorService.GetOneAuthorByIdAsync(id);
            return Ok(author);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Editor")]
        [ValidationFilter]
        public async Task<IActionResult> CreateOneAuthorAsync([FromBody] AuthorCreateDto author)
        {
            var result = await _services.AuthorService.CreateOneAuthorAsync(author);
            return StatusCode(201,result);
        }

        [Authorize(Roles = "Admin,Editor")]
        [ValidationFilter]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateOneAuthorAsync([FromRoute(Name ="id")] int id, [FromBody] AuthorUpdateDto author)
        {
            await _services.AuthorService.UpdateOneAuthorAsync(id,author,true);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOneAuthorAsync([FromRoute(Name = "id")] int id)
        {
            await _services.AuthorService.DeleteOneAuthorAsync(id);
            return Ok();
        }

        [Authorize]
        [HttpOptions]
        public IActionResult GetAuthorsOptions()
        {
            Response.Headers.Add("Allow", "GET, PUT, POST, DELETE, HEAD, OPTIONS");
            return Ok();
        }
    }
}

using Entities.DataTransferObjects;
using Entities.RequestFeatures;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;
using System.Text.Json;

namespace Presentation.Controllers
{
    //[ApiVersion("1.0")]
    [ServiceFilter(typeof(LogFilterAttribute))]
    [Route("api/[controller]")]
    [ApiController]
    //[ResponseCache(CacheProfileName ="5mins")]
    public class BooksController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public BooksController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [Authorize]
        [HttpHead]
        [HttpGet(Name = "GetAllBooksAsync")]
        [ValidateMediaType]
        //[ResponseCache(Duration = 60)]
        //[HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 90)]
        public async Task<IActionResult> GetAllBooksAsync([FromQuery]BookParameters bookParameters)
        {
            var linkParameters = new BookLinkParameters()
            {
                BookParameters = bookParameters,
                HttpContext = HttpContext
            };

            var result = await _serviceManager
                .BookService
                .GetAllBooksAsync(linkParameters,false);

            Response.Headers.Add(
                "X-Pagination",
                JsonSerializer.Serialize(result.metaData));

            return result.linkResponse.HasLinks
                ? Ok(result.linkResponse.LinkedEntities)
                : Ok(result.linkResponse.ShapedEntities);

        }

        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOneBookAsync([FromRoute(Name = "id")] int id)
        {
            var book = await _serviceManager.BookService.GetOneBookByIdAsync(id, false);
            return Ok(book); //200
        }

        [Authorize(Roles = "Admin,Editor")]
        [ValidationFilter] // alternative 1
        [HttpPost(Name = "CreateOneBookAsync")]
        public async Task<IActionResult> CreateOneBookAsync([FromBody] BookCreateDto bookDto)
        {
            var result = await _serviceManager.BookService.CreateOneBookAsync(bookDto);
            return StatusCode(201, result);
        }

        [Authorize(Roles = "Admin,Editor")]
        [ServiceFilter(typeof(ValidationFilterAttribute))] //alternative 2
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] BookUpdateDto bookDto)
        {
            await _serviceManager.BookService.UpdateOneBookAsync(id, bookDto, true);
            return Ok(bookDto); //200
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOneBookAsync([FromRoute(Name = "id")] int id)
        {
            await _serviceManager.BookService.DeleteOneBookAsync(id, false);

            return Ok(); //200
        }

        [Authorize(Roles = "Admin,Editor")]
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> PatchOneBookAsync([FromRoute(Name = "id")] int id, [FromBody] JsonPatchDocument<BookUpdateDto> bookPatch)
        {
            if (bookPatch is null)
                return BadRequest("Patch object cannot be null.");
            var result = await _serviceManager.BookService.GetOneBookForPatchAsync(id, false);
            bookPatch.ApplyTo(result.bookUpdateDto, ModelState);
            TryValidateModel(result.bookUpdateDto);
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }
            await _serviceManager.BookService.UpdateOneBookAsync(id, result.bookUpdateDto);
            return Ok(result.bookUpdateDto);
        }

        [Authorize]
        [HttpOptions]
        public IActionResult GetBooksOptions()
        {
            Response.Headers.Add("Allow", "GET, PUT, POST, PATCH, DELETE, HEAD, OPTIONS");
            return Ok();
        }

        [HttpGet("details")]
        [Authorize]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _serviceManager.BookService.GetAllBooksAsync(false);
            return Ok(result);
        }
    }
}
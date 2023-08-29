using Entities.DataTransferObjects;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;
using System.Text.Json;

namespace Presentation.Controllers
{
    [ServiceFilter(typeof(LogFilterAttribute))]
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public BooksController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpHead]
        [HttpGet]
        [ValidateMediaType]
        public async Task<IActionResult> GetAllBooksAsync([FromQuery]BookParameters bookParameters)
        {
            var linkParameters = new LinkParameters()
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

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOneBookAsync([FromRoute(Name = "id")] int id)
        {
            var book = await _serviceManager.BookService.GetOneBookByIdAsync(id, false);
            return Ok(book); //200
        }

        [ValidationFilter] // alternative 1
        [HttpPost]
        public async Task<IActionResult> CreateOneBookAsync([FromBody] BookCreateDto bookDto)
        {
            var result = await _serviceManager.BookService.CreateOneBookAsync(bookDto);
            return StatusCode(201, result);
        }

        [ServiceFilter(typeof(ValidationFilterAttribute))] //alternative 2
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] BookUpdateDto bookDto)
        {
            await _serviceManager.BookService.UpdateOneBookAsync(id, bookDto, true);
            return Ok(bookDto); //200
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOneBookAsync([FromRoute(Name = "id")] int id)
        {
            await _serviceManager.BookService.DeleteOneBookAsync(id, false);

            return Ok(); //200
        }

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

        [HttpOptions]
        public IActionResult GetBooksOptions()
        {
            Response.Headers.Add("Allow", "GET, PUT, POST, PATCH, DELETE, HEAD, OPTIONS");
            return Ok();
        }
    }
}

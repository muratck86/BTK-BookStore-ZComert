using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Presentation.Controllers
{
    //[ApiVersion("2.0", Deprecated = true)]
    [ApiController]
    [Route("api/books")]
    [ApiExplorerSettings(GroupName = "v2")]
    public class BooksV2Controller : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public BooksV2Controller(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooksAsync()
        {
            var books = await _serviceManager.BookService.GetAllBooksAsync(false);
            var booksV2 = books.Select(b => new
            {
                Id = b.Id,
                Title = b.Title

            });
            return Ok(booksV2);
        }
    }
}

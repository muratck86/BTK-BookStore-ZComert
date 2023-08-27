using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private IServiceManager _serviceManager;

        public BooksController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public ActionResult GetAllBooks()
        {
            var result = _serviceManager.BookService.GetAllBooks(false);
            return Ok(result);

        }

        [HttpGet("{id:int}")]
        public IActionResult GetOneBook([FromRoute(Name = "id")] int id)
        {
            var book = _serviceManager.BookService.GetOneBookById(id, false);
            return Ok(book); //200
        }

        [HttpPost]
        public IActionResult CreateOneBook([FromBody] Book book)
        {
            if (book is null)
                throw new BadRequestException("No book object provided to create.");
            if (book.Id > 0)
                throw new BadRequestException("Id can not be given for Create operation.");

            _serviceManager.BookService.CreateOneBook(book);
            return StatusCode(201, book);
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] Book book)
        {
            if (book is null)
                throw new BadRequestException("No book object provided for update.");
            if (id != book.Id)
                throw new BadRequestException("You can not change id!"); //400

            _serviceManager.BookService.UpdateOneBook(id, book, true);

            return Ok(book); //200
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteABook([FromRoute(Name = "id")] int id)
        {
            _serviceManager.BookService.DeleteOneBook(id, false);

            return Ok(); //200
        }

        [HttpPatch("{id:int}")]
        public IActionResult PatchABook(
            [FromRoute(Name = "id")] int id,
            [FromBody] JsonPatchDocument<Book> bookPatch)
        {
            var book = _serviceManager.BookService.GetOneBookById(id, true);

            bookPatch.ApplyTo(book);
            _serviceManager.BookService.UpdateOneBook(id, book);
            return Ok(book);
        }

    }
}

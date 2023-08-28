using AutoMapper;
using Entities.DataTransferObjects;
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
        private readonly IServiceManager _serviceManager;

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
        public IActionResult CreateOneBook([FromBody] BookCreateDto bookDto)
        {
            if (bookDto is null)
                throw new BadRequestException("No book object provided to create.");

            if(!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            var result = _serviceManager.BookService.CreateOneBook(bookDto);
            return StatusCode(201, result);
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] BookUpdateDto bookDto)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            if (id != bookDto.Id)
                throw new BadRequestException("You can not change id!"); //400

            _serviceManager.BookService.UpdateOneBook(id, bookDto, true);

            return Ok(bookDto); //200
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneBook([FromRoute(Name = "id")] int id)
        {
            _serviceManager.BookService.DeleteOneBook(id, false);

            return Ok(); //200
        }

        [HttpPatch("{id:int}")]
        public IActionResult PatchOneBook( //temporary solution for Dtos
            [FromRoute(Name = "id")] int id,
            [FromBody] JsonPatchDocument<BookUpdateDto> bookPatch)
        {
            if (bookPatch is null)
                return BadRequest("Patch object cannot be null.");

            var result = _serviceManager.BookService.GetOneBookForPatch(id, false);

            bookPatch.ApplyTo(result.bookUpdateDto, ModelState);

            TryValidateModel(result.bookUpdateDto);

            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            _serviceManager.BookService.UpdateOneBook(id, result.bookUpdateDto);
            return Ok(result.bookUpdateDto);
        }

    }
}

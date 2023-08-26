using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            try
            {
                var result = _serviceManager.BookService.GetAllBooks(false);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult GetOneBook([FromRoute(Name = "id")] int id)
        {
            try
            {
                var book = _serviceManager.BookService.GetOneBookById(id, false);
                return book is not null
                    ? Ok(book) //200
                    : NotFound(); //404
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateOneBook([FromBody] Book book)
        {
            try
            {
                _serviceManager.BookService.CreateOneBook(book);
                return StatusCode(201, book);
                //return Created($"/{book.Id}", book);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] Book book)
        {
            if (id != book.Id)
                return BadRequest("You can not change id!"); //400

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
            if (book is null)
                return NotFound($"No such book (id: {id})"); //404

            bookPatch.ApplyTo(book);
            _serviceManager.BookService.UpdateOneBook(id, book);
            return Ok(book);
        }

    }
}

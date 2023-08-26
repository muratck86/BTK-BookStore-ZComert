using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Repositories.EfCore;
using System.Linq;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly RepositoryContext _context;

        public BooksController(RepositoryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult GetAllBooks()
        {
            try
            {
                var result = _context.Books.ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult GetABook([FromRoute(Name = "id")] int id)
        {
            try
            {
                var book = GetBook(id);
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
        public IActionResult CreateABook([FromBody] Book book)
        {
            try
            {
                if (book is null || book.Id > 0)
                    return BadRequest(); //400

                _context.Books.Add(book);
                _context.SaveChanges();

                return StatusCode(201, book);
                //return Created($"/{book.Id}", book);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateABook([FromRoute(Name = "id")] int id, [FromBody] Book book)
        {
            var result = GetBook(id);
            if (result is null)
                return NotFound(); //404

            result.Title = book.Title;
            result.Price = book.Price;

            //_context.Books.Update(result);
            _context.SaveChanges();

            if (id != book.Id)
                return BadRequest("You can not change id!"); //400

            return Ok(result); //200
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteABook([FromRoute(Name = "id")] int id)
        {
            var book = GetBook(id);
            if (book is null)
                return NotFound($"No such book (id: {id})"); //404

            _context.Books.Remove(book);
            _context.SaveChanges();

            //return NoContent(); //204
            return Ok(book); //200
        }

        [HttpPatch("{id:int}")]
        public IActionResult PatchABook(
            [FromRoute(Name = "id")] int id,
            [FromBody] JsonPatchDocument<Book> bookPatch)
        {
            var book = GetBook(id);
            if (book is null)
                return NotFound($"No such book (id: {id})"); //404

            bookPatch.ApplyTo(book);

            return Ok(book);
        }

        private Book? GetBook(int id)
        {
            return _context.Books.SingleOrDefault(b => b.Id.Equals(id));
        }
    }
}

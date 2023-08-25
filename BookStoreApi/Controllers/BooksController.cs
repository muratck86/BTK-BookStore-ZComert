using BookStoreApi.Data;
using BookStoreApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var books = ApplicationContext.Books;
            return Ok(books);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get([FromRoute(Name ="id")] int id)
        {
            var book = GetBook(id);
            return book is not null 
                ? Ok(book) //200
                : NotFound(); //404
        }

        [HttpPost]
        public IActionResult Create([FromBody] Book book)
        {
            try
            {
                if (book is null)
                    return BadRequest(); //400
                ApplicationContext.Books.Add(book);
                return StatusCode(201, book);
                //return Created($"/{book.Id}", book);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult Update([FromRoute(Name ="id")] int id, [FromBody] Book book)
        {
            var result = GetBook(id);
            if (result is null)
                return NotFound(); //404
            result.Title = book.Title;
            result.Price = book.Price;

            if (id != book.Id)
                return BadRequest("You can not change id!"); //400

            return Ok(result); //200
        }

        [HttpDelete]
        public IActionResult Delete()
        {
            ApplicationContext.Books.Clear();
            return NoContent(); //204
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute(Name ="id")] int id)
        {
            var book = GetBook(id);
            if (book is null)
                return NotFound($"No such book (id: {id})"); //404
            ApplicationContext.Books.Remove(book);

            //return NoContent(); //204
            return Ok(book); //200
        }

        [HttpPatch("{id:int}")]
        public IActionResult Patch([FromRoute(Name ="id")] int id, [FromBody] JsonPatchDocument<Book> bookPatch)
        {
            var book = GetBook(id);
            if (book is null)
                return NotFound($"No such book (id: {id})"); //404

            bookPatch.ApplyTo(book);

            return Ok(book);
        }

        private Book? GetBook(int id)
        {
            return ApplicationContext.Books.SingleOrDefault(b => b.Id.Equals(id));

        }
    }
}

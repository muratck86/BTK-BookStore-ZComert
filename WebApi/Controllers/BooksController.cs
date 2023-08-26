using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Repositories.Contracts;
using Repositories.EfCore;
using System.Linq;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private IRepositoryManager _repositoryManager;

        public BooksController(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        [HttpGet]
        public ActionResult GetAllBooks()
        {
            try
            {
                var result = _repositoryManager.Book.GetAllBooks(false);
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
                var book = _repositoryManager.Book.GetOneBookById(id, false);
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

                _repositoryManager.Book.CreateOneBook(book);
                _repositoryManager.Save();

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
            var result = _repositoryManager.Book.GetOneBookById(id, true);
            if (result is null)
                return NotFound(); //404

            result.Title = book.Title;
            result.Price = book.Price;

            _repositoryManager.Save();

            if (id != book.Id)
                return BadRequest("You can not change id!"); //400

            return Ok(result); //200
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteABook([FromRoute(Name = "id")] int id)
        {
            var book = _repositoryManager.Book.GetOneBookById(id, true); ;
            if (book is null)
                return NotFound($"No such book (id: {id})"); //404

            _repositoryManager.Book.DeleteOneBook(book);
            _repositoryManager.Save();

            return Ok(book); //200
        }

        [HttpPatch("{id:int}")]
        public IActionResult PatchABook(
            [FromRoute(Name = "id")] int id,
            [FromBody] JsonPatchDocument<Book> bookPatch)
        {
            var book = _repositoryManager.Book.GetOneBookById(id, true);
            if (book is null)
                return NotFound($"No such book (id: {id})"); //404

            bookPatch.ApplyTo(book);
            _repositoryManager.Save();

            return Ok(book);
        }

    }
}

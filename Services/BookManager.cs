using Entities.Exceptions;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BookManager : IBookService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;

        public BookManager(IRepositoryManager manager, ILoggerService logger)
        {
            _manager = manager;
            _logger = logger;
        }

        public Book CreateOneBook(Book book)
        {
            _manager.Book.CreateOneBook(book);
            _manager.Save();
            return book;
        }

        public void DeleteOneBook(int id, bool trackChanges = false)
        {
            var book = _manager.Book.GetOneBookById(id, trackChanges) 
                ?? throw new BookNotFoundException(id);

            _manager.Book.DeleteOneBook(book);
            _manager.Save();
        }

        public IEnumerable<Book> GetAllBooks(bool trackChanges = false)
        {
            return _manager.Book.GetAllBooks(trackChanges);
        }

        public Book GetOneBookById(int id, bool trackChanges = false)
        {
            var book = _manager.Book.GetOneBookById(id, trackChanges)
                ?? throw new BookNotFoundException(id);
            return book;
        }

        public void UpdateOneBook(int id, Book postedBook, bool trackChanges = false)
        {
            var dbBook = _manager.Book.GetOneBookById(id, true);
            if (dbBook is null)
                throw new BookNotFoundException(id);

            dbBook.Title = postedBook.Title;
            dbBook.Price = postedBook.Price;

            _manager.Book.UpdateOneBook(dbBook);
            _manager.Save();
        }
    }
}

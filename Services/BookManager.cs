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
            var book = _manager.Book.GetOneBookById(id, trackChanges);
            if (book is null)
            {
                var message = $"No such book: {nameof(book)}";
                _logger.LogInfo(message);
                throw new ArgumentNullException(message);
            }
            _manager.Book.DeleteOneBook(book);
            _manager.Save();
        }

        public IEnumerable<Book> GetAllBooks(bool trackChanges = false)
        {
            return _manager.Book.GetAllBooks(trackChanges);
        }

        public Book? GetOneBookById(int id, bool trackChanges = false)
        {
            return _manager.Book.GetOneBookById(id, trackChanges);
        }

        public void UpdateOneBook(int id, Book book, bool trackChanges = false)
        {
            var entity = _manager.Book.GetOneBookById(id, true);
            if (entity is null)
            {
                var message = $"No such book: {nameof(book)}";
                _logger.LogInfo(message);
                throw new Exception(message);
            }
            if (book is null)
            {
                var message =$"Argument is null {nameof(book)}";
                _logger.LogInfo(message);
                throw new ArgumentNullException(message);
            }

            entity.Title = book.Title;
            entity.Price = book.Price;

            _manager.Book.UpdateOneBook(entity);
            _manager.Save();
        }
    }
}

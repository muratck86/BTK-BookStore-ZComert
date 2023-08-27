using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class BookManager : IBookService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public BookManager(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
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

        public void UpdateOneBook(int id, BookUpdateDto bookDto, bool trackChanges = false)
        {
            var dbBook = _manager.Book.GetOneBookById(id, trackChanges);
            if (dbBook is null)
                throw new BookNotFoundException(id);

            _mapper.Map(bookDto,dbBook);

            _manager.Book.UpdateOneBook(dbBook);
            _manager.Save();
        }
    }
}

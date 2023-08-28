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

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync(bool trackChanges = false)
        {
            var books = await _manager.Book.GetAllBooksAsync(trackChanges);
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public async Task<BookDto> GetOneBookByIdAsync(int id, bool trackChanges = false)
        {
            var book = await _manager.Book.GetOneBookByIdAsync(id, trackChanges)
                ?? throw new BookNotFoundException(id);

            var bookDto = _mapper.Map<BookDto>(book);
            return bookDto;
        }

        public async Task<BookDto> CreateOneBookAsync(BookCreateDto bookCreateDto)
        {
            var book = _mapper.Map<Book>(bookCreateDto);
            _manager.Book.CreateOneBook(book);
            await _manager.SaveAsync();
            return _mapper.Map<BookDto>(book);
        }

        public async Task DeleteOneBookAsync(int id, bool trackChanges = false)
        {
            var book = await _manager.Book.GetOneBookByIdAsync(id, trackChanges) 
                ?? throw new BookNotFoundException(id);

            _manager.Book.DeleteOneBook(book);
            await _manager.SaveAsync();
        }

        public async Task UpdateOneBookAsync(int id, BookUpdateDto bookDto, bool trackChanges = false)
        {
            var dbBook = await _manager.Book.GetOneBookByIdAsync(id, trackChanges);
            if (dbBook is null)
                throw new BookNotFoundException(id);

            _mapper.Map(bookDto,dbBook);

            _manager.Book.UpdateOneBook(dbBook);
            await _manager.SaveAsync();
        }

        public async Task<(BookUpdateDto bookUpdateDto, Book book)> GetOneBookForPatchAsync(int id, bool trackChanges)
        {
            var book = await _manager.Book.GetOneBookByIdAsync(id, trackChanges);
            if (book is null)
                throw new BookNotFoundException(id);

            var bookDto = _mapper.Map<BookUpdateDto>(book);
            return (bookDto, book);

        }
    }
}

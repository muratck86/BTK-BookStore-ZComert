using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Entities.RequestFeatures;
using Repositories.Contracts;
using Services.Contracts;
using System.Dynamic;

namespace Services
{
    public class BookManager : IBookService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;
        private readonly IDataShaper<BookDto> _shaper;

        public BookManager(IRepositoryManager manager, IMapper mapper, IDataShaper<BookDto> shaper)
        {
            _manager = manager;
            _mapper = mapper;
            _shaper = shaper;
        }

        public async Task<(IEnumerable<ExpandoObject> books, MetaData metaData)> GetAllBooksAsync(
            BookParameters bookParameters,
            bool trackChanges = false)
        {
            if(!bookParameters.IsValidPriceRange)
            {
                throw new PriceOutOfRangeBadRequestException();
            }
            var pagedBooks = await _manager.Book.GetAllBooksAsync(bookParameters,trackChanges);
            var booksDto = _mapper.Map<IEnumerable<BookDto>>(pagedBooks);
            var shapedBooks = _shaper.ShapeData(booksDto, bookParameters.Fields);
            return (shapedBooks, pagedBooks.MetaData);
        }

        public async Task<BookDto> GetOneBookByIdAsync(int id, bool trackChanges = false)
        {
            var book = await GetOneBookHelperAsync(id,trackChanges);
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
            var book = await GetOneBookHelperAsync(id, trackChanges);
            _manager.Book.DeleteOneBook(book);
            await _manager.SaveAsync();
        }

        public async Task UpdateOneBookAsync(int id, BookUpdateDto bookDto, bool trackChanges = false)
        {
            var dbBook = await GetOneBookHelperAsync(id, trackChanges);
            _mapper.Map(bookDto,dbBook);

            _manager.Book.UpdateOneBook(dbBook);
            await _manager.SaveAsync();
        }

        public async Task<(BookUpdateDto bookUpdateDto, Book book)> GetOneBookForPatchAsync(int id, bool trackChanges)
        {
            var book = await GetOneBookHelperAsync(id, trackChanges);
            var bookDto = _mapper.Map<BookUpdateDto>(book);
            return (bookDto, book);
        }

        private async Task<Book> GetOneBookHelperAsync(int id, bool trackChanges)
        {
            var book = await _manager.Book.GetOneBookByIdAsync(id, trackChanges)
                ?? throw new BookNotFoundException(id);
            return book;
        }
    }
}

using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.LinkModels;
using Entities.Models;
using Entities.RequestFeatures;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class BookManager : IBookService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;
        private readonly IBookLinks _bookLinks;

        public BookManager(IRepositoryManager manager, IMapper mapper, IBookLinks bookLinks)
        {
            _manager = manager;
            _mapper = mapper;
            _bookLinks = bookLinks;
        }

        public async Task<(LinkResponse linkResponse, MetaData metaData)> GetAllBooksAsync(
            BookLinkParameters linkParameters,
            bool trackChanges = false)
        {
            if(!linkParameters.BookParameters.IsValidPriceRange)
            {
                throw new PriceOutOfRangeBadRequestException();
            }
            var pagedBooks = await _manager.Book.GetAllBooksAsync(linkParameters.BookParameters,trackChanges);
            var booksDto = _mapper.Map<IEnumerable<BookDto>>(pagedBooks);
            var linkResponse = _bookLinks.TryGenerateLinks(
                booksDto,
                linkParameters.BookParameters.Fields,
                linkParameters.HttpContext);

            return (linkResponse, pagedBooks.MetaData);
        }
        public async Task<List<BookDetailsDto>> GetAllBooksAsync(bool trackChanges)
        {
            var books = await _manager.Book.GetAllBookDetailsAsync(trackChanges);
            return _mapper.Map<List<BookDetailsDto>>(books);
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

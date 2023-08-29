using AutoMapper;
using Entities.DataTransferObjects;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IBookService> _bookService;

        public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper, IDataShaper<BookDto> shaper)
        {
            _bookService = new Lazy<IBookService>(() => new BookManager(repositoryManager,mapper,shaper));
        }

        public IBookService BookService => _bookService.Value;
    }
}
